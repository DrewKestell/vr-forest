using System.Collections;
using UnityEngine;

public class FoliageExplode : MonoBehaviour
{
    public GameObject ParticleSystem;
    public Material FadeMaterial;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "GrassTop")
        {
            var newObject = Instantiate(ParticleSystem, new Vector3(transform.position.x, 0, transform.position.z), new Quaternion());
            var particleSystem = newObject.GetComponent<ParticleSystem>();
            particleSystem.Play();

            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = FadeMaterial;

        var initialColor = FadeMaterial.color;
        var color = FadeMaterial.color;

        for (float f = 1; f >= 0; f -= 0.1f)
        {
            color.a = f;
            FadeMaterial.color = color;
            yield return null;
        }

        meshRenderer.enabled = false;
    }
}
