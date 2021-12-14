using UnityEngine;
using UnityEngine.Events;

namespace TBeeD
{
    public class Bread : MonoBehaviour
    {
        public bool CompletedFlip { get; set; }

        private Animator animator;
        [SerializeField] private UnityEvent onBreadFlip;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        internal void Flip()
        {
            animator.SetTrigger("Flip");
            onBreadFlip.Invoke();
        }

        internal void Unflip()
        {
            animator.SetTrigger("Unflip");
        }

        internal void OnCompleteFlip()
        {
            CompletedFlip = true;
        }
    }
}