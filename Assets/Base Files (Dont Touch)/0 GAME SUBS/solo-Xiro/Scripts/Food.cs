using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XIRO
{
    [CreateAssetMenu(menuName = "Food")]
    public class Food : ScriptableObject
    {
        public string type;
        public Sprite sprite;
    }
}
