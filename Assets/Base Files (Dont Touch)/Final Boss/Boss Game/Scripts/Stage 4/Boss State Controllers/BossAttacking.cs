using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SecretPuddle
    {
    public class BossAttacking : StateMachineBehaviour
    {
        public float initialWindupDuration;
        public float windupDecreaseFactor = .5f;
        public float attackDuration;

        private BossController bossCont = null;
        private float speed;
        private Transform player;
        private Vector3 initialPos;
        private float attackingDistance;
        private float timeElapsed;

        // attacking rectangle components
        private GameObject attackingRect;
        private SpriteRenderer attackRectSprite;
        private float windupDuration;
        private float whiteAttackTime = 0.5f;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            timeElapsed = 0;
            if (bossCont == null)
            {
                bossCont = animator.gameObject.GetComponent<BossController>();
                speed = bossCont.speed;
                initialPos = bossCont.initialPosition;
                player = bossCont.player;
                attackingDistance = bossCont.attackingDistance;

                attackingRect = bossCont.attackingRectangle;
                attackRectSprite = attackingRect.GetComponent<SpriteRenderer>();
                windupDuration = initialWindupDuration;
            }

            if(bossCont.isStageOver)
            {
                animator.Play("Following");
            }

            bossCont.StartCoroutine(startAttacking(animator));
        }

        private IEnumerator startAttacking(Animator animator)
        {
            // make rectangle transparent 
            Color newColor = attackRectSprite.color;
            newColor.a = 0;
            attackRectSprite.color = newColor;
            attackingRect.transform.localScale = new Vector2(0, attackingRect.transform.localScale.y);

            while (timeElapsed < windupDuration)
            {
                timeElapsed += Time.deltaTime;
                float fractionCompleted = timeElapsed / windupDuration;

                // follow player
                Vector2 newPos = new Vector2();
                newPos.y = initialPos.y;
                newPos.x = Mathf.Lerp(animator.transform.position.x, player.position.x, speed * Time.deltaTime);
                animator.transform.position = newPos;

                // make rectangle become less transparent
                newColor.a = fractionCompleted;
                attackRectSprite.color = newColor;

                // make rectangle widen
                attackingRect.transform.localScale = new Vector2(fractionCompleted, attackingRect.transform.localScale.y);

                yield return null;
            }

            windupDuration = initialWindupDuration - windupDecreaseFactor;
            if (windupDuration <= 0)
                windupDuration = windupDecreaseFactor;

            // flash rectangle white for a short period of time
            attackingRect.transform.localScale = new Vector2(1, attackingRect.transform.localScale.y);
            attackRectSprite.color = Color.white;
            yield return new WaitForSeconds(whiteAttackTime);
            whiteAttackTime -= .1f;
            if (whiteAttackTime <= 0)
                whiteAttackTime = .1f;
            newColor.a = 0;
            attackRectSprite.color = newColor;

            // move boss across the screen
            BossGameManager.Instance.PlaySound("Charge");
            animator.transform.DOMoveY(animator.transform.position.y + attackingDistance, attackDuration);
            yield return new WaitForSeconds(attackDuration);
            
            // go to resetting state
            Debug.Log("Boss Attacking -> Resetting");
            animator.Play("Resetting");
        }
    }
}
