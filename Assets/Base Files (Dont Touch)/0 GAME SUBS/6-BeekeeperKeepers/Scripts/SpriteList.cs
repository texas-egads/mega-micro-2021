using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeekeeperKeepers {
    [CreateAssetMenu(menuName = "Sprite List")]
    public class SpriteList : ScriptableObject {
        public Sprite[] sprites;
    }
}