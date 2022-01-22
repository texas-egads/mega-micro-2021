using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ButtonPress : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr_A;
    [SerializeField] private SpriteRenderer sr_D;
    [SerializeField] private SpriteRenderer sr_left;
    [SerializeField] private SpriteRenderer sr_right;
    [SerializeField] private SpriteRenderer sr_space;
    [SerializeField] private Sprite[] sprite_A;
    [SerializeField] private Sprite[] sprite_D;
    [SerializeField] private Sprite[] sprite_left;
    [SerializeField] private Sprite[] sprite_right;
    [SerializeField] private Sprite[] sprite_space;
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A))
        {
            sr_A.sprite = sprite_A[0];
            sr_left.sprite = sprite_left[0];
        }
        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            sr_D.sprite = sprite_D[0];
            sr_right.sprite = sprite_right[0];
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            sr_space.sprite = sprite_space[0];
        }
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A))
        {
            sr_A.sprite = sprite_A[1];
            sr_left.sprite = sprite_left[1];
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            sr_D.sprite = sprite_D[1];
            sr_right.sprite = sprite_right[1];
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            sr_space.sprite = sprite_space[1];
        }
    }
}
