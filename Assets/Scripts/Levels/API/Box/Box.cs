using System.Collections;
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

    public void CoroutineMoveBox(int x, int y)
    {
        StartCoroutine(MoveBox(x, y));
    }

    private IEnumerator MoveBox(int x, int y)
    {
        //TODO: Box Animation

        transform.position = new Vector2(x + extraCord, y + extraCord);
        yield return new WaitForSeconds(1f);
    }

    public void ActiveFlag(bool option)
    {
        enabled = option;
    }

    public void DeleteObject()
    {
        Destroy(gameObject);
    }
}
