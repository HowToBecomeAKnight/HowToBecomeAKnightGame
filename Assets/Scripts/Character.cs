using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Character : MonoBehaviour {

    public Image HealthBar;

    private bool canTakeDamage = true;

    private float damageWaitTime = 0.7f;

    private GameObject player;

    public Transform checkPoint;

    private NavMeshAgent agent;

    public bool finishedLevel = false;

    public GameObject lever;
    public GameObject teleportToHere;

    // Use this for initialization
    void Start () {
        agent = GetComponentInChildren<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        checkPoint = GameObject.FindWithTag("StartSpawn").transform;
        HealthBar.fillAmount = 1.0f;

        gameObject.GetComponent<NavMeshAgent>().enabled = false;

    }
	
	// Update is called once per frame
	void Update () {

        if (finishedLevel)
        {
            lever.transform.position = teleportToHere.transform.position;
        }

        if (HealthBar.fillAmount == 0)
        {
            PlayerDead();
        }

        //When player completes a level reset the checkpoint to the start
        if(finishedLevel)
        {
            checkPoint = GameObject.FindWithTag("StartSpawn").transform;
        }
    }

    public void RemoveHealth(float amount)
    {
        //TODO: do some sort of animation
        HealthBar.fillAmount -= amount;
    }

    public void AddHealth(float amount)
    {
        //TODO: do some sort of animation
        HealthBar.fillAmount += amount;
    }

    public float GetHealth()
    {
        return HealthBar.fillAmount;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }

        if (col.gameObject.CompareTag("EnemyAttack") && canTakeDamage)
        {
            print("PLAYER HIT");
            RemoveHealth(.1f);
            StartCoroutine(damageDelay());
        }

        if(col.gameObject.CompareTag("Hazard") && canTakeDamage)
        {
            print("PLAYER HIT");
            RemoveHealth(.2f);
            StartCoroutine(damageDelay());
        }

        if (col.gameObject.CompareTag("Trap") && canTakeDamage)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            print("PLAYER HIT");
            RemoveHealth(.8f);
            StartCoroutine(damageDelay());
        }

        if (col.gameObject.CompareTag("OutOfBounds"))
        {
            print("OutOfBounds");
            PlayerDead();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Hazard") && canTakeDamage)
        {
            print("PLAYER HIT");
            RemoveHealth(.2f);
            StartCoroutine(damageDelay());
        }

        if (col.gameObject.CompareTag("Trap") && canTakeDamage)
        {
            print("PLAYER HIT");
            RemoveHealth(.8f);
            StartCoroutine(damageDelay());
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("CheckPoint"))
        {
            checkPoint = col.transform;
        }

        if (col.gameObject.CompareTag("Hazard") && canTakeDamage)
        {
            print("PLAYER HIT");
            RemoveHealth(.2f);
            StartCoroutine(damageDelay());
        }
    }

    private void PlayerDead()
    {
        player.transform.position = checkPoint.position;
        //Player is at full health again
        AddHealth(1.0f);
    }

    IEnumerator damageDelay()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageWaitTime);
        canTakeDamage = true;
    }
}
