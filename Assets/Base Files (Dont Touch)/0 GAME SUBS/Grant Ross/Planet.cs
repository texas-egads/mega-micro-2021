using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grant
{
    public class Planet : MonoBehaviour
    {
        private Animator _anim;

        private bool _dead;
        private void Start()
        {
            _anim = GetComponent<Animator>();
            MinigameManager.Instance.minigame.gameWin = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_dead) return;
            _dead = true;
            _anim.SetBool("Dead", true);
            MinigameManager.Instance.minigame.gameWin = false;
            MinigameManager.Instance.PlaySound("Boom");
            StartCoroutine(Die());
        }

        private IEnumerator Die()
        {
            yield return new WaitForSeconds(1.6f);
            Destroy(gameObject);
        }
    }
}