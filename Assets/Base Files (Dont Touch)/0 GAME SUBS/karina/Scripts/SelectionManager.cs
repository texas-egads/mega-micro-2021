using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace karina
{
public class SelectionManager : MonoBehaviour
{
    [SerializeField] private float gridSize;

    private int placeOnGrid = 0;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            moveUp();
        }
        if (Input.GetKeyDown("s"))
        {
            moveDown();
        }
    }

    void moveUp()
    {
        if (placeOnGrid > 0)
        {
            placeOnGrid = placeOnGrid - 1;
            transform.position += new Vector3 (0, gridSize, 0);
        }
    }

    void moveDown()
    {
        if (placeOnGrid < 2)
        {
            placeOnGrid = placeOnGrid + 1;
            transform.position -= new Vector3 (0, gridSize, 0);
        }
    }

}
}
