using UnityEngine;

public class LosingFocus : QuestChecks
{
    private bool movedCamera = false;
    private bool movedZoom = false;

    private Camera cam;
    private Vector3 startCamPosition;
    private float startCamZoom;

    protected override void Start()
    {
        base.Start();
        SetCameraVariables();
    }

    private void SetCameraVariables()
    {
        cam = Camera.main;
        startCamPosition = cam.transform.position;
        startCamZoom = cam.orthographicSize;
    }

    private void Update()
    {
        if(!movedCamera)
            if (startCamPosition != cam.transform.position)
                movedCamera = true;
        
        if(!movedZoom)
            if (startCamZoom != cam.orthographicSize)
                movedZoom = true;

        if (movedCamera && movedZoom)
        {
            QuestCompleted();
            GetComponent<LosingFocus>().enabled = false;
        }
    }
}
