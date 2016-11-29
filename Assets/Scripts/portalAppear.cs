using UnityEngine;
using System.Collections;

public class portalAppear : MonoBehaviour {

    public GameObject player;
    public Renderer rend;
    bool finished;

	// Use this for initialization
	void Start () {

        rend = GetComponent<Renderer>();
        rend.enabled = false;
        finished = player.GetComponent<Character>().finishedLevel;
	}
	
	// Update is called once per frame
	void Update () {

        if (finished)
            rend.enabled = true;
	
	}
}
