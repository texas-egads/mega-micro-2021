using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(menuName = "Minigame")]
public class Minigame : ScriptableObject
{    
        public enum GameTime
        {
            Short,
            Long
        }
        //******* ADD THESE IN THE INSPECTOR *******//
        public GameTime gameTime;
        public AudioClip music;
        [Range(0, 1)] public float volume = 1;
        public SoundAsset[] sounds;

        //*****************************************//

        //******* UPDATE THIS WHEN THE PLAYER WINS *******//
        public bool gameWin;
        //***********************************************//
}
