using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject TimeManager;
    public AudioSource DaytimeAmbience;
    public AudioSource NighttimeAmbience;

    Timer timer;
    bool fading;

    void Start()
    {
        timer = TimeManager.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        // turn on the light at 6pm
        if (timer.HourCount == 17 && timer.MinuteCount == 0 && !fading)
        {
            fading = true;
            NightFade();
        }
        // turn off the light at 6am
        if (timer.HourCount == 5 && timer.MinuteCount == 0 && !fading)
        {
            fading = true;
            DayFade();
        }        
    }

    void NightFade()
    {
        StartCoroutine(FadeOut(DaytimeAmbience));
        StartCoroutine(FadeIn(NighttimeAmbience));
    }

    void DayFade()
    {
        StartCoroutine(FadeOut(NighttimeAmbience));
        StartCoroutine(FadeIn(DaytimeAmbience));
    }

    IEnumerator FadeOut(AudioSource audioSource)
    {
        for (float f = 1f; f >= 0; f -= 0.05f)
        {
            audioSource.volume = f;
            yield return new WaitForSeconds(1f);
        }
        fading = false;
        audioSource.Stop();
    }

    IEnumerator FadeIn(AudioSource audioSource)
    {
        for (float f = 0; f <= 1f; f += 0.05f)
        {
            // FIXME: the SteamVR Camera prefab isn't enabled during the Start method for some reason, so we have to start the AudioClip here
            if (!audioSource.isPlaying)
                audioSource.Play();

            audioSource.volume = f;
            yield return new WaitForSeconds(1f);
        }
    }
}
