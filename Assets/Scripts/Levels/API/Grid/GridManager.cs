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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = grid.WorldToCell(worldPoint);

        if (!ground.HasTile(position))
        {
            Debug.Log("No tile");
            return;
        }

        Debug.Log(position);
    }
}
