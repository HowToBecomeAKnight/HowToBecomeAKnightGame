using UnityEngine;
using System.Collections;

public class leverOpenGate : MonoBehaviour {

    Animation anim;
    Animation gateAnim;
    private bool colliding = false;
    public GameObject gate;

    // Use this for initialization
    void Start() {

        anim = GetComponent<Animation>();
        gateAnim = gate.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.F) && colliding)//if f is pressed and objects are colliding
        {
            anim.Play("pullLever");//play lever animation
            Debug.Log("play pull lever");
            gateAnim.Play("move_gate");//move gate animation
            Debug.Log("move gate");
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

