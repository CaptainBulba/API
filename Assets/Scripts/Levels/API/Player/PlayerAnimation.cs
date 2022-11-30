using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    public enum AnimNames
    {
        PlayerIdle,
        PlayerWalking,
        PlayerJump
    }


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAnimation(AnimNames animation)
    {
        anim.Play(animation.ToString());
    }

    public void PlayAnimationOnce(AnimNames animation)
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
