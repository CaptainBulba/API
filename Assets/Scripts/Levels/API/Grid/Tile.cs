using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer ren;
    [SerializeField] private GameObject highlight;
    [SerializeField] private TextMesh cordText;

    public void InitiateCoordinates(int x, int y)
    {
        cordText.text = x + ", " + y;
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
     //   Debug.Log(transform.position.x + " " + transform.position.y);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
