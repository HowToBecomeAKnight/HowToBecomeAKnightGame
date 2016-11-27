using UnityEngine;
using System.Collections;

public class CheckPointHandler : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Character character = col.gameObject.GetComponent<Character>();
            character.checkPoint = gameObject.transform;
            print("checkpoint reached");
        }
    }
}
