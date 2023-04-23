using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private GameObject CellPrefab;
    [SerializeField] private int GridSizeX = 5;
    [SerializeField] private int GridSizeY = 5;
    [SerializeField] private int GridSizeZ = 5;
    [SerializeField] private int CellSize = 1;
    [SerializeField] private float CellSpace = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                for (int z = 0; z < GridSizeZ; z++)
                {
                    float PosX = x * (CellSize + CellSpace);
                    float PosY = y * (CellSize + CellSpace);
                    float PosZ = z * (CellSize + CellSpace);

                    GameObject cell = Instantiate(CellPrefab, new Vector3(PosX, PosY, PosZ), Quaternion.identity);

                    cell.transform.localScale = new Vector3(CellSize, CellSize, CellSize);

                    cell.transform.SetParent(transform);
                }
            }
        }
    }
}
