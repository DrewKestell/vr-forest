using UnityEngine;
using VRTK;

public class MenuManager : MonoBehaviour
{
    public GameObject InventoryCanvas;
    public GameObject SettingsCanvas;

    public VRTK_ControllerEvents LeftControllerEvents;
    public VRTK_ControllerEvents RightControllerEvents;
    public Transform PlayerPosition;

    bool showing;
    GameObject currentCanvas;
    bool buttonHeld;
    float startPressTime;
    bool ignoreRelease;

    void Start()
    {
        LeftControllerEvents.ButtonTwoPressed += ButtonTwoPress;
        RightControllerEvents.ButtonTwoPressed += ButtonTwoPress;

        LeftControllerEvents.ButtonTwoReleased += ButtonTwoRelease;
        RightControllerEvents.ButtonTwoReleased += ButtonTwoRelease;
    }

    void Update()
    {
        if (!showing && buttonHeld && Time.time > startPressTime + 0.5f)
        {
            ignoreRelease = true;
            ShowCanvas(SettingsCanvas);
        }
    }

    void ButtonTwoPress(object sender, ControllerInteractionEventArgs e)
    {
        buttonHeld = true;
        
        startPressTime = Time.time;
    }

    void ButtonTwoRelease(object sender, ControllerInteractionEventArgs e)
    {
        if (showing && !ignoreRelease)
        {
            currentCanvas.SetActive(false);
            currentCanvas = null;
            showing = false;
        }
        else
        {
            if (Time.time <= startPressTime + 0.5f)
                ShowCanvas(InventoryCanvas);
        }

        ignoreRelease = false;
        buttonHeld = false;
    }

    void ShowCanvas(GameObject canvas)
    {
        canvas.transform.SetPositionAndRotation(new Vector3(PlayerPosition.transform.position.x, PlayerPosition.transform.position.y - 0.25f, PlayerPosition.transform.position.z), new Quaternion());
        canvas.transform.Translate(new Vector3(PlayerPosition.transform.forward.x * .75f, 0, PlayerPosition.transform.forward.z * .75f));
        canvas.transform.Rotate(new Vector3(0, PlayerPosition.transform.rotation.eulerAngles.y, 0));
        canvas.SetActive(true);
        currentCanvas = canvas;
        showing = true;
    }
}
