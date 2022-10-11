
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 2f;
    private Vector2 currentPos;
    private Vector2 targetPos;

    private float activateTime = 3f;
    private bool activatePlayer;
    private float currentTime = 0;

    private ApiController apiController;

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
            
            float value = Mathf.Lerp(0f, 1f, currentTime / activateTime);

            GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, value);
        }
    }

    public void SetTargetPosition(float x, float y)
    {
        targetPos = new Vector2(x, y);
    }

    public void ActivatePlayer()
    {
        gameObject.SetActive(true);
        activatePlayer = true;
    }
}
