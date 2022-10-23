using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerAnimations currentAnim;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAnimation(PlayerAnimations animation)
    {
        anim.Play(animation.ToString());
        currentAnim = animation;
    }
}
