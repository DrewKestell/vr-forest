using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public GameObject TimeManager;

    Text text;
    Timer timer;

	void Start ()
    {
        timer = TimeManager.GetComponent<Timer>();
        text = gameObject.GetComponent<Text>();
    }
	
	void Update ()
    {
        text.text = timer.GetTime();
	}
}
