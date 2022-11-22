using UnityEngine;

public class User : MonoBehaviour
{
    [HideInInspector] public States currentState; 

    public enum States
    {
        Playing,
        Helpo,
        Quest
    }
}
