using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace imjayk
{
public class LineRendererController : MonoBehaviour
{
    [SerializeField] List<Transform> nodes;
    private LineRenderer lr;
    private Transform[] points;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = nodes.Count;
    }

    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }
    
    private void Update()
    {
        UpdateLine();
    }

    public void UpdateLine()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }

        lr.SetPositions(nodes.ConvertAll(n => n.position - new Vector3(0, 0, 5)).ToArray());
    }

    public Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        return positions;
    }

    public float GetWidth()
    {
        return lr.startWidth;
    }
}

}
