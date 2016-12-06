using UnityEngine;
using System.Collections;

public class RattleboneAI : SkeletonAI, EnemyInterface
{
    public BossStage currentState = BossStage.None;

    public GameObject sheild;

    private GameObject player;

    private bool canTakeDamage = true;

    private float damageWaitTime = 1.0f;

    private float shotWaitTime = 5.0f;

    private bool sheildOn = false;

    private float invulnerableWaitTime = 5.0f;

    public Bullet bullet;

    private bool canShoot = true;

    Vector3 move;

    Vector3 rotationMask = new Vector3(0, 1, 0);
    float rotationSpeed = 60.0f; 

    // Use this for initialization
    void Start () {
        base.maxHealth = 600.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        sheild = GameObject.FindGameObjectWithTag("Block");
        sheild.SetActive(false);

        base.Start();
    }
	
	// Update is called once per frame
	void Update () {

        if (!IsDead)
        {
            if (currentState == BossStage.None)
            {
                ChangeCurrentStage();
            }

            if (currentState == BossStage.Stage1)
            {
                base.Update();
            }

            if (currentState == BossStage.Stage2 || currentState == BossStage.Stage3)
            {
                if (sheildOn)
                {
                    EnemyInvulnerable();
                }
                else
                {
                    sheild.SetActive(false);
                    StartCoroutine(InvulnerableDelayOn());
                }

                base.Update();
            }

            if (currentState == BossStage.Stage3)
            {
                GameObject fireBall = GameObject.FindGameObjectWithTag("ProjectileSpawn");

                if (canShoot)
                {
                    Bullet projectile = (Bullet)Instantiate(bullet, fireBall.transform.position, fireBall.transform.rotation);
      
                    projectile.GetComponent<Rigidbody>().velocity = (player.transform.position - fireBall.transform.position) * 0.8f;
           
                    StartCoroutine(shotDelay());
                }
                base.Update();
            }

            if (CurrentHealth == (getMaxHealth() - getMaxHealth() / 3) && currentState == BossStage.Stage1)
            {
                ChangeCurrentStage();
            }

            if (CurrentHealth == (getMaxHealth() - (getMaxHealth() / 3)*2) && currentState == BossStage.Stage2)
            {
                ChangeCurrentStage();
            }

        }
        else
        {
            player.GetComponent<Character>().finishedLevel = true;
        }

    }

    //Changes the current stage of the rattlebone boss fight
    void ChangeCurrentStage()
    {
        switch (currentState)
        {
            case BossStage.None:
                currentState = BossStage.Stage1;
                return;

            case BossStage.Stage1:
                currentState = BossStage.Stage2;
                sheildOn = true;
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

    void EnemyInvulnerable()
    {
        sheild.SetActive(true);
        sheild.transform.RotateAround(this.transform.position, rotationMask, rotationSpeed * Time.deltaTime);
        StartCoroutine(InvulnerableDelayOff());
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GetAnimator.SetTrigger("Attack");
        }

        if (col.gameObject.CompareTag("Weapon") && canTakeDamage && !sheildOn && base.IsAlive)
        {
            print("BOSS HIT");
            CurrentHealth -= 25.0f;
            StartCoroutine(damageDelay());
        }
    }

    IEnumerator damageDelay()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageWaitTime);
        canTakeDamage = true;
    }

    IEnumerator shotDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotWaitTime);
        canShoot = true;
    }

    IEnumerator InvulnerableDelayOn()
    {
        yield return new WaitForSeconds(invulnerableWaitTime);
        sheildOn = true;
    }

    IEnumerator InvulnerableDelayOff()
    {
        yield return new WaitForSeconds(invulnerableWaitTime);
        sheildOn = false;
    }
}
