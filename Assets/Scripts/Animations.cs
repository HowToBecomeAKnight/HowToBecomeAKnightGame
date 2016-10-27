using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour
{

    Animator anim;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))// 
        {
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
}