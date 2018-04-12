using System.Collections;
using System.Linq;
using UnityEngine;

public class TreeShatter : MonoBehaviour
{
    static System.Random random = new System.Random();
    ParticleSystem woodShards;
    float lastHitTime;
    int hitCount;
    CapsuleCollider axeHandle;
    BoxCollider axeBlade;

    public Material TrunkFadeMaterial;
    public GameObject Loot;

    void Start()
    {
        var hatchet = GameObject.Find("Hatchet");
        axeHandle = hatchet.GetComponentInChildren<CapsuleCollider>();
        axeBlade = hatchet.GetComponentInChildren<BoxCollider>();
        woodShards = transform.parent.Find("WoodShards").GetComponent<ParticleSystem>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Blade" && collision.relativeVelocity.magnitude > 4 && (lastHitTime == 0 || Time.time > lastHitTime + 1))
        {
            hitCount++;
            lastHitTime = Time.time;
            woodShards.transform.position = collision.contacts[0].point;
            woodShards.Play();

            if (hitCount == 3)
            {
                transform.GetComponent<MeshCollider>().enabled = false;

                var cells = transform.parent.GetComponentsInChildren<Transform>()
                    .Where(c => c.name.Contains("Trunk_cell"));

                foreach (var cell in cells)
                {
                    var randomX = random.Next(150);
                    var randomY = random.Next(150);
                    var randomZ = random.Next(150);

                    cell.gameObject.AddComponent<Rigidbody>();
                    cell.GetComponent<Rigidbody>().AddForce(new Vector3(randomX, randomY, randomZ));
                    Physics.IgnoreCollision(cell.GetComponent<MeshCollider>(), axeHandle);
                    Physics.IgnoreCollision(cell.GetComponent<MeshCollider>(), axeBlade);
                    cell.GetComponent<MeshRenderer>().material = TrunkFadeMaterial;
                }

                var newObject = Instantiate(Loot);
                newObject.transform.position = new Vector3(transform.parent.position.x, 1f, transform.parent.position.z);

                StartCoroutine(FadeOut());
            }
        }
    }

    IEnumerator FadeOut()
    {
        var initialColor = TrunkFadeMaterial.color;
        var color = TrunkFadeMaterial.color;

        for (float f = 1; f >= 0; f -= 0.01f)
        {
            color.a = f;
            TrunkFadeMaterial.color = color;
            yield return null;
        }

        TrunkFadeMaterial.color = initialColor;
        transform.parent.parent.GetComponentInChildren<TreeRespawner>().PrepareRespawn();
        Destroy(transform.parent.gameObject);
    }
}
