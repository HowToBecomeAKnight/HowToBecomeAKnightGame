using UnityEngine;
using System.Collections;

public class ZombunnyAI : EnemyAI, EnemyInterface
{
    Animator animator;

    public float maxHealth = 100.0f;
    float currHealth;
    bool isDead = false;

    float distanceToPlayer;

    CapsuleCollider capsuleCollider;

    Rigidbody rigidBody;

    private bool sinkEnemy = false;

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

        if (distanceToPlayer < 15 && !isDead)
        {
            animator.SetTrigger("Chase");
            MoveEnemy(Player.position);
        }
        if (currHealth == 0.0f && !isDead)
        {
            Death();
            base.NavMesh.Stop();
            rigidBody.isKinematic = true;
            capsuleCollider.isTrigger = true;

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
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Weapon"))
        {
            print("HIT");
            currHealth -= 25.0f;
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
}
