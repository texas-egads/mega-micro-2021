using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CriticalBass
{
    public class HeadMovement : MonoBehaviour
    {
        public bool go;

        GameObject player;
        GameObject head;

        public Transform headTransfom;

        CircleCollider2D col;

        public Vector3 targetPos;

        void Start()
        {
            go = false;

            player = GameObject.Find("Player");
            head = GameObject.Find("PlayerHead");
            col = GetComponentInChildren<CircleCollider2D>();

            head.GetComponent<SpriteRenderer>().enabled = false;

            headTransfom = gameObject.transform.GetChild(0);

            targetPos = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z) + player.transform.right * 7.5f;

            col.enabled = true;
            StartCoroutine(Snap());
        }

        IEnumerator Snap()
        {
            go = true;
            MinigameManager.Instance.PlaySound("SnapSound");
            yield return new WaitForSeconds(0.2f);
            col.enabled = false;
            yield return new WaitForSeconds(0.15f);
            go = false;
        }

        void Update()
        {
            if(MinigameManager.Instance.minigame.gameWin)
            {
                Destroy(this.gameObject);
            }
            if(go)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 40);
            }

            if(!go)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Time.deltaTime * 40);
            }

            if(!go && Vector3.Distance(player.transform.position, transform.position) < 1.5)
            {
                head.GetComponent<SpriteRenderer>().enabled = true;
                Destroy(this.gameObject);
            }
        }


    }
}