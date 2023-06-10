using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Plane plane;

    void OnMouseDown()
    {
        plane = new Plane(Camera.main.transform.forward, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            offset = transform.position - ray.GetPoint(enter);
            isDragging = true;
        }
    }

    void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            transform.position = ray.GetPoint(enter) + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}
