using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SecretPuddle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RocketController : MonoBehaviour
    {
        [Header("Attack Parameters")]
        public GameObject bulletObj;
        public Transform [] bulletSpawnLocs;

        [Header("Movement Parameters")]
        [SerializeField] private float speed;
        public TrailRenderer trailRend;

        [Header("Death Parameters")]
        public ParticleSystem deathParticles;

        private Rigidbody2D rb;
        private bool isRightBulletSpawn;
        private bool readInput = true;
        private bool isAlive = true;
        private float loopTime = 0.0f;
        private CameraShake camShake;

        void Start()
        {
           rb = GetComponent<Rigidbody2D>(); 
           camShake = Camera.main.GetComponent<CameraShake>();
           Stage4.instance.gameLose.AddListener(die);
           Stage4.instance.stageOver.AddListener(stageOver);

            BossGameManager.Instance.PlaySound("Rocket");
            StartCoroutine(PlayRocketSound());
        }

        void Update()
        {   
            // handle attacking
            if (Input.GetKeyDown(KeyCode.Space) && readInput)
            {
                int spawnLoc = (isRightBulletSpawn) ? 0 : 1;
                GameObject obj = Instantiate(bulletObj, bulletSpawnLocs[spawnLoc].position, Quaternion.identity);
                obj.transform.parent = transform.parent;
                isRightBulletSpawn = !isRightBulletSpawn;
                BossGameManager.Instance.PlaySound("Shoot");
            }
        }

        void FixedUpdate() 
        {
            if (readInput)
            {
                // handle Movement
                float horizInput = Input.GetAxisRaw("Horizontal");
                float vertInput = Input.GetAxisRaw("Vertical");

                Vector2 newVelocity = new Vector2();
                newVelocity.x = horizInput;
                newVelocity.y = vertInput;
                newVelocity.Normalize();
                newVelocity *= speed;

                rb.velocity = newVelocity;
            }
        }

        public void stageOver()
        {
            readInput = false;
            rb.velocity = Vector2.zero;
            rb.GetComponent<Collider2D>().enabled = false;

            // start coroutine for final animation
            StartCoroutine(winCutscene());
        }

        private IEnumerator PlayRocketSound()
        {
            while (isAlive)
            {
                loopTime += Time.deltaTime;
                if (loopTime >= 0.857f)
                {
                    loopTime = 0.0f;
                    BossGameManager.Instance.PlaySound("Rocket");
                }
                yield return null;
            }
        }

        private IEnumerator winCutscene()
        {
            // move to center of screen
            float duration = 1f;
            Vector3 newPos = Vector3.zero;
            newPos.y += 1;
            transform.DOMove(newPos, duration);
            yield return new WaitForSeconds(duration);

            // pull back slightly
            duration = 2f;
            newPos.y -= 1.8f;
            transform.DOMove(newPos, duration);
            yield return new WaitForSeconds(duration);

            // ZOOOOOOOOOOOM
            duration = .2f;
            newPos.y = 7;
            transform.DOMove(newPos, duration);
            camShake.shakeCamera(.3f, .3f);
            yield return new WaitForSeconds(duration);

            // win stage
            yield return new WaitForSeconds(.5f);
            Camera.main.transform.position = camShake.originalPos;
            Stage4.instance.gameWon.Invoke();
        }

        public void die()
        {
            isAlive = false;
            camShake.shakeCamera(.2f, .5f);
            deathParticles.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            readInput = false;
            trailRend.emitting = false;
            BossGameManager.Instance.PlaySound("PlayerHit");
        }
    }
}

