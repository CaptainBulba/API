using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 oldPos;
    private Vector3 panOrigin;
    private float panSpeed = 26;
    private bool bDragging = false;

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(2))
        {
            bDragging = true;
            oldPos = transform.position;
            panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            pos = pos - panOrigin;
            transform.position = oldPos + -pos * panSpeed;
        }

        if (Input.GetMouseButtonUp(2))
        {
            bDragging = false;
        }
    }
}
