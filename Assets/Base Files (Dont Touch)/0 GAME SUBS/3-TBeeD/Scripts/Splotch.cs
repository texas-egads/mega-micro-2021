using UnityEngine;

namespace TBeeD
{
    public class Splotch : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                FindObjectOfType<GameController>().SplotchesCovered++;
                Destroy(gameObject);
            }
        }
    }
}