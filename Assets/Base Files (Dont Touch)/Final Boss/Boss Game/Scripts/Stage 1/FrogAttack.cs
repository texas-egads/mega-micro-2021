using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecretPuddle
{
    public class FrogAttack : MonoBehaviour
    {
        public GameObject bulletObj;
        public Transform bulletSpawn;

        private BossGameManager bossMan;
        private bool canAttack = true;

        private void Start() 
        {
            Stage1.instance.gameLost.AddListener(stopAttack);
        }

        public void stopAttack()
        {
            canAttack = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) && canAttack)
            {
                BossGameManager.Instance.PlaySound("Shoot");
                GameObject obj = Instantiate(bulletObj, bulletSpawn.transform.position, Quaternion.identity);
                obj.transform.parent = transform.parent;
            }
        }
    }
}

