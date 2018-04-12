using UnityEngine;
using System.Collections.Generic;

public class Target : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Arrow")
        {
            Debug.Log("yeah!");
            var rigidbodies = new List<Rigidbody>();
            rigidbodies.AddRange(collision.collider.gameObject.GetComponentsInChildren<Rigidbody>());
            rigidbodies.AddRange(collision.collider.gameObject.GetComponentsInParent<Rigidbody>());

            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }
}
