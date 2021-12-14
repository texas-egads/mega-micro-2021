using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum orderType { drink, fries, borgar }
namespace karina
{
    public class OrderManager : MonoBehaviour
    {
        public static int order;
        private int orderNumber;

        public List<Sprite> orderSprites;
        public orderType myStage;
        SpriteRenderer render;

        public static string customerOrder;

        void Start()
        {
            render = GetComponent<SpriteRenderer>();
            render.sprite = orderSprites[(int)myStage];

            var orderNumber = Random.Range(0, 3);
            //print(orderNumber);

            if (orderNumber == 0)
            {
                myStage = orderType.drink;
                render.sprite = orderSprites[(int)myStage];

                customerOrder = "drink";
            }
            if (orderNumber == 1)
            {
                myStage = orderType.fries;
                render.sprite = orderSprites[(int)myStage];

                customerOrder = "fries";
            }
            if (orderNumber == 2)
            {
                myStage = orderType.borgar;
                render.sprite = orderSprites[(int)myStage];

                customerOrder = "borgar";
            }
        }
    }
}
