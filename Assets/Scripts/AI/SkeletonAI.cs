using UnityEngine;
using System.Collections;

public class SkeletonAI : EnemyAI, EnemyInterface {

    #region Variables
    Animator animator;

    bool isDead = false;

    public float maxHealth = 100.0f;
    float currHealth;

    float distanceToPlayer;

    bool isAlive = false;

    private BoxCollider boxCollider;

    private Rigidbody rigidBody;

    private BoxCollider enemyAttack;

    private bool canTakeDamage = true;

    private float damageWaitTime = 1.0f;

    private bool sinkEnemy = false;
    #endregion

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
        enemyAttack = GameObject.FindGameObjectWithTag("EnemyAttack").GetComponent<BoxCollider>();
        enemyAttack.isTrigger = true;

        rigidBody.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
        this.GetComponent<NavMeshAgent>().enabled = false;
        currHealth = maxHealth;
        base.Start();
        base.NavMesh.Stop();
    }
	
	// Update is called once per frame
	protected override void Update () {
        distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        if (!isDead)
        {
            if (distanceToPlayer < 30 && distanceToPlayer > 15 && !isAlive)
            {
                animator.SetTrigger("Resurrect");
            }

            if (distanceToPlayer < 15 && isAlive && !PlayerInSight())
            {
                base.NavMesh.Resume();
                animator.SetTrigger("Chase");
                MoveEnemy(Player.position);
            }

            if (currHealth == 0.0f && isAlive)
            {
                Death();
            }
        }

        if (sinkEnemy)
        {
            transform.Translate(-Vector3.up * 1.8f * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
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

        base.NavMesh.Stop();
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        rigidBody.isKinematic = true;
        boxCollider.isTrigger = true;
        enemyAttack.isTrigger = true;
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

    IEnumerator disableObject()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }

    public void StartResurrection()
    {
    }

    public void ResurrectionDone()
    {
        isAlive = true;
        canTakeDamage = true;
        this.GetComponent<NavMeshAgent>().enabled = true;
        rigidBody.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;
        boxCollider.enabled = true;
    }

    public void StartDestory()
    {
        sinkEnemy = true;

        // After 1.5 seconds disable the enemy.
        StartCoroutine(disableObject());
    }

    public bool PlayerInSight()
    {
        NavMeshHit hit;
        return this.NavMesh.Raycast(Player.position, out hit);
    }

    public void AttackDone()
    {
        enemyAttack.isTrigger = true;
    }

    public void AttackStart()
    {
        enemyAttack.isTrigger = false;
    }
}
