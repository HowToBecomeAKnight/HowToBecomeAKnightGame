using UnityEngine;
using System.Collections;

public class leverOpenGate : MonoBehaviour {

    Animation anim;
    Animation gateAnim;
    private bool colliding = false;
    public GameObject metalGate;
    public AudioSource openGate;
    public AudioSource openLever;

    // Use this for initialization
    void Start() {

        anim = GetComponent<Animation>();
        metalGate = GameObject.FindGameObjectWithTag("Gate");
        gateAnim = metalGate.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.F) && colliding)//if f is pressed and objects are colliding
        {
            openLever.PlayDelayed((float)(0.7));
            openGate.Play();
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

