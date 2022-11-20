using System.Collections;
using TMPro;
using UnityEngine;

public class Button : MonoBehaviour
{
    private LoadNextScene scene;
    private Animator anim;

    private string pressedAnim = "ButtonPressed";
    private float extraTime = 1f;

    [SerializeField] private TextMeshProUGUI buttonText; 

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
        buttonText.enabled = false;

        player.Jump();
        anim.Play(pressedAnim);

        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSecondsRealtime(animationLength + extraTime);
        
        player.SetIdleState();
        scene.NextScene();
    }
}
