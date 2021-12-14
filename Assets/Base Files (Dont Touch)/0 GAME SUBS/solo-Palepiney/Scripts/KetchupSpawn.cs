using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Palepiney
{
    public class KetchupSpawn : MonoBehaviour
    {
        public GameObject ketchup;
        private SpriteRenderer rend;
        private Sprite bottle, squeezed;

        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponent<SpriteRenderer>();
            bottle = Resources.Load<Sprite>("honey bottle");
            squeezed = Resources.Load<Sprite>("honey bottle squeezed");
            rend.sprite = bottle;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Space"))
            {
                rend.sprite = squeezed;
                Instantiate(ketchup, new Vector2(transform.position.x, transform.position.y - 1), Quaternion.identity);
            }
        }
    }
}
