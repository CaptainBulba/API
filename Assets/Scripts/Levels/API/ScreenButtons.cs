using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenButtons : MonoBehaviour
{
    [SerializeField] private GameObject pipemanObject;

    public void OpenPipeman()
    {
        pipemanObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
