using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadDungeon2 : MonoBehaviour {

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player hit portal2");

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Load dungeon2");
            SceneManager.LoadScene("dungeon2");
        }
    }
}
