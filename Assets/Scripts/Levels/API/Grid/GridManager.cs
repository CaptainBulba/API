using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    private Grid grid;
    [SerializeField] private Tilemap ground;
    [SerializeField] private GameObject player;

    void Start()
    {
        grid = GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        // save the camera as public field if you using not the main camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // get the collision point of the ray with the z = 0 plane
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = grid.WorldToCell(worldPoint);

        if (!ground.HasTile(position))
        {
            Debug.Log("No tile");
            return;
        }

        Debug.Log(position);

       


        player.transform.position = new Vector2 (position.x + 0.5f, position.y + 0.5f);


    }
}
