using UnityEngine;

public class Inventory : MonoBehaviour
{    
    public InventoryUI InventoryUI;
    public Transform Camera;

    public AudioSource AudioSource;

    void Update()
    {
        transform.localPosition = new Vector3(Camera.localPosition.x, Camera.localPosition.y, Camera.localPosition.z);
        transform.Translate(-Camera.forward.x, -1f, -Camera.forward.z);
    }

    void OnTriggerEnter(Collider collider)
    {
        var storable = collider.GetComponentInParent<Storable>();

        if (storable != null)
        {
            if (InventoryUI.AddItemToInventory(storable))
            {
                AudioSource.Play();
                storable.gameObject.SetActive(false);
            }
        }
    }
}
