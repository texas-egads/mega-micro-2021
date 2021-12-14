using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XIRO
{
    public class MyMGManager : MonoBehaviour
    {
        public Food[] foods;
        private Food targetFood;
        private Food selectedFood;
        public Image targetImg, selectedImg;
        public Transform conveyorStart, conveyorEnd;
        public GameObject foodOptionPrefab;
        public FoodGrabPoint foodPoint;
        private bool isWon;

        // Start is called before the first frame update
        void Start()
        {
            selectedFood = null;
            isWon = false;
            int target = Random.Range(0, foods.Length);
            targetFood = foods[target];
            targetImg.sprite = targetFood.sprite;
            StartCoroutine(SpawnFoodOptions());
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Space"))
            {
                if (foodPoint.canGetFood && !isWon)
                {
                    selectedFood = foodPoint.foodToGrab;
                    selectedImg.sprite = selectedFood.sprite;

                    if (selectedFood.type == targetFood.type)
                        MinigameManager.Instance.PlaySound("win");
                    else
                        MinigameManager.Instance.PlaySound("wrong");
                }
            }

            isWon = (selectedFood != null) && (selectedFood.type == targetFood.type);
            MinigameManager.Instance.minigame.gameWin = isWon;
        }

        private IEnumerator SpawnFoodOptions()
        {
            List<Food> availableFoods = new List<Food>();
            for (int f = 0; f < foods.Length; f++)
            {
                availableFoods.Add(foods[f]);
            }

            yield return new WaitForSeconds(1.5f);

            while (availableFoods.Count > 0)
            {
                int randomFood = Random.Range(0, availableFoods.Count);
                Food nextFood = availableFoods[randomFood];
                availableFoods.RemoveAt(randomFood);

                GameObject newFood = Instantiate(foodOptionPrefab);
                newFood.transform.position = conveyorStart.position;
                FoodOption newFoodOption = newFood.GetComponent<FoodOption>();
                newFoodOption.food = nextFood;
                newFoodOption.start = conveyorStart;
                newFoodOption.end = conveyorEnd;
                yield return new WaitForSeconds(1.5f);
            }
        }
    }
}
