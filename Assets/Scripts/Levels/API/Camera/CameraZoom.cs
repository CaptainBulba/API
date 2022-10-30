
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;

    private float targetZoom;
    private float zoomFactor = 3f;

    private Vector3 zoomPos;
    private Vector3 previousPos;
    private float previousZoom;

    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float zoomSpeed = 3;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 5f;

    private ZoomStates currentState;

    private bool initiateQuest = false;

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

    private void Update()
    {
        if (currentState != ZoomStates.None)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(zoomPos.x, zoomPos.y, cam.transform.position.z), moveSpeed * Time.deltaTime);

            if (currentState == ZoomStates.Zooming)
                targetZoom = Mathf.Clamp(minZoom, minZoom, maxZoom);

            else if (currentState == ZoomStates.Returning)
            {
                targetZoom = previousZoom;
                if (cam.orthographicSize == targetZoom && transform.position == new Vector3(previousPos.x, previousPos.y, cam.transform.position.z))
                {
                    currentState = ZoomStates.None;
                    if (initiateQuest)
                        ApiController.Instance.GetQuestManager().InitiateQuestChecks();
                }
            }
        }
        else
        {
            float scrollData = Input.GetAxis("Mouse ScrollWheel");
            targetZoom -= scrollData * zoomFactor;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }
        cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
    }

    public void ZoomToObject(GameObject obj)
    {
        if (currentState != ZoomStates.Zooming)
        {
            previousPos = transform.position;
            previousZoom = cam.orthographicSize;
            zoomPos = obj.transform.position;
            currentState = ZoomStates.Zooming;
        }
    }

    public void ReturnFromZoom(bool initQuest)
    {
        initiateQuest = initQuest;
        zoomPos = previousPos;
        currentState = ZoomStates.Returning;
    }
}
