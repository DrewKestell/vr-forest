using System.Collections;
using UnityEngine;

public class FadeInLoot : MonoBehaviour
{
    public Material Material;

    void Start()
    {
        var color = Material.color;
        color.a = 0;
        Material.color = color;

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        var initialColor = Material.color;
        var color = Material.color;

        for (float f = 0; f <= .95f; f += 0.01f)
        {
            color.a = f;
            Material.color = color;
            yield return null;
        }
    }
}
