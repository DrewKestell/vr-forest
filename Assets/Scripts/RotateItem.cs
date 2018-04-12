using UnityEngine;

public class RotateItem : MonoBehaviour
{
	void Update()
    {
        transform.Rotate(Vector3.up * 1.5f, Space.World);
	}
}
