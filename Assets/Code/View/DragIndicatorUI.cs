using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DragIndicatorUI : MonoBehaviour
{
    static public Vector3 startPos;
    Vector3 endPos;
    [SerializeField]
    private Camera camera;
    public LineRenderer lr;

    Vector3 camOldPos;
    Vector3 camNewPos;

    Vector3 camOffset;

    // Start is called before the first frame update
    void Start()
    {
        //camera = Camera.main;
        lr = GetComponent<LineRenderer>();
        camOldPos = camera.transform.position;
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

    private Vector2 _StartMousePos;

    // Update is called once per frame
    void Update()
    {
        camNewPos = camera.transform.position;

        camOffset = camNewPos - camOldPos;

        if (Input.GetMouseButtonDown(0))
        {
            _StartMousePos = Input.mousePosition;
        }



        if (Input.GetMouseButton(0))
        {
            Vector3 startPos = camera.ScreenToWorldPoint(_StartMousePos);
            Vector3 currentPoint = camera.ScreenToWorldPoint(Input.mousePosition);
            startPos = new Vector3(startPos.x, startPos.y, -2.5f);
            currentPoint = new Vector3(currentPoint.x, currentPoint.y, -2.5f);
            print(Input.mousePosition);
            RenderLine(startPos, currentPoint);
        }

        camOldPos = camera.transform.position;

        if (Input.GetMouseButtonUp(0))
        {
            EndLine();
        }
    }
}
