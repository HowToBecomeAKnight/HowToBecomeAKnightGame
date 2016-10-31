using UnityEngine;
using System.Collections;

public class ZombunnyAI : EnemyAI
{
    Animator animator;

    float Health = 100.0f;

    bool isDead = false;

    float distanceToPlayer;

    // Use this for initialization
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 15)
        {
            animator.SetTrigger("Chase");
            MoveEnemy(player.position);
        }
    }

    void Death()
    {
        isDead = true;
        animator.SetTrigger("Die");
    }
}
