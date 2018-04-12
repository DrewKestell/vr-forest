using UnityEngine;
using VRTK;

public class Hatchet : MonoBehaviour
{
    static System.Random random = new System.Random();
    AudioSource audioSource;
    VRTK_InteractGrab controller;
    bool currentlyHeld;

    public AudioClip[] AudioClips;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        var interact = GetComponent<VRTK_InteractableObject>();
        interact.InteractableObjectGrabbed += new InteractableObjectEventHandler(DoObjectGrab);
        interact.InteractableObjectUngrabbed += new InteractableObjectEventHandler(DoObjectUngrab);
    }

    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            var randomAudioClip = AudioClips[random.Next(AudioClips.Length)];
            audioSource.clip = randomAudioClip;
            audioSource.Play();

            if (currentlyHeld)
                VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(controller.gameObject), randomAudioClip);
        }
    }

    void DoObjectGrab(object sender, InteractableObjectEventArgs e)
    {
        currentlyHeld = true;

        if (VRTK_DeviceFinder.IsControllerLeftHand(e.interactingObject))
            controller = VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_InteractGrab>();
        else
            controller = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_InteractGrab>();
    }

    void DoObjectUngrab(object sender, InteractableObjectEventArgs e) => currentlyHeld = false;
}
