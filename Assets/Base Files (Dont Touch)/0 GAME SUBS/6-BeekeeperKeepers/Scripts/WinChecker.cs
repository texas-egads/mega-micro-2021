using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeekeeperKeepers
{
    public class WinChecker : MonoBehaviour
    {
        ListManager lm;

        // Start is called before the first frame update
        void Start()
        {
            lm = FindObjectOfType<ListManager>();
        }

        public bool hasWon() 
        {
            foreach (Ingredient ing in GetComponentsInChildren<Ingredient>()) 
            {
                if (!lm.IsWinIngredient(ing.myType)) 
                {
                    return false;
                }
            }

            return lm.WinListEmpty();
        }
    }
}
