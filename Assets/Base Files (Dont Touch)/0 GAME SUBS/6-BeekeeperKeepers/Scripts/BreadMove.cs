using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeekeeperKeepers {
    public class BreadMove : MonoBehaviour {
        private const int EXPECTED_FRAMERATE = 30; // used for movement calibration

        [Range(0f, 3f)] public float moveSpeed; // used to control speed of player

        [Range(0f, 3f)] public float borderSize; // size of left and right scrreen borders
        private float leftBound; //used to keep player onscreen
        private float rightBound;
        private SpriteRenderer[] bees;

        // Start is called before the first frame update
        void Awake() {  
            SetBounds();
        }

        void Start() 
        {
            bees = GetComponentsInChildren<SpriteRenderer>();
        }

        // sets left and right boundaries for player movement
        private void SetBounds() {
            // gets camera borders
            Camera mainCam = FindObjectOfType<Camera>();
            Vector3 botLeft = mainCam.ScreenToWorldPoint(Vector3.zero);
            Vector3 botRight = mainCam.ScreenToWorldPoint(new Vector3(mainCam.pixelWidth, 0));

            // sets movement bounds based on camera border + player sprite
            SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
            float edgeSize = myRenderer.bounds.extents.x + borderSize;
            leftBound = botLeft.x + edgeSize;
            rightBound = botRight.x - edgeSize;
        }
        
        // Update is called once per frame
        void Update() {
            float moveInput = Input.GetAxis("Horizontal");
            if (moveInput != 0) {
                float newX = transform.position.x + moveInput * moveSpeed * Time.deltaTime * EXPECTED_FRAMERATE;
                transform.position = new Vector2(Mathf.Clamp(newX, leftBound, rightBound), transform.position.y); // thanks Unity

                // REVERSE THE BEES
                bool flip = moveInput < 0;
                foreach (SpriteRenderer bee in bees) 
                {
                    if (bee.gameObject != this.gameObject)
                    {
                        bee.flipX = flip;
                    }
                }
            }
        }
    }
}
