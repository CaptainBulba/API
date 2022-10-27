using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    private Grid grid;

    [SerializeField] private Tilemap ground;
    [SerializeField] private Tilemap walls;
    
    private DisplayCoordinates displayCoords;

    private List<Vector2> tilesPosition = new List<Vector2>();

    private void Start()
    {
        grid = GetComponent<Grid>();
        displayCoords = FindObjectOfType<DisplayCoordinates>();

        AddTilesPosition();
    }

    private void Update()
    {
        DisplayCoordinates();
    }

    private void AddTilesPosition()
    {
        foreach (var pos in ground.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector2 place = ground.CellToWorld(localPlace);
            if (ground.HasTile(localPlace) && !walls.HasTile(localPlace))
            {
                tilesPosition.Add(place);
            }
        }
    }

    public Vector2 GetClosestTile(GameObject player)
    {
        Vector2 playerCords = new Vector2(player.transform.position.x - 0.5f, player.transform.position.y - 0.5f);

        tilesPosition.Sort(delegate (Vector2 a, Vector2 b)
        {
            return Vector2.Distance(playerCords, a).CompareTo(Vector2.Distance(playerCords, b));
        });

        for (int i = 0; i < tilesPosition.Count; i++)
        {
            if (Vector2.Distance(tilesPosition[i], playerCords) > 0.01f)
            {
                Debug.Log(tilesPosition[i]);
                return new Vector2 (tilesPosition[i].x + 0.5f, tilesPosition[i].y + 0.5f);
            }
        }

        return player.GetComponent<Player>().GetStartPosition();
    }

    private void DisplayCoordinates()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = grid.WorldToCell(worldPoint);

        if (ground.HasTile(position) || walls.HasTile(position))
            displayCoords.ChangeCoords("Coordinates: " + position.x + ", " + position.y);
    }
}
