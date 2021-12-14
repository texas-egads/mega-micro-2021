using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Team14 
{
    public class PieHunterManager : MonoBehaviour
    {
        private static PieHunterManager _instance;
        public static PieHunterManager Instance => _instance ? _instance : _instance = FindObjectOfType<PieHunterManager>();

        [SerializeField] private Text WinText;
        public Pie Pie;

        private void Awake()
        {
            WinText.gameObject.SetActive(false);
        }

        public void Win()
        {
            WinText.gameObject.SetActive(true);
            Pie.PieState = Pie.State.Disabled;
            MinigameManager.Instance.PlaySound("Win SFX");
            MinigameManager.Instance.minigame.gameWin = true;
        }
    }
}
