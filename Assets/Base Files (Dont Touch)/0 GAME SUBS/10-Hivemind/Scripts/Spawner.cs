using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hivemind{
    public enum Ingredient_ID{
        Egg,
        Noodles,
        Chicken,
        Broth,
        GreenOnion
    }

    //it's called Spawner, but really it's more of a manager
    //I didn't call it manager so as not to confuse it with the MinigameManager
    public class Spawner : MonoBehaviour
    {
        public int next_ing;
        public Ingredient_ID[] ids_needed;
        public GameObject[] ingredients;
        public Vector2[] track_starts;

        public GameObject bowlProgressIndicator;
        public ParticleSystem partSys;
        public Sprite[] bowlSprites;

        private float time_elapsed = 0.0f;
        private float time_to_wait = 0.3f;

        void Start(){
            partSys.Stop();
        }

        // Update is called once per frame
        void Update()
        {
            //if we've waited long enough...
            if(time_elapsed > time_to_wait){
                //reset the timer
                time_elapsed = 0.0f;
                //choose a new amount of time to wait between .1 and .3 sec
                time_to_wait = Random.Range(10,30) / 100.0f;
                //choose a random ingredient (slightly weighted towards what we need next)
                int idToSpawn =
                    Random.Range(0,4) == 0 && next_ing < ids_needed.Length ?
                    (int)(ids_needed[next_ing]) : Random.Range(0,5);
                GameObject objToSpawn = ingredients[idToSpawn];
                //choose a random track to spawn on
                Vector2 trackToSpawnOn = track_starts[Random.Range(0,3)];
                //spawn it and play a sound
                Instantiate(objToSpawn, trackToSpawnOn, Quaternion.identity);
                MinigameManager.Instance.PlaySound("Pop");
            }
            time_elapsed += Time.deltaTime;
        }

        public void NextIng(){
            MinigameManager.Instance.PlaySound("Ding");
            bowlProgressIndicator.GetComponent<SpriteRenderer>().sprite = bowlSprites[next_ing];
            StartCoroutine(PuffSteam());
            next_ing++;
            if(next_ing == ids_needed.Length){
                MinigameManager.Instance.minigame.gameWin = true;
                MinigameManager.Instance.PlaySound("Applause");
            }
	    }

        IEnumerator PuffSteam(){
            partSys.Play();

            yield return new WaitForSeconds(0.3f);

            partSys.Stop();
        }
    }
}