using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Grant
{
    public class Rock : MonoBehaviour
    {
        [SerializeField] private float speed = 0;
        private Rigidbody2D _rb2d;
        private float _rotationSpeed;

        private float _masterTimer = 3;
        private bool _soundPlayed = false;

        private void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _rotationSpeed = Random.Range(-270, 270);
            _rb2d.velocity = -transform.position * speed;
            transform.up = _rb2d.velocity.normalized;
        }

        private void Update()
        {
            transform.GetChild(0).Rotate(Vector3.back,_rotationSpeed * Time.deltaTime);
            _masterTimer -= Time.deltaTime;
            if (_masterTimer <= 2.5f && !_soundPlayed)
            {
                _soundPlayed = true;
                MinigameManager.Instance.PlaySound("Rock");
            }
            if(_masterTimer <= 0) Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Object 1"))
            {
                MinigameManager.Instance.PlaySound("Block");
                Destroy(gameObject);
                
            }
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Object 4")) Destroy(gameObject);   
        }
    }
}

