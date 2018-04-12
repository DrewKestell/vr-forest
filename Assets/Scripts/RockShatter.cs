using System.Collections;
using UnityEngine;

public class RockShatter : MonoBehaviour
{
    float birthTime;
    ParticleSystem smoke;
    ParticleSystem sparks;
    AudioSource rocksCrumbling;

    public string AnimationClipName;
    public Material FadeMaterial;
    public GameObject Loot;

    void Start()
    {
        birthTime = Time.time;
        smoke = transform.parent.Find("RockBreakSmoke").GetComponent<ParticleSystem>();
        sparks = transform.parent.Find("RockBreakSparks").GetComponent<ParticleSystem>();
        rocksCrumbling = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Pick" && Time.time > birthTime + 1 && collision.relativeVelocity.magnitude > 4)
        {
            GetComponent<MeshCollider>().enabled = false;
            transform.parent.GetComponent<Animator>().Play(AnimationClipName);

            foreach (var material in transform.parent.GetComponentsInChildren<MeshRenderer>())
                material.material = FadeMaterial;

            rocksCrumbling.Play();
            smoke.Play();
            sparks.transform.position = collision.contacts[0].point;
            sparks.Play();

            var newObject = Instantiate(Loot);
            newObject.transform.position = new Vector3(transform.parent.position.x, 1f, transform.parent.position.z);

            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        var color = FadeMaterial.color;

        for (float f = 1; f >= 0; f -= 0.01f)
        {
            color.a = f;
            FadeMaterial.color = color;
            yield return null;
        }

        color.a = 1f;
        FadeMaterial.color = color;        
        Destroy(transform.parent.gameObject);
    }
}
