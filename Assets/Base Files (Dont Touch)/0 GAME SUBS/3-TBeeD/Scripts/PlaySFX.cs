using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBeeD
{
    public class PlaySFX : MonoBehaviour
    {
        [SerializeField] private string soundName = "";

        private AudioSource audioSource = null;
        private bool initedAudio;

        void Init()
        {
            audioSource = GetComponent<AudioSource>();
            int clipIndex = GetClipIndexFromMinigame(soundName);

            audioSource.clip = MinigameManager.Instance.minigame.sounds[clipIndex].clip;
            audioSource.volume = MinigameManager.Instance.minigame.sounds[clipIndex].volume;

            initedAudio = true;
        }

        public void PlaySoundEffect()
        {
            if (!initedAudio)
            {
                Init();
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        public void StopSoundEffect()
        {
            audioSource.Stop();
        }

        int GetClipIndexFromMinigame(string clipName)
        {
            int clip = 0;
            for (int i = 0; i < MinigameManager.Instance.minigame.sounds.Length; i++)
            {
                if (MinigameManager.Instance.minigame.sounds[i].soundName == clipName)
                {
                    clip = i;
                    break;
                }
            }
            return clip;
        }
    }
}