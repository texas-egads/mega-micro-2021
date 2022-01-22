using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    public class MazeRoom : MonoBehaviour
    {
        public GameObject roomCover;
        public LayerMask doorLayer;
        public float doorCheckRayDistance;
        public bool hasBomb = false;

        public GameObject target;
        private Collider2D collider;
        
        public enum RoomType
        {
            Normal,
            Start,
            Bomb
        }

        public RoomType roomType;

        // Start is called before the first frame update
        void Start()
        {
            collider = GetComponent<Collider2D>();
            if (roomType == RoomType.Bomb)
            {
                Instantiate(target, transform);
            }
        }

        public bool CanMove(Vector2 moveDir)
        {
            collider.enabled = false;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, doorCheckRayDistance, doorLayer);
            if (hit.collider == null)
            {
                collider.enabled = true;
                return true;
            }
            collider.enabled = true;
            print(hit.collider.gameObject.name);
            return false;
        }

        public void HideRoom()
        {
            roomCover.SetActive(true);
        }
    }
}
