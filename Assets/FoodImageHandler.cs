using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FoodImageHandler : MonoBehaviour
{
    [SerializeField] private Image foodImage;
    [SerializeField] private Image revealImage;
    [SerializeField] private Animator animator;
    [Serializable] private struct Food
    {
        public Sprite unknown;
        public Sprite good;
        public Sprite bad;
    }

    [SerializeField] private Food[] food;
    private void Awake()
    {
        MainGameManager.OnMainStart += ShowFood;
        MainGameManager.Instance.NextGameWait += ShowMysteryFood;
    }

    private void ShowMysteryFood() { StartCoroutine(MysteryFoodHelper()); }
    private IEnumerator MysteryFoodHelper()
    {
        yield return new WaitForSeconds(MainGameManager.ShortTime/4 - .2f);
        foodImage.sprite = food[MainGameManager.Instance.currentFood].unknown;
        foodImage.enabled = true;
    }
    public void ShowFood(bool win) { StartCoroutine(FoodHelper(win)); }
    private IEnumerator FoodHelper(bool win)
    {
        revealImage.sprite = win ? food[MainGameManager.Instance.currentFood].good : food[MainGameManager.Instance.currentFood].bad;
        foodImage.sprite = food[MainGameManager.Instance.currentFood].unknown;
        foodImage.enabled = true;
        revealImage.enabled = true;
        animator.Play("food-reveal");
        int nextFood = Random.Range(0, food.Length);
        while (nextFood == MainGameManager.Instance.currentFood) nextFood = Random.Range(0, food.Length);
        MainGameManager.Instance.currentFood = nextFood;
        yield return new WaitForSeconds(MainGameManager.ShortTime/4);
        foodImage.enabled = false;
        revealImage.enabled = false;
    }
    private void OnDestroy()
    {
        MainGameManager.OnMainStart -= ShowFood;
        MainGameManager.Instance.NextGameWait -= ShowMysteryFood;
    }
}
