using UnityEngine;
using System.Collections;

public class SpiderAI : EnemyAI, EnemyInterface {

    #region Variables
    private Animator animator;

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
    #endregion

    #region Properties
    public float CurrentHealth
    {
        get
        {
            return currHealth;
        }

        set
        {
            currHealth = value;
        }
    }

    public bool IsDead
    {
        get
        {
            return isDead;
        }

        set
        {
            isDead = value;
        }
    }

    public bool CanTakeDamage
    {
        get
        {
            return canTakeDamage;
        }

        set
        {
            canTakeDamage = value;
        }
    }

    public Animator GetAnimator
    {
        get
        {
            return animator;
        }

        set
        {
            animator = value;
        }
    }
    #endregion

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

        if (!isDead)
        {
            if (IsGrounded())
            {
                //Hanging enemies need their navmesh agent enabled when they are on the ground
                if (this.gameObject.GetComponent<NavMeshAgent>().enabled != true)
                {
                    this.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;
                    this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
                }

                if (!PlayerInSight())
                {
                    // enemy can see the player!
                    animator.SetTrigger("Chase");
                    MoveEnemy(Player.position);
                }


                if (currHealth == 0.0f)
                {
                    Death();
                }
            }
            else
            {
                this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
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

    public void Death()
    {
        isDead = true;

        animator.SetTrigger("Die");

        base.NavMesh.Stop();
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        rigidBody.isKinematic = true;
        spider.isTrigger = true;
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

    IEnumerator disableObject()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }

    public void AttackDone()
    {
        boxCollider.isTrigger = true;
    }

    public void StartDestory()
    {
        sinkEnemy = true;
        // After 1.5 seconds disable the enemy.
        StartCoroutine(disableObject());
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    public bool PlayerInSight()
    {
        NavMeshHit hit;
        return this.NavMesh.Raycast(Player.position, out hit);
    }
}
