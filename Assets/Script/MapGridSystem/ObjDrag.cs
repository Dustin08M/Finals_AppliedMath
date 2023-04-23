using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjDrag : MonoBehaviour
{
    private Vector3 Offset;

    private void OnMouseDown()
    {
        Offset = transform.position - BuildSystem.GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        Vector3 pos = BuildSystem.GetMouseWorldPosition() + Offset;
        transform.position = BuildSystem.current.SnapCoordinateToGrid(pos);
    }
}
