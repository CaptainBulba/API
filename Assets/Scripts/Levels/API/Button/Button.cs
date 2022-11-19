using System.Collections;
using UnityEngine;

public class Button : MonoBehaviour
{
    private LoadNextScene scene;
    private Animator anim;

    private string pressedAnim = "ButtonPressed";

    private float extraTime = 1f; 

    private void Start()
    {
        scene = GetComponent<LoadNextScene>();
        anim = GetComponent<Animator>();
        
    }

    public void CoroutinePressButton()
    {
        StartCoroutine(PressButton());
    }

    private IEnumerator PressButton()
    {
        Player player = ApiController.Instance.GetPlayer().GetComponent<Player>();

        player.Jump();
        anim.Play(pressedAnim);

        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length + extraTime;
        yield return new WaitForSecondsRealtime(animationLength);

        player.SetIdleState();
        scene.NextScene();
    }
}
