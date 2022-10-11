using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer ren;
    [SerializeField] private GameObject highlight;

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
        Debug.Log(transform.position.x + " " + transform.position.y);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
