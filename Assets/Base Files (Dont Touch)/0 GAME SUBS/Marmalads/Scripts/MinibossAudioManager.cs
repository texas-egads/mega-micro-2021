using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalads
{
    public class MinibossAudioManager : SingletonMonobehaviour<MinibossAudioManager>
    {
        //How to play audio--> AudioManager.Current.Instance.PlayMethod();

        [Header("Audio Sources")]

        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _playerAudioSource;
        [SerializeField] private AudioSource _groundEnemyAudioSource;
        [SerializeField] private AudioSource _bossAudioSource;
        [SerializeField] private AudioSource _miscAudioSource;

        [Header("Audio Clips")]

        [SerializeField] private AudioClip musicClip;
        [SerializeField] private AudioClip frogAttackClip;
        [SerializeField] private AudioClip frogGrappleClip;
        [SerializeField] private AudioClip frogHitClip;
        [SerializeField] private AudioClip enemyHitClip;
        [SerializeField] private AudioClip bossWarningClip;
        [SerializeField] private AudioClip bossAttackClip;
        [SerializeField] private AudioClip bossHitClip;
        [SerializeField] private AudioClip winClip;
        [SerializeField] private AudioClip loseClip;

        private void Start()
        {
            _musicAudioSource.clip = musicClip;
            _musicAudioSource.loop = true;
            _musicAudioSource.Play();
        }

        public void PlayFrogAttackSFX()
        {
            _playerAudioSource.pitch = Random.Range(.9f, 1.1f);
            _playerAudioSource.clip = frogAttackClip;
            _playerAudioSource.loop = false;

            _playerAudioSource.Play();
        }

        public void PlayFrogGrappleSFX()
        {
            _playerAudioSource.pitch = Random.Range(.9f, 1.1f);
            _playerAudioSource.clip = frogGrappleClip;
            _playerAudioSource.loop = false;

            _playerAudioSource.Play();
        }

        public void PlayFrogHitSFX()
        {
            _playerAudioSource.pitch = Random.Range(.9f, 1.1f);
            _playerAudioSource.clip = frogHitClip;
            _playerAudioSource.loop = false;

            _playerAudioSource.Play();
        }

        public void PlayGroundEnemyHitSFX()
        {
            _groundEnemyAudioSource.pitch = Random.Range(.9f, 1.1f);
            _groundEnemyAudioSource.clip = enemyHitClip;
            _groundEnemyAudioSource.loop = false;

            _groundEnemyAudioSource.Play();
        }

        public void PlayBossHitSFX()
        {
            _bossAudioSource.pitch = Random.Range(.9f, 1.1f);
            _bossAudioSource.clip = bossHitClip;
            _bossAudioSource.loop = false;

            _bossAudioSource.Play();
        }

        public void PlayBossWarningSFX()
        {
            _bossAudioSource.pitch = Random.Range(.9f, 1.1f);
            _bossAudioSource.clip = bossWarningClip;
            _bossAudioSource.loop = true;

            _bossAudioSource.Play();
        }

        public void PlayBossAttackSFX()
        {
            _bossAudioSource.pitch = Random.Range(.9f, 1.1f);
            _bossAudioSource.clip = bossAttackClip;
            _bossAudioSource.loop = false;

            _bossAudioSource.Play();
        }

        public void PlayWinSFX()
        {
            _miscAudioSource.pitch = 1.0f;
            _miscAudioSource.clip = winClip;
            _miscAudioSource.loop = false;

            _miscAudioSource.Play();
        }

        public void PlayLoseSFX()
        {
            _miscAudioSource.pitch = 1.0f;
            _miscAudioSource.clip = loseClip;
            _miscAudioSource.loop = false;

            _miscAudioSource.Play();
        }

        public void PlayExampleSFX(int exampleInt)
        {
            /*
            _goodAudioSource.pitch = Random.Range(1f, 1.5f);
            _goodAudioSource.clip = goodSFX[goodNumSFX];
            _goodAudioSource.loop = false;

            _goodAudioSource.Play();
            */
        }
    }
  }
