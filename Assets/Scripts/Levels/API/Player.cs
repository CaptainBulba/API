using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float speed = 2f;
    private Vector2 currentPos;
    private Vector2 targetPos;
    private float extraCord = 0.5f; 

    private bool activatePlayer;
    
    private ApiController apiController;
    private Canvas canvas;
    private TextMeshProUGUI nameText;

    // Timers
    private float currentTime = 0;
    private float activateTime = 1.5f;
    private float typingTimer = 0.125f;

    private string wallTag = "Wall";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        apiController = FindObjectOfType<ApiController>();
       
        activatePlayer = true;
    }

    private void Update()
    {
        if (Vector3.Distance(currentPos, targetPos) > 0.01f)
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);


        if (activatePlayer && currentTime <= activateTime)
        {
            currentTime += Time.deltaTime;

            float alphaValue = Mathf.Lerp(0f, 1f, currentTime / activateTime);

            GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, alphaValue);
            nameText.color = new Color(0f, 0f, 0f, alphaValue);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void SetTargetPosition(float x, float y)
    {
        currentPos = transform.position;
        targetPos = new Vector2(x + extraCord, y + extraCord);
    }

    public void ActivatePlayer(string name,int x, int y)
    { 
        gameObject.SetActive(true);
    
        AssignUiElements();
        canvas.worldCamera = FindObjectOfType<Camera>();

        nameText.text = name;
        transform.position = new Vector2(x + extraCord, y + extraCord);
       
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
        if(col.gameObject.CompareTag(wallTag))
        {
            transform.position = currentPos;
            targetPos = currentPos;
        }
    }

    private void AssignUiElements()
    {
        canvas = GetComponentInChildren<Canvas>();
        nameText = canvas.GetComponentInChildren<TextMeshProUGUI>();
    }
}
