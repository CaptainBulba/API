using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private PlayerAnimation anim;
    private SpriteRenderer sprite;

    private float speed = 2f;
    private Vector2 startPos;
    private Vector2 targetPos;
    private float extraCord = 0.5f; 

    private bool activatePlayer;
    
    private Canvas canvas;
    private TextMeshProUGUI nameText;

    private GridManager gridManager;

    // Timers
    private float currentTime = 0;
    private float activateTime = 1.5f;
    private float typingTimer = 0.125f;

    private PlayerStates currentState;

    private enum PlayerStates
    {
        Idle, 
        Walking,
        Jumping
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        anim = GetComponent<PlayerAnimation>();
        sprite = GetComponent<SpriteRenderer>();

        gridManager = FindObjectOfType<GridManager>();
       
        activatePlayer = true;
    }

    private void Update()
    {
        if(currentState != PlayerStates.Jumping)
        {
            if (new Vector2(transform.position.x, transform.position.y) != targetPos)
            {
                Vector2 dir = (new Vector2(transform.position.x, transform.position.y) - targetPos).normalized;

                if (dir.x > 0f)
                {
                    anim.ChangeAnimation(PlayerAnimation.AnimNames.PlayerWalking);
                    sprite.flipX = true;
                }
                else if (dir.x < 0f)
                {
                    anim.ChangeAnimation(PlayerAnimation.AnimNames.PlayerWalking);
                    sprite.flipX = false;
                }
                else if (dir.y > 0f || dir.y < 0f)
                    anim.ChangeAnimation(PlayerAnimation.AnimNames.PlayerWalking);

                transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
                currentState = PlayerStates.Walking;
            }
            else
            {
                anim.ChangeAnimation(PlayerAnimation.AnimNames.PlayerIdle);
                currentState = PlayerStates.Idle;
            }
        }
        
        if (activatePlayer && currentTime <= activateTime)
        {
            currentTime += Time.deltaTime;

            float alphaValue = Mathf.MoveTowards(0f, 1f, currentTime / activateTime);

            GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, alphaValue);
            nameText.color = new Color(0f, 0f, 0f, alphaValue);
        }
    }

    public void SetTargetPosition(float x, float y)
    {
        startPos = transform.position;
        targetPos = new Vector2(x + extraCord, y + extraCord);
    }

    public void ActivatePlayer(string name, int x, int y)
    { 
        gameObject.SetActive(true);
    
        AssignUiElements();
        canvas.worldCamera = FindObjectOfType<Camera>();

        nameText.text = name;
        transform.position = new Vector2(x + extraCord, y + extraCord);
        targetPos = transform.position;

        activatePlayer = true;
    }

    public void CoroutineChangeName(string name)
    {
        StartCoroutine(ChangeName(name));
    }

    public IEnumerator ChangeName(string newName)
    {
        string currentName = nameText.text;

        if (!string.IsNullOrWhiteSpace(currentName))
        {
            foreach (char c in currentName)
            {
                nameText.text = currentName.Substring(0, currentName.Length - 1);
                currentName = currentName.Substring(0, currentName.Length - 1);
                yield return new WaitForSeconds(typingTimer);
            }
        }

        foreach (char c in newName)
        {
            nameText.text += c;
            yield return new WaitForSeconds(typingTimer);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag(ObjectsTags.Wall.ToString()))
        {
            Vector2 newPosition = gridManager.GetClosestTile(gameObject);

            transform.position = newPosition;
            targetPos = newPosition;
        }
    }

    private void AssignUiElements()
    {
        canvas = GetComponentInChildren<Canvas>();
        nameText = canvas.GetComponentInChildren<TextMeshProUGUI>();
    }

    public Vector3 GetStartPosition()
    {
        return startPos;
    }

    public void Jump()
    {
        anim.PlayAnimationOnce(PlayerAnimation.AnimNames.PlayerJump);
        currentState = PlayerStates.Jumping;
    }

    public void SetIdleState()
    {
        currentState = PlayerStates.Idle;
    }

    public void DecoySwitch(GameObject playerDecoy)
    {
        transform.position = playerDecoy.transform.position;
        targetPos = transform.position;
    }
    
    // Testing
    void OnMouseDown()
    {
        if (Input.GetKey("mouse 0"))
        {
            PressedButton pressedButton = new PressedButton(ApiController.Instance.GetButtonObject());
            pressedButton.Run();
        }
    }
}
