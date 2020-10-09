using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DragIndicatorUI : MonoBehaviour
{
    [SerializeField]
    public LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        //camera = Camera.main;
        lr = GetComponent<LineRenderer>();
    }

    void RenderLine(Vector3 startPoint, Vector3 endPoint)
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

    private Vector2 _StartMousePos = Vector3.zero;
    private Vector3 startPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _StartMousePos = Input.mousePosition;

            startPosition = new Vector3(
                MathUtil.Map(Input.mousePosition.x, 0, Screen.width, -2.8f, 2.8f),
                MathUtil.Map(Input.mousePosition.y, 0, Screen.height, -6.1f, 6.1f),
                0);
        }

        if (Input.GetMouseButton(0))
        {
            print(Input.mousePosition);

            Vector3 thisPosition = new Vector3(
                MathUtil.Map(Input.mousePosition.x, 0, Screen.width, -2.8f, 2.8f),
                MathUtil.Map(Input.mousePosition.y, 0, Screen.height, -6.1f, 6.1f),
                0);

            RenderLine(startPosition, thisPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndLine();
        }
    }
}
