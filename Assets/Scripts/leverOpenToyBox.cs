using UnityEngine;
using System.Collections;

public class leverOpenToyBox : MonoBehaviour {

    Animation anim;
    Animation toyboxAnim;
    private bool colliding = false;
    public GameObject toybox;
    Collider lidCollider;
    public AudioSource openLever;

    public Light directional;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animation>();
        toyboxAnim = toybox.GetComponent<Animation>();

        //handles the toybox lid collider
        lidCollider = toybox.GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.F) && colliding)//if f is pressed and objects are colliding
        {
            //increases ambient lighting to simulate a lack of lighting in toybox
            RenderSettings.ambientIntensity = (float)1.72;
            RenderSettings.reflectionIntensity = (float)0.5;
            directional.intensity = 1;

            //handles animations
            anim.Play("pullLever");//play lever animation
            Debug.Log("play pull lever");
            toyboxAnim.Play("ArmatureAction");//move gate animation
            Debug.Log("open toybox");
            colliding = false;
            lidCollider.enabled = false;

            //lever audio
            openLever.PlayDelayed((float)(0.4));
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