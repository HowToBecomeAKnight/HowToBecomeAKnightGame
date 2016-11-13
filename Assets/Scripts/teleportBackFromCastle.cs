using UnityEngine;
using System.Collections;

public class teleportBackFromCastle : MonoBehaviour {

    public GameObject character;
    public GameObject teleportToHere;

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player hit BackFromCastle teleporter");

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("teleport to ToCastle portal");
            character.transform.position = teleportToHere.transform.position;
        }
    }
}
