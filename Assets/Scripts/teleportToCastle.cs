using UnityEngine;
using System.Collections;

public class teleportToCastle : MonoBehaviour {

    public GameObject character;
    public GameObject teleportToHere;

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player hit ToCastle teleporter");

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("teleport to castle");
            character.transform.position = teleportToHere.transform.position;
        }
    }
}
