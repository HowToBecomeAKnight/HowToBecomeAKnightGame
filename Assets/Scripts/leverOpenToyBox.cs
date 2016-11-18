using UnityEngine;
using System.Collections;

public class leverOpenToyBox : MonoBehaviour {

    Animation anim;
    Animation toyboxAnim;
    private bool colliding = false;
    public GameObject toybox;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animation>();
        toyboxAnim = toybox.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.F) && colliding)//if f is pressed and objects are colliding
        {
            anim.Play("pullLever");//play lever animation
            Debug.Log("play pull lever");
            toyboxAnim.Play("ArmatureAction");//move gate animation
            Debug.Log("open toybox");
            colliding = false;
        }

    }

    void OnTriggerEnter(Collider other)//true when objects are colliding
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("colliding = true");
            colliding = true;
        }
    }

    void OnTriggerExit(Collider other)//false when objects arent 
    {
        // Debug.Log("Colliding = false");
        colliding = false;
    }
}