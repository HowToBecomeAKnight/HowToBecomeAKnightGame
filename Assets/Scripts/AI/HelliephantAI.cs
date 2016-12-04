using UnityEngine;
using System.Collections;

public class HelliephantAI : EnemyAI, EnemyInterface
{
    #region Variables
    Animator animator;

    public BossStage currentState = BossStage.None;

    public float maxHealth = 300.0f;

    float currHealth;

    bool isDead = false;

    private bool sinkEnemy = false;

    private bool canTakeDamage = true;

    private bool charging = false;

    private bool makeCopy = false;

    private bool isClone = false;

    CapsuleCollider enemyCollider;

    GameObject damageCollider;

    float distanceToPlayer;

    Rigidbody rigidBody;

    Vector3 chargePosition;

    public GameObject chargeStart;

    public GameObject clone1;

    public GameObject clone2;

    private float damageWaitTime = 0.7f;
    #endregion

    #region Properties
    public bool IsClone
    {
        get
        {
           return this.isClone;
        }
        set
        {
            this.isClone = value;
        }
    }
    #endregion

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        enemyCollider = GameObject.FindGameObjectWithTag("Enemy").GetComponent<CapsuleCollider>();
        damageCollider = GameObject.FindGameObjectWithTag("EnemyAttack");
        rigidBody = GetComponent<Rigidbody>();
        currHealth = maxHealth;
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        if (!isDead)
        {
            if (distanceToPlayer < 25 && currentState == BossStage.None)
            {
                ChangeCurrentStage();
            }

            if(currentState == BossStage.Stage1)
            {
                animator.SetTrigger("Chase");
                base.Update();
            }

            if(currentState == BossStage.Stage2 || currentState == BossStage.Stage3)
            {
                if(distanceToPlayer < 15 && !charging)
                {
                    RunAway();
                }
                else
                {
                    Charge();
                }
            }

            if(currentState == BossStage.Stage3 && !makeCopy)
            {
                CreateClone();
            }

            if (currHealth == (getMaxHealth() - getMaxHealth() / 3) && currentState == BossStage.Stage1)
            {
                ChangeCurrentStage();
            }

            if (currHealth == (getMaxHealth() - (getMaxHealth() / 3) * 2) && currentState == BossStage.Stage2)
            {
                ChangeCurrentStage();
            }

            if (currHealth == 0.0f)
            {
                Death();
            }
        }

        if (sinkEnemy)
        {
            transform.Translate(-Vector3.up * 1.8f * Time.deltaTime);
        }
    }

    void RunAway()
    {
        print("RUN AWAY");
        NavMesh.speed = 20.0f;
        animator.SetTrigger("Chase");
        MoveEnemy(chargeStart.transform.position);
    }

    void Charge()
    {
        print("CHARGING");
        charging = true;
        NavMesh.speed = 15.0f;
        if (distanceToPlayer > 15)
        {
            animator.SetTrigger("Chase");
            MoveEnemy(Player.position);
            chargePosition = Player.position;
        }
        else
        {
            animator.SetTrigger("Chase");
            MoveEnemy(chargePosition);

            if(NavMesh.remainingDistance < 3)
            {
                charging = false;
            }
        }
    }

    void CreateClone()
    {
        makeCopy = true;
        clone1 = Instantiate(gameObject);
        clone1.GetComponent<HelliephantAI>().currentState = BossStage.Stage2;
        clone1.GetComponent<HelliephantAI>().currHealth = 50.0f;
        clone1.GetComponent<HelliephantAI>().maxHealth = 50.0f;
        clone1.GetComponent<HelliephantAI>().IsClone = true;

        clone2 = Instantiate(gameObject);
        clone2.GetComponent<HelliephantAI>().currentState = BossStage.Stage2;
        clone2.GetComponent<HelliephantAI>().currHealth = 50.0f;
        clone2.GetComponent<HelliephantAI>().maxHealth = 50.0f;
        clone2.GetComponent<HelliephantAI>().maxHealth = 50.0f;
        clone2.GetComponent<HelliephantAI>().IsClone = true;

    }

    void Death()
    {
        isDead = true;
        animator.SetTrigger("Die");

        base.NavMesh.Stop();
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        rigidBody.isKinematic = true;
        enemyCollider.isTrigger = true;
        damageCollider.GetComponent<CapsuleCollider>().enabled = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Weapon") && canTakeDamage)
        {
            print("HIT");
            currHealth -= 25.0f;
            StartCoroutine(damageDelay());
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

    IEnumerator damageDelay()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageWaitTime);
        canTakeDamage = true;
    }

    void StartDestroy()
    {
        print("Destory");
        sinkEnemy = true;

        // After 1.5 seconds destory the enemy.
        Destroy(gameObject, 1.5f);
    }

    //Changes the current stage of the helliephant boss fight
    void ChangeCurrentStage()
    {
        switch (currentState)
        {
            case BossStage.None:
                currentState = BossStage.Stage1;
                return;

            case BossStage.Stage1:
                currentState = BossStage.Stage2;
                return;

            case BossStage.Stage2:
                currentState = BossStage.Stage3;
                return;

            case BossStage.Stage3:
                return;

            default:
                return;
        }
    }
}
