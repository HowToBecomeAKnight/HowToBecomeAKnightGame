using UnityEngine;
using System.Collections;

public class SpiderAI : EnemyAI {

    Animator animator;

    float Health = 100.0f;

    bool isDead = false;

    BoxCollider boxCollider;

    Rigidbody rigidBody;

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();

        if (Health == 0.0f && !isDead)
        {
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

        if (col.gameObject.CompareTag("Weapon"))
        {
            print("HIT");
            Health = 0.0f;
        }
    }

    void Death()
    {
        isDead = true;
    }
}
