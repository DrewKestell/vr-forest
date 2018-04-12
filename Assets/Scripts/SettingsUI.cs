using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class SettingsUI : MonoBehaviour
{
    public Toggle SlideMovementToggle;
    public GameObject LeftTouchpadControl;
    public GameObject RightTouchpadControl;
    public GameObject TouchpadControlActions;
    public VRTK_HeightAdjustTeleport TeleportControl;
    public VRTK_Pointer LeftControllerPointer;
    public VRTK_BezierPointerRenderer LeftControllerPointerRenderer;
    public VRTK_Pointer RightControllerPointer;
    public VRTK_BezierPointerRenderer RightControllerPointerRenderer;

    void Start()
    {
        SlideMovementToggle.onValueChanged.AddListener(delegate
        {
            ToggleValueChanged(SlideMovementToggle);
        });
	}
	
	void ToggleValueChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            TeleportControl.enabled = false;
            LeftTouchpadControl.SetActive(true);
            RightTouchpadControl.SetActive(true);
            TouchpadControlActions.SetActive(true);
            LeftControllerPointer.enabled = false;
            LeftControllerPointerRenderer.enabled = false;
            RightControllerPointer.enabled = false;
            RightControllerPointerRenderer.enabled = false;
        }
        else
        {
            TeleportControl.enabled = true;
            LeftTouchpadControl.SetActive(false);
            RightTouchpadControl.SetActive(false);
            TouchpadControlActions.SetActive(false);
            LeftControllerPointer.enabled = true;
            LeftControllerPointerRenderer.enabled = true;
            RightControllerPointer.enabled = true;
            RightControllerPointerRenderer.enabled = true;
        }
    }
}
