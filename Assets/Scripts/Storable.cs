using UnityEngine;

public class Storable : MonoBehaviour
{
    public Sprite Sprite;
    public UnityEngine.Video.VideoClip VideoClip;
    public string ItemName;
    [TextArea]
    public string DescriptionText;
    public bool Stackable;
    public int Id;

	void Update ()
    {
		
	}
}
