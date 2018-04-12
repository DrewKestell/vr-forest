using UnityEngine.UI;
using VRTK;

public class Checkbox : VRTK_InteractableObject
{
    Toggle toggle;

    void Start()
    {
        toggle = GetComponentInParent<Toggle>();
    }

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        if (toggle.isOn)
            toggle.isOn = false;
        else
            toggle.isOn = true;
    }
}
