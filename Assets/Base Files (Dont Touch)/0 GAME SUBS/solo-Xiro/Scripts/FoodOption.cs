using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XIRO
{
    public class FoodOption : MonoBehaviour
    {
        public Food food;
        public SpriteRenderer renderer;
        public float timer;
        public Transform start, end;

        private float duration = 0f;

        // Start is called before the first frame update
        void Start()
        {
            renderer.sprite = food.sprite;
        }

        // Update is called once per frame
        void Update()
        {
            duration += Time.deltaTime;

            float newX = Mathf.Lerp(start.position.x, end.position.x, duration / timer);
            gameObject.transform.position = new Vector3(newX, gameObject.transform.position.y, gameObject.transform.position.z);

            if (duration >= timer)
                Destroy(gameObject);
        }
    }
}
