using UnityEngine;
using System.Collections;

public class SpiderAI : EnemyAI {

    Animator animator;

    float Health = 100.0f;

    bool isDead = false;

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
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
    }

    void Death()
    {
        isDead = true;
    }
}
