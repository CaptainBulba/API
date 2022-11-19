using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAnimation(PlayerAnimations animation)
    {
        anim.Play(animation.ToString());
    }

    public void PlayAnimationOnce(PlayerAnimations animation)
    {
        anim.Play(animation.ToString(), 0, 0.0f);
    }


    public float AnimatorIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }


    public bool AnimatorIsPlaying1()
    {
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

}
