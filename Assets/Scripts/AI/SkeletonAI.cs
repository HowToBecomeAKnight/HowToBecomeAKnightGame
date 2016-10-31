using UnityEngine;
using System.Collections;

public class SkeletonAI : EnemyAI {

    Animator animator;

    bool isDead = false;

    float Health = 100.0f;

    float distanceToPlayer;

    bool isAlive = false;

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        base.Start();

    }
	
	// Update is called once per frame
	protected override void Update () {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 30 &&  distanceToPlayer > 15 && !isAlive)
        {
            print("Alive");
            animator.SetTrigger("Resurrect");
            isAlive = true;
        }

        if(distanceToPlayer < 15 && distanceToPlayer > 5 && isAlive)
        {
            print("Chase");
            animator.SetTrigger("Chase");
            MoveEnemy(player.position);
        }

        if (distanceToPlayer < 5 && isAlive)
        {
            print("attack");
            animator.SetTrigger("Attack");
        }


    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
        }
    }

    void Death()
    {
        isDead = true;
        animator.SetTrigger("Die");
    }
}
