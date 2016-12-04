using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadForestHub : MonoBehaviour
{
    GameObject player;

    GameObject companion;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        companion = GameObject.FindGameObjectWithTag("Companion");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player hit portal");

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Load Foresthub");
            SceneManager.LoadScene("ForestHub");
        }
    }
}
