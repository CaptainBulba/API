using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 oldPos;
    private Vector3 panOrigin;
    [SerializeField] private float moveSpeed = 10;

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            oldPos = transform.position;
            panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            pos = pos - panOrigin;
            transform.position = oldPos + -pos * moveSpeed;
        }
    }
}
