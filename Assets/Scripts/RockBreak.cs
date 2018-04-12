using UnityEngine;

public class RockBreak : MonoBehaviour
{
    public GameObject prefab;

	void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Pick" && collision.relativeVelocity.magnitude > 4)
        {
            var scale = transform.localScale.x;

            var newObject = Instantiate(prefab, transform.position, new Quaternion());
            newObject.transform.localScale = new Vector3(scale, scale, scale);
            var sparks = newObject.transform.Find("RockBreakSparks").GetComponent<ParticleSystem>();
            sparks.transform.position = collision.contacts[0].point;
            sparks.Play();
            Destroy(gameObject);
        }
    }
}
