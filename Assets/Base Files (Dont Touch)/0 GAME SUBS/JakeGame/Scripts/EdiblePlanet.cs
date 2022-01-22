using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace imjayk
{
public class EdiblePlanet : MonoBehaviour
{
    [SerializeField] private int initialVelocity = 3;
    [SerializeField] private GameObject playerPosition;
    [SerializeField] private GameObject planetParticles;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private CircleCollider2D cc;
    private CircleCollider2D child_cc;
    private bool planetHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        child_cc = GetComponentInChildren<CircleCollider2D>();
        InitialForce();
    }

    void Update()
    {
        if (planetHit)
        {
            cc.enabled = false;
        }
    }
    private void InitialForce()
    {
        int initialDirection = Random.Range(1, 4);
        switch (initialDirection)
        {
            case 4:
                rb.AddForce(Vector3.up * initialVelocity, ForceMode2D.Impulse);
                rb.AddForce(Vector3.left * initialVelocity, ForceMode2D.Impulse);
                break;
            case 3:
                rb.AddForce(Vector3.down * initialVelocity, ForceMode2D.Impulse);
                rb.AddForce(Vector3.left * initialVelocity, ForceMode2D.Impulse);
                break;
            case 2:
                rb.AddForce(Vector3.up * initialVelocity, ForceMode2D.Impulse);
                rb.AddForce(Vector3.right * initialVelocity, ForceMode2D.Impulse);
                break;
            case 1:
                rb.AddForce(Vector3.down * initialVelocity, ForceMode2D.Impulse);
                rb.AddForce(Vector3.right * initialVelocity, ForceMode2D.Impulse);
                break;
        }

        rb.angularVelocity = Random.Range(-100, 100);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            planetHit = true;
        }
        if (other.tag == "Object 1")
        {
            PlanetDeath();
        }
    }

    private void PlanetDeath()
    {
        Instantiate(planetParticles, playerPosition.transform);
        if (!playerPosition.GetComponent<PlayerPlanet>().planetHit)
        {
            playerPosition.GetComponent<PlayerPlanet>().planetHit = true;
        }
        StartCoroutine(PlayMunchSound());
        Destroy(gameObject);
    }

    private IEnumerator PlayMunchSound()
    {
        MinigameManager.Instance.PlaySound("planetMunch");
        MinigameManager.Instance.PlaySound("explosion");
        yield return new WaitForSeconds(1);
    }
}

}