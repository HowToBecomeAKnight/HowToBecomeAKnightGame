using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadDungeon3 : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider other)
    {

        Debug.Log("Player hit portal3");

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Load dungeon3");
            SceneManager.LoadScene("dungeon3");
        }
    }
    
}
