using UnityEngine;
using System.Collections;

public class LoadDungeon1 : MonoBehaviour
{

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player hit portal1");

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Load dungeon1");
            Application.LoadLevel("dungeon1");
        }
    }
}
