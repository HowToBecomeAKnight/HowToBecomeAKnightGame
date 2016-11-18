using UnityEngine;
using System.Collections;

public class leverOpenFinalPortal : MonoBehaviour {

    Animation anim;
    Animation gateAnim;
    private bool colliding = false;
    public GameObject portal;
    public GameObject portalGlow;
    private MeshRenderer rend1;
    private MeshRenderer rend;//this script will make the invisible finish portal appear when it is pulled

    // Use this for initialization
    void Start()
    {
        //rend1 and rend are used to hide the portal itself, as well as the purple glowing center part.
        anim = GetComponent<Animation>();
        gateAnim = portal.GetComponent<Animation>();
        rend = portal.GetComponent<MeshRenderer>();
        rend.enabled = false;
        rend1 = portalGlow.GetComponent<MeshRenderer>();
        rend1.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.F) && colliding)//if f is pressed and objects are colliding
        {
            //make portal appear when lever is pulled
            anim.Play("pullLever");//play lever animation
            Debug.Log("play pull lever");
            rend.enabled = true;
            rend1.enabled = true;
            Debug.Log("make portal appear");
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
