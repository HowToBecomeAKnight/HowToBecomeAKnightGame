using UnityEngine;
using System.Collections;

public class SkeletonAI : EnemyAI {

    Animator animator;

    bool isDead = false;

    float Health = 100.0f;

    float distanceToPlayer;

    bool isAlive = false;

    CapsuleCollider capsuleCollider;

    Rigidbody rigidBody;

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidBody = GetComponent<Rigidbody>();
        base.Start();
        base.NavMesh.Stop();
    }
	
	// Update is called once per frame
	protected override void Update () {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

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
            MoveEnemy(player.position);
        }

        if (distanceToPlayer < 5 && isAlive && !isDead)
        {
            print("attack");
            animator.SetTrigger("Attack");
        }

        if (Health == 0.0f && !isDead && isAlive)
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
            Health = 0.0f;
        }
    }

    void Death()
    {
        isDead = true;
        animator.SetTrigger("Die");
    }
}
