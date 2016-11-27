using UnityEngine;
using System.Collections;

public class HealthPotionLogic : MonoBehaviour {

    private bool colliding = false;

    private Character player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.G) && colliding)//if f is pressed and objects are colliding
        {
            player.AddHealth(0.5f);
            colliding = false;
            //Item picked up, becomes deactivated
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)//true when objects are colliding
    {
        if (other.gameObject.tag == "Player")
        {
            print("AtHealthPotion");
            colliding = true;
        }
    }

    void OnTriggerExit(Collider other)//false when objects arent 
    {
        colliding = false;
    }
}
