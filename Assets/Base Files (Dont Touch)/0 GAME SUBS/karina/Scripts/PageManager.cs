using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace karina
{
public class PageManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> pageList;
    [SerializeField] private List<GameObject> glowList;

    [SerializeField] private GameManager GameManager;

    public int currentPage = 0;

    public void nextPage()
    {
        currentPage++;
        if (currentPage == 0)
        {
            pageList[0].SetActive(true);
            pageList[1].SetActive(false);
            pageList[2].SetActive(false);
            glowList[0].SetActive(true);
        }
        else if (currentPage == 1)
        {
            pageList[0].SetActive(false);
            pageList[1].SetActive(true);
            pageList[2].SetActive(false);
            glowList[1].SetActive(true);
        }
        else if (currentPage == 2)
        {
            pageList[0].SetActive(false);
            pageList[1].SetActive(false);
            pageList[2].SetActive(true);
            glowList[2].SetActive(true);
        }
        else if (currentPage >= 2)
        {
            StartCoroutine(GameManager.endGame());
        }
    }


}
}