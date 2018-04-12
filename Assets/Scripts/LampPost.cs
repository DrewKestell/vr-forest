using UnityEngine;

public class LampPost : MonoBehaviour {
    public GameObject TimeManager;
    Timer timer;
    new Light light;
    Material glassMaterial;

	void Start ()
    {
        timer = TimeManager.GetComponent<Timer>();
        light = gameObject.GetComponentInChildren<Light>();
        glassMaterial = gameObject.transform.Find("Glass").gameObject.GetComponent<Renderer>().material;
	}
	
	void Update ()
    {
        // turn on the light at 6pm
		if (timer.HourCount == 18 && timer.MinuteCount == 0)
        {
            if (light.intensity == 0)
            {
                light.intensity = 1f;
                glassMaterial.SetColor("_EmissionColor", new Color(1.236907f, 1.3f, 0.65f, 1));
            }
        }
        // turn off the light at 6am
        if (timer.HourCount == 6 && timer.MinuteCount == 0 && light.intensity == 0)
        {
            if (light.intensity > 0)
            {
                light.intensity = 0;
                glassMaterial.SetColor("_EmissionColor", Color.black);
            }           
        }
	}
}
