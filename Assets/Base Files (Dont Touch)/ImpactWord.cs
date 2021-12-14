using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImpactWord : MonoBehaviour
{
    public static ImpactWord instance;
    public Text impactText;
    private RectTransform textRect;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        impactText.enabled = false;
        textRect = impactText.GetComponent<RectTransform>();
    }

    public void HandleImpactText(string text)
    {
        StartCoroutine(ImpactText(text));
    }

    private IEnumerator ImpactText(string text)
    {
        textRect.localScale = new Vector3(2,2);
        impactText.enabled = true;
        impactText.text = text + "!";
        while (textRect.localScale.x > 1)
        {
            textRect.localScale -= new Vector3(.05f,.05f);
            yield return new WaitForSeconds(.01f);
        }
        yield return new WaitForSeconds(1);
        impactText.enabled = false;
    }
}
