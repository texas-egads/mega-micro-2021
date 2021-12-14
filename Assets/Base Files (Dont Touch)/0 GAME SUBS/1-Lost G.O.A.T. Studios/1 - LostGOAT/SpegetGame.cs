using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LostGOAT
{
    public class SpegetGame : MonoBehaviour
    {
        [SerializeField] private GameObject[] Keys;

        private bool isHere;

        private bool lose = false;

        private string currentNote;

        private string buttonPressed;

        private bool good = false;

        private bool once = false;

        [SerializeField] private GameObject screen;

        [SerializeField] private GameObject[] Goodwords;
        [SerializeField] private GameObject[] Badwords;

        private GameObject currentMeat;
        [SerializeField] GameObject noMeat;

        [SerializeField] private GameObject dance;
        [SerializeField] private GameObject sad;
        
        private void Awake()
        {
            for (int x = -900; x < 450; x += 450)
            {
                GameObject meatball = Instantiate(Keys[Random.Range(0, Keys.Length)], new Vector2(x-450, 31.30005f), Quaternion.identity);
                
                meatball.transform.SetParent(screen.transform, false);
            }
            sad.SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GameTimer());
            currentMeat = noMeat;
        }

        // Update is called once per frame
        void Update()
        {
            //if(Input.anyKeyDown) print (Input.inputString );
            if (isHere == false && Input.anyKeyDown == true)
            {
                lose = true;
                GameObject word = Badwords[Random.Range(0, Badwords.Length)];
                word.GetComponent<Animator>().SetTrigger("Go");
                //Debug.Log("Pressed no note");
            }
            else if (isHere == true)
            {
                if (Input.anyKeyDown == true && Input.inputString != "")
                {
                    buttonPressed = Input.inputString;
                }
                if (buttonPressed != currentNote && once == false && Input.inputString != "")   /// if they pressed th wrong button
                {
                    once = true;
                    good = false;
                    lose = true;
                    GameObject word = Badwords[Random.Range(0, Badwords.Length)];
                    word.GetComponent<Animator>().SetTrigger("Go");
                    //Debug.Log("Wrong");
                }
                else if (once == false && currentNote == buttonPressed)
                {
                    once = true;
                    good = true;
                    GameObject word = Goodwords[Random.Range(0, Goodwords.Length)];
                    word.GetComponent<Animator>().SetTrigger("Go");
                    currentMeat.GetComponent<RythmMoves>().IStabbedAMeatyBall = true;
                    StartCoroutine(Sounds());
                    StartCoroutine(SteveSad());
                    //Debug.Log("Correct");
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Ground")
            {
                isHere = true;
                currentNote = collision.gameObject.name.Substring(0, 1);
                currentMeat = collision.gameObject;
                //Debug.Log(currentMeat.name);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Ground")
            {
                print("exit");
                once = false;
                isHere = false;
                currentNote = "";
                buttonPressed = "";
                if (good == false)
                {
                    lose = true;
                    GameObject word = Badwords[Random.Range(0, Badwords.Length)];
                    word.GetComponent<Animator>().SetTrigger("Go");
                    //Debug.Log("Missed");
                }
                //good = false;
                currentMeat = noMeat;
            }
        }
        private IEnumerator GameTimer()
        {
            yield return new WaitForSeconds(6.81f);
            if (lose == false)
            {
                MinigameManager.Instance.minigame.gameWin = true;
            }
        }
        private IEnumerator Sounds()
        {
            MinigameManager.Instance.PlaySound("MeatSquish");
            yield return new WaitForSeconds(.5f);
            MinigameManager.Instance.PlaySound("Munch");
        }
        private IEnumerator SteveSad()
        {
            dance.SetActive(false);
            sad.SetActive(true);
            yield return new WaitForSeconds(.7f);
            sad.SetActive(false);
            dance.SetActive(true);
        }
    }
}
