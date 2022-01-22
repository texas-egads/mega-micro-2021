using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
    {
    public class BossFollowing : StateMachineBehaviour
    {
        public float phaseDuration;

        private BossController bossCont = null;
        private Transform player = null;
        private Vector3 initialPos;
        private float speed;
        private float timeElapsed = 0;
        private bool isStageOver;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
            timeElapsed = 0;
            if (player == null)
            {
                bossCont = animator.gameObject.GetComponent<BossController>();
                player = bossCont.player;
                initialPos = bossCont.initialPosition;
                speed = bossCont.speed;
            }
            Debug.Log("entered following state");
            isStageOver = bossCont.isStageOver;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
        {
            Vector2 newPos = new Vector2();
            newPos.y = initialPos.y;
            newPos.x = Mathf.Lerp(animator.transform.position.x, player.position.x, speed * Time.deltaTime);
            animator.transform.position = newPos;

            timeElapsed += Time.deltaTime;
            if(timeElapsed >= phaseDuration && !isStageOver)
            {
                Debug.Log("Boss Following -> Boss Attacking");
                animator.Play("Attacking");
            }
        }
    }
}
