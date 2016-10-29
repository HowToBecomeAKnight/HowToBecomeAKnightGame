using UnityEngine;
using System.Collections;

public class LoadForestHub : MonoBehaviour
{

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player hit portal");

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Load Foresthub");
            Application.LoadLevel("ForestHub");
        }
    }
}
