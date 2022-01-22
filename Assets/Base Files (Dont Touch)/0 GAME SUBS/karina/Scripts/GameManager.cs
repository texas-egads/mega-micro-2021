using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace karina
{
public class GameManager : MonoBehaviour
{
    private int correctClothesCount = 0;
    [SerializeField] private Animator frogAnim;

    public void correctChoice()
    {
        correctClothesCount++;

        if (correctClothesCount >= 3)
        {
            MinigameManager.Instance.minigame.gameWin = true;            
        }
        else MinigameManager.Instance.minigame.gameWin = false;
    }

    public IEnumerator endGame()
    {
        if (correctClothesCount >= 3)
        {
            frogAnim.Play("End Game");
            yield return new WaitForSeconds(.5f);
            MinigameManager.Instance.PlaySound("suck");
            yield return new WaitForSeconds(.5f);
            MinigameManager.Instance.PlaySound("win");
        }
        else
        {
            frogAnim.Play("End Game Lose");
            yield return new WaitForSeconds(.5f);
            MinigameManager.Instance.PlaySound("suck");
            yield return new WaitForSeconds(.5f);
            MinigameManager.Instance.PlaySound("explosion");
            MinigameManager.Instance.PlaySound("wrong");
        }        
    }
}
}