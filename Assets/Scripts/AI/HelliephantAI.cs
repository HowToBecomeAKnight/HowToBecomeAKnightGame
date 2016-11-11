using UnityEngine;
using System.Collections;

public class HelliephantAI : EnemyAI
{
    Animator animator;

    float Health = 100.0f;

    bool isDead = false;

    float distanceToPlayer;

    CapsuleCollider capsuleCollider;

    Rigidbody rigidBody;

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidBody = GetComponent<Rigidbody>();
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

        if (Health == 0.0f && !isDead)
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
            Health = 0.0f;
        }
    }
}
