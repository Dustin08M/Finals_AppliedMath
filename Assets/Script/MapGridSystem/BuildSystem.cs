using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildSystem : MonoBehaviour
{
    public static BuildSystem current;
    public GridLayout GridLayout;
    private Grid _grid;
    [SerializeField] private Tilemap MainTileMap;
    [SerializeField] private TileBase _WhiteTile;
    [SerializeField] private GameObject _gameObjPrefab;
    [SerializeField] private GameObject _gameObjPrefab2;
    private PlaceableObj PlaceObj;

    void Awake()
    {
        current = this;
        _grid = GridLayout.gameObject.GetComponent<Grid>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse4))
        {
            InitializeObj(_gameObjPrefab);
        }

        else if (Input.GetKeyDown(KeyCode.Mouse5))
        {
            InitializeObj(_gameObjPrefab2);
        }
        if (!PlaceObj)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlaceObj.Rotate();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanBePlaced(PlaceObj))
            {
                PlaceObj.Place();
                Vector3Int start = GridLayout.WorldToCell(PlaceObj.GetStartPosition());
                TakeArea(start, PlaceObj.Size);
            }
            else
            {
                Destroy(PlaceObj.gameObject);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(PlaceObj.gameObject);
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rayCastHit))
        {
            return rayCastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = GridLayout.WorldToCell(position);
        position = _grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, z:0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }

    public void InitializeObj(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);
        GameObject obj = Instantiate(prefab,position,Quaternion.identity);
        PlaceObj = obj.GetComponent<PlaceableObj>();
        obj.AddComponent<ObjDrag>();
    }

    private bool CanBePlaced(PlaceableObj placeableObj)
    {
        BoundsInt area = new BoundsInt();
        area.position = GridLayout.WorldToCell(PlaceObj.GetStartPosition());
        area.size = placeableObj.Size;

        TileBase[] BaseArray = GetTilesBlock(area, MainTileMap);

        foreach (var b in BaseArray)
        {
            if (b == _WhiteTile)
            {
                return false;
            }
        }
        return true;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        MainTileMap.BoxFill(start, _WhiteTile, start.x, start.y, start.x + size.x,
                            start.y + size.y);
    }
}
