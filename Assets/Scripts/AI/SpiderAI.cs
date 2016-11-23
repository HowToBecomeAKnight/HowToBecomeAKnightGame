using UnityEngine;
using System.Collections;

public class SpiderAI : EnemyAI, EnemyInterface {

    Animator animator;

    public float maxHealth = 100.0f;
    float currHealth;
    bool isDead = false;

    BoxCollider boxCollider;

    Rigidbody rigidBody;

    private bool canTakeDamage = true;

    private float damageWaitTime = 1.0f;

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
        currHealth = maxHealth;
        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();

        if (currHealth == 0.0f && !isDead)
        {
            Death();
            base.NavMesh.Stop();
            rigidBody.isKinematic = true;
            boxCollider.isTrigger = true;

        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("PlayerClose");
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Chase");
        }

        if (col.gameObject.CompareTag("Weapon") && canTakeDamage)
        {
            print("HIT");
            currHealth -= 25.0f;
            StartCoroutine(damageDelay());
        }
    }

    void Death()
    {
        isDead = true;
        animator.SetTrigger("Die");
    }

    public float getCurrHealth()
    {
        return currHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    IEnumerator damageDelay()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageWaitTime);
        canTakeDamage = true;
    }
}
