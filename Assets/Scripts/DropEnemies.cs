using UnityEngine;
using System.Collections;

public class DropEnemies : MonoBehaviour {

    public GameObject spider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            print("DropEnemy");
            spider.GetComponent<Rigidbody>().useGravity = true;
            spider.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }
}
