using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss Game")]
public class BossGame : ScriptableObject
{
    public bool gameWin;
    public bool gameOver;
    
    public SoundAsset[] sounds;
}
