using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team14
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Bob))]
    public class Target : MonoBehaviour
    {
        public bool IsWanted = false;
        [SerializeField] private SpriteRenderer _headwearRenderer;
        [SerializeField] private SpriteRenderer _faceRenderer;
        [SerializeField] private Sprite[] _faceAnimation;

        private Bob _bob;
        public TargetGenerator Generator;

        private void Start()
        {
            _bob = GetComponent<Bob>();
            SetFaceSprite(_faceAnimation[0]);
        }

        public void SetFaceSprite(Sprite sprite)
        {
            _faceRenderer.sprite = sprite;
        }

        public void SetHeadwearSprite(Sprite sprite)
        {
            _headwearRenderer.sprite = sprite;
        }

        public void Generate(Sprite headwear)
        {
            SetHeadwearSprite(headwear);
        }

        private IEnumerator HitRoutine()
        {
            MinigameManager.Instance.PlaySound("Pie Splat");
            foreach (Sprite s in _faceAnimation) 
            {
                SetFaceSprite(s);
                yield return new WaitForSeconds(.2f);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Pie pie = collision.GetComponent<Pie>();   
            
            if (pie)
            {
                StartCoroutine(HitRoutine());
                _bob.Stop();

                if (IsWanted)
                {
                    PieHunterManager.Instance.Win();
                    pie.Stop();
                }
                else
                {
                    MinigameManager.Instance.PlaySound("Lose SFX");
                    Generator.GenerateTargets();
                    pie.Stop();
                }
            }
        }
    }
}
