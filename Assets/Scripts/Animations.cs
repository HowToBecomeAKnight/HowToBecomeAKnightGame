using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour
{

    Animator anim;

    BoxCollider collider;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        var weapon = GameObject.FindWithTag("Weapon");
        collider = weapon.GetComponent<BoxCollider>();
        //Disable the collider on the weapon untill the player swings it
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))// 
        {
            collider.isTrigger = false;
            anim.SetTrigger("Attack");
            Debug.Log("Attack");
        }
        //For Keyboard --------------------------
        if (Input.GetKey(KeyCode.W))//W
        {
            anim.SetFloat("Speed", 1);
        }
        else
            anim.SetFloat("Speed", 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            Debug.Log("Jump");
        }
    }

    public void AttackDone()
    {
        collider.isTrigger = true;
    }
}