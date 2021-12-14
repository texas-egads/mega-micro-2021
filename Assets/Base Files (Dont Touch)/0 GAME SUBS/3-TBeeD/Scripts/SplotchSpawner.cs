using UnityEngine;

namespace TBeeD
{
    public class SplotchSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject splotchPrefab;
        [SerializeField] private Sprite[] splotchPalette;
        [SerializeField] private int minSplotches;
        [SerializeField] private int maxSplotches;
        private BoxCollider2D boxCollider2D;

        void Awake()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            Spawn();
        }

        void Spawn()
        {
            int splotchCount = Random.Range(minSplotches, maxSplotches);
            FindObjectOfType<GameController>().TotalSplotches = splotchCount;

            for (int i = 0; i < splotchCount; i++)
            {
                float x = Random.Range(-boxCollider2D.size.x / 2f, boxCollider2D.size.x / 2f);
                float y = Random.Range(-boxCollider2D.size.y / 2f, boxCollider2D.size.y / 2f);
                var splotch = Instantiate(splotchPrefab, new Vector3(x, y, 0f), Quaternion.identity, transform);
                splotch.GetComponent<SpriteRenderer>().sprite = splotchPalette[Random.Range(0, splotchPalette.Length)];
            }
        }
    }
}