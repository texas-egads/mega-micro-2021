using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AveDisturbia{
    public class RandomizeObstacle : MonoBehaviour{
        //references
        [SerializeField] private Sprite[] obstacles;

        //variables
        private SpriteRenderer spriteRenderer;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            int randomIndex = Random.Range(0, obstacles.Length);
            spriteRenderer.sprite = obstacles[randomIndex];
        }
    }
}