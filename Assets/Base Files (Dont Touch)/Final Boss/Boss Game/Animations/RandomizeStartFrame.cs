using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeStartFrame : MonoBehaviour
{
    
    public float cycleOffset;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetFloat("CycleOffset", cycleOffset);
    }
}
