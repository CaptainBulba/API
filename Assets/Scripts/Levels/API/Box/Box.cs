using UnityEngine;

public class Box : MonoBehaviour
{
    private bool activateBox = false;

    private float currentTime = 0;
    private float activateTime = 1.5f;

    private float extraCord = 0.5f;

    private void Update()
    {
        if (activateBox && currentTime <= activateTime)
        {
            currentTime += Time.deltaTime;

            float alphaValue = Mathf.MoveTowards(0f, 1f, currentTime / activateTime);

            GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, alphaValue);
        }
    }

    public void ActivateBox(int x, int y)
    {
        gameObject.SetActive(true);

        transform.position = new Vector2(x + extraCord, y + extraCord);

        activateBox = true;
    }
}
