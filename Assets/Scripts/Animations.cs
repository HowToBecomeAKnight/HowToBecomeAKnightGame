using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour
{

    Animator anim;
    private CharacterController controller;
    BoxCollider collider;
    private bool onGround;

    // Use this for initialization
    void Start()
    {
        //get animator on character
        anim = GetComponent<Animator>();
        //get controller on character
        controller = GetComponent<CharacterController>();

        var weapon = GameObject.FindWithTag("Weapon");
        collider = weapon.GetComponent<BoxCollider>();
        //Disable the collider on the weapon untill the player swings it
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

        //Left click attack, slice attack
        if (Input.GetMouseButtonDown(0)) 
        {
            collider.isTrigger = false;
            anim.SetTrigger("Slice Attack");
            Debug.Log("Slice Attack");
        }
        
        //right click stab attack
        else if (Input.GetMouseButtonDown(1))
        {
            collider.isTrigger = false;
            anim.SetTrigger("Stab Attack");
            Debug.Log("Stab Attack");
        }
       
        //Set speed to 1.0 when W is pressed
        if (Input.GetKey(KeyCode.W))//W
        {
            anim.SetFloat("Speed", 1);
        }
        //Set speed to -1 when S is pressed 
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetFloat("Speed", -1);
        }
        //When at 0, play Idle animation
        else
            anim.SetFloat("Speed", 0);

        //play the jump animation when space is pressed, only trigger when player is grounded
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround)
            {
                anim.SetTrigger("Jump");
                Debug.Log("Jump");
            }
        }

        //to check if character is grounded
        if (controller.isGrounded)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        //pull lever if standing still
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Pull Lever");
        }
        
    }

    public void AttackDone()
    {
        collider.isTrigger = true;
    }
}