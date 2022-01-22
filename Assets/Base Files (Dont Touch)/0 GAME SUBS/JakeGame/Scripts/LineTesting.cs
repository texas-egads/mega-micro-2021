using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace imjayk
{
public class LineTesting : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineRendererController line;
    void Start()
    {
        line.SetUpLine(points);
    }
}

}