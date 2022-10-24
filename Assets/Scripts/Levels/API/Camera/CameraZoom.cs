
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;

    private float targetZoom;
    private float zoomFactor = 3f;

    private Vector2 zoomPos;

    private Vector2 previousPos;
    private float previousZoom;

    [SerializeField] private float zoomSpeed = 10;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 5f;

    private ZoomStates currentState;

    private enum ZoomStates
    {
        None,
        Zooming, 
        Returning
    }

    private void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    private void LateUpdate()
    {
        if(currentState != ZoomStates.None)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(zoomPos.x, zoomPos.y, cam.transform.position.z), zoomSpeed * Time.deltaTime);
            
            if (currentState == ZoomStates.Zooming)
                    targetZoom = Mathf.Clamp(minZoom, minZoom, maxZoom);

            else if (currentState == ZoomStates.Returning)
            {
                targetZoom = previousZoom;
                if (Mathf.Abs(targetZoom - cam.orthographicSize) <= 0.1f)
                    currentState = ZoomStates.None;
            }
        }
        else
        {
            float scrollData = Input.GetAxis("Mouse ScrollWheel");
            targetZoom -= scrollData * zoomFactor;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    }

    public void ZoomToObject(GameObject obj)
    {
        if(currentState != ZoomStates.Zooming)
        {
            previousZoom = cam.orthographicSize;
            zoomPos = obj.transform.position;
            currentState = ZoomStates.Zooming;
        }
    }

    public void ReturnFromZoom()
    {
        zoomPos = previousPos;
        currentState = ZoomStates.Returning;
    }
}
