using UnityEngine;
using System.Collections;

public class SpiderAI : EnemyAI, EnemyInterface {

    Animator animator;

    public float maxHealth = 100.0f;

    float currHealth;

    bool isDead = false;

    Rigidbody rigidBody;

    private bool canTakeDamage = true;

    private float damageWaitTime = 1.0f;

    GameObject enemyAttack;

    BoxCollider spider;

    BoxCollider collider;

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        spider = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();

        enemyAttack = GameObject.FindWithTag("EnemyAttack");
        collider = enemyAttack.GetComponent<BoxCollider>();
        //Disable the collider on the spider untill it attacks
        collider.isTrigger = true;

        currHealth = maxHealth;
        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update () {

        NavMeshHit hit;
        if (!base.NavMesh.Raycast(base.player.position, out hit))
        {
            // enemy can see the player!
            animator.SetTrigger("Chase");
            MoveEnemy(base.player.position); 
        }


        if (currHealth == 0.0f && !isDead)
            {
            Death();
            base.NavMesh.Stop();
            rigidBody.isKinematic = true;
            spider.isTrigger = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            collider.isTrigger = false;
            animator.SetTrigger("PlayerClose");
        }

        if (col.gameObject.CompareTag("Weapon") && canTakeDamage)
        {
            print("HIT");
            currHealth -= 25.0f;
            StartCoroutine(damageDelay());
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
        animator.SetTrigger("Die");
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

    public void AttackDone()
    {
        collider.isTrigger = true;
    }

    //While the player is in collision with the trigger, move him forward 
    //void OnTriggerStay(Collider other)
    //{
    //    float slideSpeed = 15.0f;
    //    var controller = other.GetComponent<CharacterController>();
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        controller.SimpleMove(GameObject.FindGameObjectWithTag("Player").transform.forward * slideSpeed);
    //    }
    //}
}
