using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    private Grid grid;
    [SerializeField] private Tilemap ground;
    [SerializeField] private Tilemap walls;
    private DisplayCoordinates displayCoords;

    private void Start()
    {
        grid = GetComponent<Grid>();
        displayCoords = FindObjectOfType<DisplayCoordinates>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = grid.WorldToCell(worldPoint);

        if (ground.HasTile(position) || walls.HasTile(position))
            displayCoords.ChangeCoords("Coordinates: " + position.x + ", " + position.y);
    }
}
