using UnityEngine;
using System.Collections;

public class leverOpen : MonoBehaviour {

    Animation anim;
    private bool colliding = false;
    public GameObject toHere;
    public GameObject obstacle;

    // Use this for initialization
    void Start() {

        anim = GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.F) && colliding)//if f is pressed and objects are colliding
        {
            anim.Play("pullLever");
            Debug.Log("play pull lever");
            colliding = false;
            obstacle.transform.position = toHere.transform.position;//move stone into place when lever is pulled
            Debug.Log("Stone has moved into place");
        }

    }

    void OnTriggerEnter(Collider other)//true when objects are colliding
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("colliding = true");
            colliding = true;
        }
    }

    void OnTriggerExit(Collider other)//false when objects arent 
    {
        Debug.Log("Colliding = false");
        colliding = false;
    }
}
