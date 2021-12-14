using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Credit", menuName = "Credit")]
public class CreditPanel : ScriptableObject
{
    public enum CreditType
    {
        Team, Base, Solo
    }
    public CreditType type;
    [TextArea(30, 10)]
    public string text;
    public Sprite[] screenshot;
}
