using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    public float spawnTime = 3f;
    public GameObject enemy;
    public Transform[] spawnLocations;
    Object[] enemyList;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        enemyList = new Object[spawnLocations.Length];
    }

    void Spawn()
    {
        if (player.GetComponent<Character>().GetHealth() <= 0f)
        {
            return;
        }
        int spawnLocationIndex = Random.Range(0, spawnLocations.Length);
        if (enemyList[spawnLocationIndex] == null)
        {
            enemyList[spawnLocationIndex] = Instantiate(enemy, spawnLocations[spawnLocationIndex].position, spawnLocations[spawnLocationIndex].rotation);
        }

    }
}
