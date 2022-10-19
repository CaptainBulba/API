using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    private Grid grid;
    [SerializeField] private Tilemap ground;
    [SerializeField] private Tilemap walls;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI cordText;

    private void Start()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = grid.WorldToCell(worldPoint);

        if (ground.HasTile(position) && walls.HasTile(position))
            cordText.text = "Coordinates: " + position.x + ", " + position.y;
    }
}
