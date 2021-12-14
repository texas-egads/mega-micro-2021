using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private const float Tick = .42857f;
    public IEnumerator GameTimer(int time)
    {
        while (time >= 0)
        {
            time -= 1;
            yield return new WaitForSeconds(Tick);
            
        }
        gameObject.SetActive(false);
    }
}
