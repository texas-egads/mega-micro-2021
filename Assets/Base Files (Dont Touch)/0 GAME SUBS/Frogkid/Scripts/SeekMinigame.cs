using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace FROGKID
{
    public class SeekMinigame : MonoBehaviour
    {

        [SerializeField]
        private List<Planet> planets = null;

        [SerializeField]
        private List<int> planetSelector;

        int homePlanet;

        // Start is called before the first frame update
        void Start()
        {
            MinigameManager.Instance.minigame.gameWin = false;
            homePlanet = Random.Range(0, 3);

            for(int i = 0; i < 3; i++)
            {
                if(i != homePlanet)
                {
                    // select a sprite at random
                    int index = Random.Range(0, planetSelector.Count);
                    int p = planetSelector[index];
                    planetSelector.RemoveAt(index);
                    planets[i].setPlanetNum(p);
                } else
                {
                    // set home planet sprite
                    planets[i].setPlanetNum(0);
                }
            }

            MinigameManager.Instance.PlaySound("drum");

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void doWin()
        {
            if (!MinigameManager.Instance.minigame.gameWin)
            {
                MinigameManager.Instance.minigame.gameWin = true;
                MinigameManager.Instance.PlaySound("win");
            }
        }

        public void doLose()
        {
            MinigameManager.Instance.PlaySound("lose");
        }
    }
}
