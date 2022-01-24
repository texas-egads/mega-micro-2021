using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalads
{
    public class RotateToTarget : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        private Vector2 direction;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform armHead;
        void Update()
        {
            //change direction to whatever you want it to
            direction = armHead.position;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            
            //Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position = Vector2.MoveTowards(transform.position, cursorPos, moveSpeed * Time.deltaTime);
        }
    }
}