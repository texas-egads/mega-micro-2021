using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public float startDelay;
    public CreditPanel[] creditPanels;
    public RectTransform panelToParent;
    public Rigidbody2D moveRb;
    public float moveSpeed;
    public float speedMultiplier;
    public GameObject[] creditPanelTemplates;
    private RectTransform lastObj;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCreditPanels();
        StartCoroutine(StartScroll(startDelay));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.S))
        {
            SetScrollVelocity(moveSpeed * speedMultiplier);
        }
        //I know there's a bug here but idgaf
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.S))
        {
            SetScrollVelocity(moveSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScreen");
        }
        print(((RectTransform)lastObj.transform).position.y);
        if (((RectTransform)lastObj.transform).position.y > Screen.height)
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }
    private void GenerateCreditPanels()
    {
        for(int i = 0; i < creditPanels.Length; i++)
        {
            if(creditPanels[i].type == CreditPanel.CreditType.Solo)
            {
                GenerateSoloPanels(creditPanels[i]);
            }
            else
            {
                PanelController panelInst;
                if (i % 2 == 0)
                {
                    panelInst = Instantiate(creditPanelTemplates[0], panelToParent).GetComponent<PanelController>();
                }
                else
                {
                    panelInst = Instantiate(creditPanelTemplates[1], panelToParent).GetComponent<PanelController>();
                }
                var splitText = creditPanels[i].text.Split(new char[] { '\n' }, 2);
                panelInst.UpdateFields(creditPanels[i].screenshot[0], splitText[0], splitText[1]);
                panelToParent = (RectTransform)panelInst.transform;

            }
        }
        lastObj = (RectTransform)Instantiate(creditPanelTemplates[3], panelToParent).transform;
    }
    private void GenerateSoloPanels(CreditPanel panel)
    {
        panelToParent = (RectTransform)Instantiate(creditPanelTemplates[2], panelToParent).transform;
        var splitText = panel.text.Split('\n');
        for(int i = 1; i < splitText.Length; i++)
        {
            var lineSplit = splitText[i].Split('-');
            PanelController panelInst;
            if (i % 2 == 0)
            {
                panelInst = Instantiate(creditPanelTemplates[0], panelToParent).GetComponent<PanelController>();
            }
            else
            {
                panelInst = Instantiate(creditPanelTemplates[1], panelToParent).GetComponent<PanelController>();
            }
            panelInst.UpdateFields(panel.screenshot[i - 1], lineSplit[0], lineSplit[1]);
            panelToParent = (RectTransform)panelInst.transform;
        }
    }
    private IEnumerator StartScroll(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetScrollVelocity(moveSpeed);
    }
    private void SetScrollVelocity(float newSpeed)
    {
        var newVel = moveRb.velocity;
        newVel.y = newSpeed;
        moveRb.velocity = newVel;
    }
}
