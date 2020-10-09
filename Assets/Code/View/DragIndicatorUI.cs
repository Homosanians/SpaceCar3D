using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Jobs.LowLevel.Unsafe;
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

    private const float jopa = 414 / 2.83f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _StartMousePos = Input.mousePosition;

            float xPos = MathUtil.Map(Input.mousePosition.x, 0, Screen.width, -Screen.width / 2, Screen.width / 2);
            float yPos = MathUtil.Map(Input.mousePosition.y, 0, Screen.height, -Screen.height / 2, Screen.height / 2);
            startPosition = new Vector3(xPos / jopa, yPos / jopa, 0);
        }

        if (Input.GetMouseButton(0))
        {
            print(Input.mousePosition);

            float xPos = MathUtil.Map(Input.mousePosition.x, 0, Screen.width, -Screen.width / 2, Screen.width / 2);
            float yPos = MathUtil.Map(Input.mousePosition.y, 0, Screen.height, -Screen.height / 2, Screen.height / 2);
            Vector3 thisPosition = new Vector3(xPos / jopa, yPos / jopa, 0);

            RenderLine(startPosition, thisPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndLine();
        }
    }
}


-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------