using UnityEngine;
using System.Collections;

public class ZombunnyAI : EnemyAI, EnemyInterface
{
    #region Variables
    Animator animator;

    public float maxHealth = 100.0f;

    float currHealth;

    bool isDead = false;

    float distanceToPlayer;

    CapsuleCollider capsuleCollider;

    Rigidbody rigidBody;

    private bool sinkEnemy = false;

    private bool canTakeDamage = true;

    private float damageWaitTime = 0.7f;
    #endregion

    // Use this for initialization
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidBody = GetComponent<Rigidbody>();
        currHealth = maxHealth;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        if (!isDead)
        {
            animator.SetTrigger("Chase");
            MoveEnemy(Player.position);

            if (currHealth == 0.0f)
            {
                Death();
            }
        }

        if (sinkEnemy)
        {
            transform.Translate(-Vector3.up * 1.8f * Time.deltaTime);
        }
    }

    void Death()
    {
        isDead = true;
        animator.SetTrigger("Die");

        base.NavMesh.Stop();
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        rigidBody.isKinematic = true;
        capsuleCollider.isTrigger = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Weapon") && canTakeDamage)
        {
            print("HIT");
            currHealth -= 50.0f;
            StartCoroutine(damageDelay());
        }
    }

    void StartDestroy()
    {
        sinkEnemy = true;

        // After 1.5 seconds destory the enemy.
        Destroy(gameObject, 1.5f);
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
