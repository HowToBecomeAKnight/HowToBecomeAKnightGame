using UnityEngine;
using System.Collections;

/*Makes the character a child of the moving platform so that it does not move in strange ways as the platform moves*/
public class CharacterPlatformLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Make player child of platform when they are on it
    void OnTriggerEnter(Collider other)
    {
        other.transform.parent = gameObject.transform;
    }

    //Player is no longer child of plaform when they get off it
    void OnTriggerExit(Collider other)
    {
            other.transform.parent = null;   
    }
}
