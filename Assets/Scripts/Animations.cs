using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour
{

    Animator anim;
    private CharacterController controller;
    BoxCollider collider;
    private bool onGround;
    private bool weaponEqipped = true;
    GameObject weapon;

    public AudioSource swing;
    public AudioSource walk;
    public AudioSource run;
    public AudioSource jump;


    // Use this for initialization
    void Start()
    {
        //get animator on character
        anim = GetComponent<Animator>();
        //get controller on character
        controller = GetComponent<CharacterController>();

        weapon = GameObject.FindWithTag("Weapon");
        collider = weapon.GetComponent<BoxCollider>();
        //Disable the collider on the weapon untill the player swings it
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

        //Left click attack, slice attack
        if (Input.GetMouseButtonDown(0) && weaponEqipped) 
        {
            swing.PlayDelayed((float)(0.3));
            collider.isTrigger = false;
            anim.SetTrigger("Slice Attack");
            Debug.Log("Slice Attack");

        }
        
        //right click stab attack
        else if (Input.GetMouseButtonDown(1) && weaponEqipped)
        {
            swing.PlayDelayed((float)(0.8));
            collider.isTrigger = false;
            anim.SetTrigger("Stab Attack");
            Debug.Log("Stab Attack");
        }
       
        //Set speed to 1.0 when W is pressed
        if (Input.GetKey(KeyCode.W))//W
        {
            if (!walk.isPlaying && !Input.GetKeyDown(KeyCode.Space))
                walk.Play();

            anim.SetFloat("Speed", 1);
        }
        //Set speed to -1 when S is pressed 
        else if (Input.GetKey(KeyCode.S))
        {
            if (!walk.isPlaying)
                walk.Play();

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
                jump.PlayDelayed(1);
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

        if(Input.GetKeyDown(KeyCode.G))
        {
            anim.SetTrigger("Pick Up");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(weaponEqipped)
            {
                anim.SetBool("Equipped Weapon", false);
                weapon.SetActive(false);
                weaponEqipped = false;
            }
            else
            {
                anim.SetBool("Equipped Weapon", true);
                weapon.SetActive(true);
                weaponEqipped = true;
            }
        }
        
    }

    public void AttackDone()
    {
        collider.isTrigger = true;
    }
}