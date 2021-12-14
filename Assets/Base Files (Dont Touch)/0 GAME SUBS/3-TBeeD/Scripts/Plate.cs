using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace TBeeD
{
    public class Plate : MonoBehaviour
    {
        [SerializeField] private Bread rightBread;
        [SerializeField] private float rightBreadOffsetX = 0f;
        [SerializeField] private float moveSpeed = 0f;
        [SerializeField] private float enterPositionX = 0f;
        [SerializeField] private float exitPositionX = 0f;

        [SerializeField] private UnityEvent onPlateMove;

        void Update()
        {
            if (rightBread.CompletedFlip)
            {
                StartCoroutine(OnExit());
            }
        }

        IEnumerator OnEnter()
        {
            onPlateMove.Invoke();
            rightBread.transform.localPosition = new Vector3(rightBreadOffsetX, rightBread.transform.localPosition.y, rightBread.transform.localPosition.z);

            while (transform.position.x >= 0f)
            {
                // transform.Translate(Vector2.left * moveSpeed);
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }

        IEnumerator OnExit()
        {
            onPlateMove.Invoke();
            rightBread.CompletedFlip = false;

            while (transform.position.x > exitPositionX)
            {
                // transform.Translate(Vector2.left);
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

                yield return null;
            }

            transform.position = new Vector3(enterPositionX, transform.position.y, transform.position.z);
            rightBread.Unflip();
            StartCoroutine(OnEnter());
        }
    }
}