using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObj : MonoBehaviour
{
    public bool isPlaced { get; private set; }
    public Vector3Int Size { get; private set; }
    private Vector3[] Vertices;

    private void GetColliderVertexPositionLocal()
    {
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        Vertices = new Vector3[4];
        Vertices[0] = box.center + new Vector3(-box.size.x, -box.size.y, -box.size.z) * 0.5f;
        Vertices[1] = box.center + new Vector3(box.size.x, -box.size.y, -box.size.z) * 0.5f;
        Vertices[2] = box.center + new Vector3(box.size.x, -box.size.y, box.size.z) * 0.5f;
        Vertices[3] = box.center + new Vector3(-box.size.x, -box.size.y, box.size.z) * 0.5f;
    }

    private void CalculateSizeCells()
    {
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(Vertices[i]);
            vertices[i] = BuildSystem.current.GridLayout.WorldToCell(worldPos);
        }
        //Size = new Vector3Int(x: Math.Abs((vertices[0] - vertices[1]).x), y: Math.Abs ((vertices[0] - vertices[3]).y), z: 1);
        Size = new Vector3Int(x:Math.Abs((vertices[0] - vertices[1]).x), y:Math.Abs((vertices[0] - vertices[3]).y),1);
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(Vertices[0]);
    }

    private void Start()
    {
        GetColliderVertexPositionLocal();
        CalculateSizeCells();
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0,90,0));
        Size = new Vector3Int(Size.y, Size.x, 1);

        Vector3[] vertices = new Vector3[Vertices.Length];
        for (int i = 0; i < Vertices.Length; i++)
        {
            vertices[i] = Vertices[(i + 1) % Vertices.Length];
        }
        Vertices = vertices;
    }
    public virtual void Place()
    {
        ObjDrag drag = gameObject.GetComponent<ObjDrag>();
        Destroy(drag);

        isPlaced = true;
    }
}
