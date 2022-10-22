using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCoordinates : MonoBehaviour
{
    private TextMeshProUGUI coords; 
    private void Start()
    {
        coords = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeCoords(string text)
    {
        coords.text = text;
    }
}
