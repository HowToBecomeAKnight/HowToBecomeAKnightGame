using UnityEngine;
using System.Collections;

public class displayLeverText : MonoBehaviour
{
    public Renderer rend;
    private bool colliding = false;
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (colliding)
        {
            rend.enabled = true;//when willy collides with lever make text appear
        }
        else
            rend.enabled = false;//when he walks away make the text disapear
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

