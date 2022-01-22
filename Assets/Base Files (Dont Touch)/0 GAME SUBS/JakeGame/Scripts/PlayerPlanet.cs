using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace imjayk
{
public class PlayerPlanet : MonoBehaviour
{
    [SerializeField] private int initialVelocity = 2;
    [SerializeField] private int angularVelocity = 30;
    [SerializeField] private float rotateSpeed = 4f;
    [SerializeField] private float _attackTime = .5f;
    [SerializeField] private GameObject tongueTarget;
    [SerializeField] private GameObject planetObject;
    [SerializeField] private Animator _animator;
    [SerializeField] private LineRendererController _LRC;
    [SerializeField] private SpringJoint2D[] ediblePlanetSprings;
    private Rigidbody2D rb;
    public bool planetHit = false;
    private bool soundEnded = true;
    private float rotationNum;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InitialForce();
        rb.angularVelocity = angularVelocity;
        rotationNum = 0;
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A))
        {
            rotationNum += rotateSpeed;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            rotationNum -= rotateSpeed;
        }
        if(Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(TongueAttack());
        }
        if(planetHit)
        {
            StartCoroutine(EatPlanet());
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationNum));
    }
    private IEnumerator TongueAttack()
    {
        _animator.SetTrigger("TongueAttack");
        if (!planetHit && soundEnded)
        {
            MinigameManager.Instance.PlaySound("tongueExtend");
            soundEnded = false;
            yield return new WaitForSeconds(_attackTime);
            soundEnded = true;
        }
        else
        {
            yield return new WaitForSeconds(_attackTime);
        }
    }

    private IEnumerator EatPlanet()
    {
        _animator.SetBool("PlanetHit", true);
        if(planetObject!=null)
        {
            tongueTarget.transform.position = planetObject.transform.position;
        }
        ediblePlanetSprings[0].enabled = true;
        MinigameManager.Instance.minigame.gameWin = true;
        yield return new WaitForSeconds(5);
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
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Object 2")
        {
            planetHit = true;
        }
    }
}

}