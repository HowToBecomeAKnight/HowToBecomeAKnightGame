using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Character : MonoBehaviour {

    public Image HealthBar;

    private bool canTakeDamage = true;

    private float damageWaitTime = 1.0f;

    // Use this for initialization
    void Start () {
	    HealthBar.fillAmount = 1f;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void RemoveHealth(int amount)
    {
        //TODO: do some sort of animation
        HealthBar.fillAmount -= amount;
    }

    public void AddHealth(int amount)
    {
        //TODO: do some sort of animation
        HealthBar.fillAmount += amount;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("EnemyAttack") && canTakeDamage)
        {
            print("PLAYER HIT");
            RemoveHealth(10);
            StartCoroutine(damageDelay());
        }
    }

    IEnumerator damageDelay()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageWaitTime);
        canTakeDamage = true;
    }
}
