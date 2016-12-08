using UnityEngine;
using System.Collections;

public class displayHint : MonoBehaviour
{

    public Renderer rend;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Tab))//if text is invisible and tab is pressed, display the text
        {
            rend.enabled = true;
        }
        else //if text is visible and tab is pressed, hide the text
        {
            rend.enabled = false;
        }


    }
}