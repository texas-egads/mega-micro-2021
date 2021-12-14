using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public Image screenshot;
    public Text titleText;
    public Text roleText;

    public void UpdateFields(Sprite pic, string gameTitle, string bodyText)
    {
        screenshot.sprite = pic;
        titleText.text = gameTitle;
        roleText.text = bodyText;
    }
}
