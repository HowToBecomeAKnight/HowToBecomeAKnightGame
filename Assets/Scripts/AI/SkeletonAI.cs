using UnityEngine;
using System.Collections;

public class SkeletonAI : EnemyAI, EnemyInterface {

    Animator animator;

    bool isDead = false;

    public float maxHealth = 100.0f;
    float currHealth;

    float distanceToPlayer;

    bool isAlive = false;

    CapsuleCollider capsuleCollider;

    Rigidbody rigidBody;

    private bool canTakeDamage = true;

    private float damageWaitTime = 1.0f;

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidBody = GetComponent<Rigidbody>();
        currHealth = maxHealth;
        base.Start();
        base.NavMesh.Stop();
    }
	
	// Update is called once per frame
	protected override void Update () {
        distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        if (distanceToPlayer < 30 &&  distanceToPlayer > 15 && !isAlive && !isDead)
        {
            print("Alive");
            animator.SetTrigger("Resurrect");
            isAlive = true;
        }

        if(distanceToPlayer < 15 && distanceToPlayer > 5 && isAlive && !isDead)
        {
            base.NavMesh.Resume();
            print("Chase");
            animator.SetTrigger("Chase");
            MoveEnemy(Player.position);
        }

        if (distanceToPlayer < 5 && isAlive && !isDead)
        {
            print("attack");
            animator.SetTrigger("Attack");
        }

        if (currHealth == 0.0f && !isDead && isAlive)
        {
            Death();
            base.NavMesh.Stop();
            rigidBody.isKinematic = true;
            capsuleCollider.isTrigger = true;

        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
        }

        if (col.gameObject.CompareTag("Weapon"))
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
