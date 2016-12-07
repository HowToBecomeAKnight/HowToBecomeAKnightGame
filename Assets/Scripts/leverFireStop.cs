using UnityEngine;
using System.Collections;

public class leverFireStop : MonoBehaviour {

    Animation anim;
    Animation gateAnim;
    private bool colliding = false;
    public AudioSource fire;
 
    public GameObject fireTrap;
    Collider col;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animation>();
        col = fireTrap.GetComponent<BoxCollider>();
        fire = fireTrap.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.F) && colliding)//if f is pressed and objects are colliding
        {
            anim.Play("pullLever");//play lever animation
            Debug.Log("play pull lever");
     
            colliding = false;
            col.enabled = false;
            fireTrap.GetComponentInChildren<ParticleSystem>().Stop();
            fire.mute = true;
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

