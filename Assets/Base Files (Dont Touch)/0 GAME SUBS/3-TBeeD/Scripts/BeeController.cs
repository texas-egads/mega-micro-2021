using UnityEngine;
using UnityEngine.Events;

namespace TBeeD
{
    public class BeeController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 0f;
        [SerializeField] private float dashForce = 0f;
        [SerializeField] private float dashDuration = 0f;
        [SerializeField] private AnimationCurve dashCurve = default;
        [SerializeField] private UnityEvent onBeeMove;
        [SerializeField] private UnityEvent onBeeDash;
        [SerializeField] private GameObject dashEffectPrefab = null;
        private GameController gameController;
        private float currentSpeed;
        private Rigidbody2D rb;
        private float horizontalAxis;
        private float verticalAxis;
        private Vector2 moveInput;
        private bool activatedDash;
        private float dashTimer = 0f;
        private ParticleSystem dashEffect;
        private Animator animator;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            gameController = FindObjectOfType<GameController>();
            currentSpeed = moveSpeed;
        }

        void Update()
        {
            HandleInput();
        }

        void FixedUpdate()
        {
            currentSpeed = horizontalAxis == 0 && verticalAxis == 0 ? 0 : moveSpeed;

            if (horizontalAxis == 0 && verticalAxis == 0)
            {
                currentSpeed = 0;
            }
            else
            {
                transform.up = rb.velocity.normalized;
            }

            rb.velocity = moveInput * currentSpeed;

            if (currentSpeed > 0)
            {
                if (!gameController.GameEnded)
                {
                    onBeeMove.Invoke();
                }
            }

            if (activatedDash)
            {
                Dash();
            }
        }

        void HandleInput()
        {
            Move();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                activatedDash = true;
                animator.Play("beeDash");

                if (!gameController.GameEnded)
                {
                    onBeeDash.Invoke();
                }
            }
        }

        void Move()
        {
            horizontalAxis = Input.GetAxisRaw("Horizontal");
            verticalAxis = Input.GetAxisRaw("Vertical");
            moveInput = new Vector2(horizontalAxis, verticalAxis).normalized;
        }

        void Dash()
        {
            if (dashEffect == null)
            {
                dashEffect = Instantiate(dashEffectPrefab, transform).GetComponent<ParticleSystem>();
                dashEffect.transform.SetParent(transform);
                dashEffect.Play();
                dashEffect.transform.SetParent(null);
            }

            dashTimer += Time.fixedDeltaTime;

            if (dashTimer >= dashDuration)
            {
                activatedDash = false;
                dashTimer = 0f;
                animator.Play("beeMoveRegular");

                if (dashEffect != null)
                {
                    Destroy(dashEffect.gameObject);
                }
            }

            rb.AddForce(transform.up * dashForce * dashCurve.Evaluate(dashTimer / dashDuration), ForceMode2D.Impulse);

            if (!gameController.GameEnded)
            {
                onBeeMove.Invoke();
            }
        }
    }
}
