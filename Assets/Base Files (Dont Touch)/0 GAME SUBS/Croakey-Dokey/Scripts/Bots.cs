using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

namespace Croakey_Dokey{
    public class Bots : MonoBehaviour
    {
        [SerializeField] GameObject[] bots;
        float timer = 0f;

        private void Start()
        {
            MinigameManager.Instance.minigame.gameWin = false;
            for(int i = 0; i < bots.Length; i++){
                GameObject bot = bots[i];
                float bound = 1.25f + 0.75f * i; //bounds for each bot: 1.25, 2.0, 2.75
                bot.transform.position += new Vector3(Random.Range(-bound, bound), Random.Range(-bound, bound), 0f);
            }
        }

        private void FixedUpdate()
        {
            if(!MinigameManager.Instance.minigame.gameWin){
                timer += Time.fixedDeltaTime;
                if(timer > 3f/7f){  // 3/7 == 60/140, or the number of seconds per beat of music -- in other words, do this on each beat
                    timer = 0f;
                    Transform t = bots[Random.Range(0,3)].transform;
                    t.position =
                        new Vector3(
                            Mathf.Clamp(t.position.x + Random.Range(-1f,1f),-3f,3f),
                            Mathf.Clamp(t.position.y + Random.Range(-1f,1f),-3f,3f),
                            0f
                        );
                    MinigameManager.Instance.PlaySound("pop");
                }
            }
        }
    }
}
