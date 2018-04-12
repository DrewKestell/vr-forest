using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour {
    static System.Random random = new System.Random();
    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!AnimatorIsPlaying())
        {
            var rand = random.NextDouble();

            if (rand > 0.85 && rand < 0.9)
                animator.Play("wolf_bow");
            else if (rand > 0.9 && rand < 0.95)
                animator.Play("wolf_idle01");
            else if (rand > 0.95)
                animator.Play("wolf_idle02");
        }
	}

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
