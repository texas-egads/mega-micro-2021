using System.Collections;
using UnityEngine;

namespace ToadallyAwesome {
    public class Frog : MonoBehaviour {

        [SerializeField]
        private float moveSpeed = 5.0f;

        [SerializeField]
        private float tongueActionTime = 0.5f;

        [SerializeField]
        private float tongueCooldownTime = 0.5f;

        private Rigidbody2D rb;
        private GameObject tongue;
        private bool bCanEat;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
            tongue = transform.GetChild(0).gameObject;
            tongue.SetActive(false);
            bCanEat = true;
            //mini game is always in winning state, only lose if you collide with asteroid
            MinigameManager.Instance.minigame.gameWin = true;
        }

        private void Update() {
           checkMovement();
           checkTongue();
        }

        private void checkMovement() {
            if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
                rb.velocity = new Vector2(0.0f, moveSpeed);
                tongue.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, moveSpeed);
            }
            else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
                rb.velocity = new Vector2(0.0f, -moveSpeed);
                tongue.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -moveSpeed);
            }
        }

        private void checkTongue() {
            if(Input.GetKeyDown(KeyCode.Space) && bCanEat) {
                StartCoroutine(tongueAction());
            }
        }

        private IEnumerator tongueAction() {
            bCanEat = false;
            MinigameManager.Instance.PlaySound("Pop");
            tongue.SetActive(true);
            yield return new WaitForSeconds(tongueActionTime);
            tongue.SetActive(false);
            yield return new WaitForSeconds(tongueCooldownTime);
            bCanEat = true;
        }
    }
}
