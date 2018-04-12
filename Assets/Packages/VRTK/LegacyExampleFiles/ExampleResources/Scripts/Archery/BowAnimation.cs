namespace VRTK.Examples.Archery
{
    using UnityEngine;

    public class BowAnimation : MonoBehaviour
    {
        public Animation animationTimeline;

        public void SetFrame(float frame)
        {
            animationTimeline["Bow_DrawBack"].speed = 0;
            animationTimeline["Bow_DrawBack"].time = frame;
            animationTimeline.Play("Bow_DrawBack");
        }
    }
}