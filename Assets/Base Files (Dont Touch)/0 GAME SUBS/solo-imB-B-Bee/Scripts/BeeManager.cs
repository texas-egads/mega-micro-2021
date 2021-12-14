using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour
{
    public GameObject Bee1;
    public GameObject Bee2;
    public GameObject Bee3;

    Bee Bee1Script;
    Bee Bee2Script;
    Bee Bee3Script;

    private void Update()
    {
        Bee1Script = Bee1.GetComponent<Bee>();
        Bee2Script = Bee2.GetComponent<Bee>();
        Bee3Script = Bee3.GetComponent<Bee>();

        

        //if bees are all happy, MinigameManager.Instance.minigame.gameWin
        if (Bee1Script.GetHappy() && Bee2Script.GetHappy() && Bee3Script.GetHappy())
        {
            MinigameManager.Instance.minigame.gameWin = true;
        }
    }
}
