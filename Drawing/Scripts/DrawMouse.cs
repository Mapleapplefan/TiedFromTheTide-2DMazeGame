
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawMouse : MonoBehaviour 
{
    public LineRenderer line;
    private Vector3 previousPosition;
    private bool isDrawing = false;

    [SerializeField]
    private float minDistance = 0.1f;
    [SerializeField, Range(0.1f, 2f)]
    private float width = 0.1f;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startColor = Color.red;
        line.positionCount = 0;
        previousPosition = transform.position;
        line.startWidth = line.endWidth = width;
    }

    public void StartLine(Vector2 position)
    {
        isDrawing = true;
        line.positionCount = 1;
        line.SetPosition(0, position);
        previousPosition = position;
    }

    public void UpdateLine()
    {
        if (isDrawing)
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;

            if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, currentPosition);
                previousPosition = currentPosition;
            }
        }
    }

    //when mouse is released
    public void EndLine()
    {
        isDrawing = false;
    }

    public void ClearLine()
    {
        line.positionCount = 0;
        isDrawing = false;
    }
}