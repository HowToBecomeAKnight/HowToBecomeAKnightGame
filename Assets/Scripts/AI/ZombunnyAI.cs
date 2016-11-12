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
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 15 && !isDead)
        {
            animator.SetTrigger("Chase");
            MoveEnemy(player.position);
        }

        if(currHealth == 0.0f && !isDead)
        {
            Death();
            base.NavMesh.Stop();
            rigidBody.isKinematic = true;
            capsuleCollider.isTrigger = true;

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

    public float getCurrHealth()
    {
        return currHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }
}
