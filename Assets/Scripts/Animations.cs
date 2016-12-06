using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour
{

    Animator anim;
    private CharacterController controller;
    BoxCollider collider;
    Character character;
    private bool onGround;
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
        character = GetComponent<Character>();
        //Disable the collider on the weapon until the player swings it
        getWeaponCollider().isTrigger = true;
    }

    BoxCollider getWeaponCollider()
    {
        return character.GetCurrentWeapon.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        //Left click attack, slice attack
        if (Input.GetMouseButtonDown(0) && character.GetCurrentWeapon.activeSelf) 
        {
            swing.PlayDelayed((float)(0.3));
            getWeaponCollider().isTrigger = false;
            anim.SetTrigger("Slice Attack");
            Debug.Log("Slice Attack");

        }
        
        //right click stab attack
        else if (Input.GetMouseButtonDown(1) && character.GetCurrentWeapon.activeSelf)
        {
            swing.PlayDelayed((float)(0.8));
            getWeaponCollider().isTrigger = false;
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
            if (character.GetCurrentWeapon.activeSelf)
            {
                anim.SetBool("Equipped Weapon", false);
                character.GetCurrentWeapon.SetActive(false);
            }
            else
            {
                anim.SetBool("Equipped Weapon", true);
                character.GetCurrentWeapon.SetActive(true);
            }
        }
        
    }

    public void AttackDone()
    {
        getWeaponCollider().isTrigger = true;
    }
}