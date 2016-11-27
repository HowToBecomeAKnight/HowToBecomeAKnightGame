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

    BoxCollider boxCollider;

    private float distToGround;

    private bool sinkEnemy = false;

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        spider = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();

        enemyAttack = GameObject.FindWithTag("EnemyAttack");
        boxCollider = enemyAttack.GetComponent<BoxCollider>();
        //Disable the collider on the spider untill it attacks
        boxCollider.isTrigger = true;
        distToGround = spider.bounds.extents.y;

        currHealth = maxHealth;
        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update () {

        if (IsGrounded())
        {
            //Hanging enemies need their navmesh agent enabled when they are on the ground
            if (this.gameObject.GetComponent<NavMeshAgent>().enabled != true)
            {
                this.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;
                this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }

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
                this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                rigidBody.isKinematic = true;
                spider.isTrigger = true; 
            }
        }
        else
        {
            this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }

        if(sinkEnemy)
        {
            transform.Translate(-Vector3.up * 1.8f * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            boxCollider.isTrigger = false;
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
        boxCollider.isTrigger = true;
    }

    public void StartDestory()
    {
        sinkEnemy = true;

        // After 1.5 seconds destory the enemy.
        Destroy(gameObject, 1.5f);
    }

    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
