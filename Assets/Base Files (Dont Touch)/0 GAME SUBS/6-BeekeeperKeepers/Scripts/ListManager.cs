using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeekeeperKeepers
{


    public class ListManager : MonoBehaviour
    {
        public int NUM_INGREDIENTS_DROP;
        public int WIN_MINIMUM;
        public int WIN_MAXIMUM;

        List<IngredientType> left;
        public Queue<IngredientType> dropList;
        public List<IngredientType> winList;


        // Start is called before the first frame update
        void Awake()
        {
            // Insantiate the lists
            left = new List<IngredientType>();
            dropList = new Queue<IngredientType>();
            winList = new List<IngredientType>();

            // Create list of all ingredients
            foreach (IngredientType ing in IngredientType.GetValues(typeof(IngredientType)))
            {
                left.Add(ing);
            }

            // Add ingredients in drop order to drop and win lists
            while (dropList.Count < NUM_INGREDIENTS_DROP - 1 && left.Count > 1)
            {
                int randInt = Random.Range(0, left.Count - 1);
                dropList.Enqueue(left[randInt]);
                winList.Add(left[randInt]);
                left.RemoveAt(randInt);
            }

            // Add the bun to the end
            dropList.Enqueue(IngredientType.BUN);
            winList.Add(IngredientType.BUN);

/*            Debug.Log("DropList");
            foreach (Ingredients ing in dropList)
            {
                Debug.Log(ing);
            }*/

            // Removing elements in middle of winList to get final order of winning ingredients
            int numToWin = Random.Range(WIN_MINIMUM, WIN_MAXIMUM + 1);
            while (winList.Count > numToWin)
            {
                winList.Remove(winList[Random.Range(0, winList.Count - 1)]);
            }

            Debug.Log("WinList " + numToWin);
            foreach (IngredientType ing in winList)
            {
                Debug.Log(ing);
            }
        }

        

        public IngredientType NextDrop() {
            if (dropList.Count > 0)
                return dropList.Dequeue();
            return IngredientType.BUN; // default value just to be safe
        }

        public int GetDropCount() {
            return dropList.Count;
        }

        public bool IsWinIngredient(IngredientType ing)  
        {
            bool win = winList.Contains(ing);
            if (win) 
            {
                winList.Remove(ing);
            }
            return win;
        }

        public bool WinListEmpty() 
        {
            return winList.Count == 0;
        }
    }
}
