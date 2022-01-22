using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Croakey_Dokey{
    public class FroggyController : MonoBehaviour
    {
        [SerializeField] GameObject move_prompt;
        [SerializeField] float prompt_timer;
        [SerializeField] GameObject radius;
        [SerializeField] Color[] radius_colors;
        private float[] radius_speeds = { 15f, 30f, 60f, 90f };
        private int frogs_in_personal_space;
        void FixedUpdate()
        {
            radius.transform.Rotate(Vector3.forward, radius_speeds[frogs_in_personal_space] * Time.fixedDeltaTime);
            if(prompt_timer > 0f){
                prompt_timer -= Time.fixedDeltaTime;
                if(prompt_timer <= 0f){
                    move_prompt.SetActive(false);
                }
            }
            if(!MinigameManager.Instance.minigame.gameWin){
                transform.position += 3f * Time.fixedDeltaTime * Vector3.Normalize(
                    new Vector2(
                        Input.GetAxisRaw("Horizontal"),
                        Input.GetAxisRaw("Vertical")
                    )
                );
            }
        }

        void OnTriggerEnter2D(Collider2D other){
            if(other.tag == "Object 1"){
                update_radius(true);
            }
        }

        void OnTriggerExit2D(Collider2D other){
            if(other.tag == "Object 1"){
                update_radius(false);
            }
            if(frogs_in_personal_space == 0){
                MinigameManager.Instance.minigame.gameWin = true;
                MinigameManager.Instance.PlaySound("win");
                MinigameManager.Instance.PlaySound("applause");
            }
        }

        private void update_radius(bool increment){
            radius.GetComponent<SpriteRenderer>().color = radius_colors[frogs_in_personal_space += increment ? 1 : -1];
        }
    }
}
