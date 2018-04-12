using UnityEngine;
using VRTK;

public class ItemSlot : VRTK_InteractableObject
{
    [HideInInspector]
    public Storable Storable { get; set; }
    [HideInInspector]
    public int Quantity { get; set; }

    InventoryUI InventoryUI;

    void Start()
    {
        InventoryUI = GetComponentInParent<InventoryUI>();
    }

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        if (Storable != null)
            InventoryUI.ToggleItemDisplay(this);      
    }
}
