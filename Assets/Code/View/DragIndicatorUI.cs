using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DragIndicatorUI : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lr;

    private Vector2 startTouchPosition = Vector3.zero;

    private Vector3 startWorldPosition;

    private const float jopa = 146.2379631140851966704749469561f;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void RenderLine(Vector3 startPoint, Vector3 endPoint)
    {
        lr.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint;

        lr.SetPositions(points);
    }

    public void EndLine()
    {
        lr.positionCount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;

            float xPos = MathUtil.Map(Input.mousePosition.x, 0, Screen.width, -Screen.width / 2, Screen.width / 2);
            float yPos = MathUtil.Map(Input.mousePosition.y, 0, Screen.height, -Screen.height / 2, Screen.height / 2);
            startWorldPosition = new Vector3(xPos / jopa, yPos / jopa, 0);
        }

        if (Input.GetMouseButton(0))
        {
            print(Input.mousePosition);

            float xPos = MathUtil.Map(Input.mousePosition.x, 0, Screen.width, -Screen.width / 2, Screen.width / 2);
            float yPos = MathUtil.Map(Input.mousePosition.y, 0, Screen.height, -Screen.height / 2, Screen.height / 2);
            Vector3 thisPosition = new Vector3(xPos / jopa, yPos / jopa, 0);

            RenderLine(startWorldPosition, thisPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndLine();
        }
    }
}