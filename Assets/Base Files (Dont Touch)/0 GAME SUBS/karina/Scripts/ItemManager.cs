using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace karina
{
public class ItemManager : MonoBehaviour
{
    [SerializeField] private bool isCorrect;

    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private Color startColor;
    [SerializeField] private Vector3 startScale;

    [SerializeField] private PageManager PageManager;
    [SerializeField] private GameManager GameManager;

    [SerializeField] private Animator frogAnim;
    [SerializeField] private Animator bulbAnim;

    private bool pressedSpace;
    private bool isSelected;

    private float scaleUp = 1.2f;

    private string currentSelectedItemNum;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            pressedSpace = true;
        }
        else pressedSpace = false;

        if (isSelected == true)
        {
            sprite.color = new Color (1, 1, 1, 1);
            transform.localScale = new Vector3 (scaleUp, scaleUp, scaleUp);

            if (pressedSpace == true && PageManager.currentPage <= 2)
            {
                currentSelectedItemNum = gameObject.tag.Replace("Object ", "");
                frogAnim.SetTrigger(currentSelectedItemNum);
                chooseItem();
            }
        }
        else
        {
            sprite.color = startColor;
            transform.localScale = startScale;
        }        
    }

    void chooseItem()
    {
        MinigameManager.Instance.PlaySound("select");
        if (isCorrect == true)
        {
            bulbAnim.SetTrigger("isCorrect");
            GameManager.correctChoice();
        }
        else bulbAnim.SetTrigger("isIncorrect");
        PageManager.nextPage();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {        
        if (other.collider.tag == "Player")
        {
            MinigameManager.Instance.PlaySound("switch");
        }
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {        
        if (other.collider.tag == "Player")
        {
            isSelected = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {        
        if (other.collider.tag == "Player")
        {
            isSelected = false;
        }
    }
}
}