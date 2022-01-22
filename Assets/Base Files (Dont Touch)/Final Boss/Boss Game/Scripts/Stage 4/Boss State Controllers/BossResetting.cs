using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SecretPuddle
{
    public class BossResetting : StateMachineBehaviour
    {
        public float resetDuration;

        private BossController bossCont = null;
        private float speed;
        private Vector3 initialPos;
        private float timeElapsed;
        private Transform player;
        private Collider2D bossCollider;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
        {
            timeElapsed = 0;
            if (bossCont == null)
            {
                bossCont = animator.gameObject.GetComponent<BossController>();
                speed = bossCont.speed;
                initialPos = bossCont.initialPosition;
                player = bossCont.player;
                bossCollider = animator.gameObject.GetComponent<Collider2D>();
            }

            if(bossCont.isStageOver)
            {
                animator.Play("Following");
            }

            bossCont.StartCoroutine(startResetting(animator));
        }

        private IEnumerator startResetting(Animator animator)
        {
            timeElapsed = 0;
            bossCollider.enabled = false;
            yield return new WaitForSeconds(resetDuration);

            // move boss below screen
            Vector3 teleportPos = initialPos;
            teleportPos.y -= 2;
            animator.transform.position = teleportPos;

            // move boss back into frame
            float duration = .5f;
            while (Mathf.Abs(animator.transform.position.y - initialPos.y) > .05f)
            {
                // follow player horizontally while moving into frame vertically
                // from the bottom of the screen
                Vector2 newPos = new Vector2();
                newPos.x = Mathf.Lerp(animator.transform.position.x, player.position.x, speed * Time.deltaTime);
                newPos.y = Mathf.Lerp(teleportPos.y, initialPos.y, timeElapsed / duration);

                animator.transform.position = newPos;

                timeElapsed += Time.deltaTime;
                
                yield return null;
            }
            animator.transform.position = new Vector2(animator.transform.position.x, initialPos.y);

            // return to following phase
            bossCollider.enabled = true;
            animator.Play("Following");
        }
    }
}
