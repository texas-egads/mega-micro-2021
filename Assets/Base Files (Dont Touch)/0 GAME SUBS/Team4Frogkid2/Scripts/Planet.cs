using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FROGKID2
{

    public class Planet : MonoBehaviour
    {

        [SerializeField]
        Transform transform;

        [SerializeField]
        float rateScale = 1.001f;

        [SerializeField]
        float speed = 1.001f;

        [SerializeField]
        float amount = 1.001f;


        [SerializeField]
        float speedShake = 20f;

        [SerializeField]
        float amountShake = 1.0f;


        private bool shaking = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.localScale *= rateScale;
            if (shaking)
            {
                transform.position = new Vector3(Mathf.Sin(Time.time * speedShake) * amountShake, transform.position.y, transform.position.z);

            }
            else
            {
                transform.position = new Vector3(Mathf.Sin(Time.time * speed) * amount, transform.position.y, transform.position.z);

            }
        }

        public void SetShake()
        {
            shaking = true;
        }
    }
}