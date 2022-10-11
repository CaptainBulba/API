
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 2f;
    private Vector2 currentPos;
    private Vector2 targetPos;

    private ApiController apiController;

    private void Start()
    {
        apiController = FindObjectOfType<ApiController>();
    }

    private void Update()
    {
        if (Vector3.Distance(currentPos, targetPos) > 0.01f)
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }

    public void SetTargetPosition(float x, float y)
    {
        targetPos = new Vector2(x, y);
    }

    public void ActivatePlayer()
    {
        gameObject.SetActive(true);
    }
}
