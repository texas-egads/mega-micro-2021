using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace imjayk
{
public class PlanetParticles : MonoBehaviour
{
    [SerializeField] private float particleTime;
    void Start() 
    {
        gameObject.transform.parent = null;
        StartCoroutine(DestroyParticles());
    }

    private IEnumerator DestroyParticles()
    {
        yield return new WaitForSeconds(particleTime);
        Destroy(gameObject);
    }
}

}