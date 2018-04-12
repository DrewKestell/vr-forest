using UnityEngine;

public class TreeFracture : MonoBehaviour
{
    public Material TrunkFadeMaterial;
    public GameObject FracturedPrefab;

    ParticleSystem woodShards;
    int hitCount;
    bool falling;
    float lastHitTime;

    void Start()
    {
        woodShards = transform.parent.Find("WoodShards").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        var rigidbody = transform.parent.GetComponent<Rigidbody>();

        if (rigidbody == null) return;

        var velocity = rigidbody.velocity.magnitude;

        if (velocity > 0)
            falling = true;

        if (falling && velocity == 0)
        {
            var newObject = Instantiate(FracturedPrefab, transform.parent.position, transform.parent.rotation, transform.parent.parent);
            newObject.transform.localScale = transform.parent.localScale;
            newObject.GetComponentInChildren<TreeShatter>().TrunkFadeMaterial = TrunkFadeMaterial;

            Destroy(transform.parent.gameObject);
        }
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

                transform.parent.gameObject.AddComponent<Rigidbody>();
                transform.parent.gameObject.AddComponent<SpringJoint>();

                var normalizedCollisionVector = collision.relativeVelocity.normalized;
                transform.parent.GetComponent<Rigidbody>().AddForce(new Vector3(normalizedCollisionVector.x * 100, 0, normalizedCollisionVector.z * 100));
            }
        }
    }
}
