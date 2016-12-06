using UnityEngine;
using System.Collections;

public enum BossStage
{
    None,

    Stage1,

    Stage2,

    Stage3
}

public class SpiderQueenAI : SpiderAI, EnemyInterface
{
    public BossStage currentState = BossStage.None;

    private GameObject player;

    private GameObject cocoon;

    public GameObject brood1;

    public GameObject brood2;

    public GameObject brood3;

    public GameObject acidPoolAttack;

    // Use this for initialization
    protected override void Start () {
        base.maxHealth = 300.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        cocoon = GameObject.FindGameObjectWithTag("Cocoon");

        base.Start();
	
	}

    // Update is called once per frame
    protected override void Update () {

        if (!IsDead)
        {
            if(PlayerInSight() && currentState == BossStage.None)
            {
                ChangeCurrentStage();
            }

            if (currentState == BossStage.Stage1)
            {
                //print("Stage1");
                base.Update();
            }

            if (currentState == BossStage.Stage2)
            {
                CanTakeDamage = false;
                NavMesh.speed = 15.0f;
                MoveEnemy(cocoon.transform.position);

                if (NavMesh.remainingDistance == 0)
                {
                    NavMesh.Stop();
                    GetAnimator.SetTrigger("Cocoon");
                    EnableCocoon();
                    SpawnBroodlings();
                }
            }

            if (currentState == BossStage.Stage3)
            {
                DisableCocoon();
                NavMesh.Resume();
                NavMesh.speed = 6.0f;
                CanTakeDamage = true;
                EnableAcidPools();
                base.Update();
            }

            if(CurrentHealth == (getMaxHealth() - getMaxHealth()/3) && currentState == BossStage.Stage1)
            {
                ChangeCurrentStage();
            }

            if(!brood1.activeSelf && !brood2.activeSelf && !brood3.activeSelf && currentState == BossStage.Stage2)
            {
                CurrentHealth = CurrentHealth - getMaxHealth() / 3;
                ChangeCurrentStage();
            }
        }
        else
        {
            //Completion of level one indicated in character
            player.GetComponent<Character>().FinishedLevel(0);
            player.GetComponent<Character>().finishedLevel = true;
        }
    }

    //Changes the current stage of the spider boss fight
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

    void SpawnBroodlings()
    {
        brood1.GetComponent<Rigidbody>().useGravity = true;
        brood1.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;

        brood2.GetComponent<Rigidbody>().useGravity = true;
        brood2.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;

        brood3.GetComponent<Rigidbody>().useGravity = true;
        brood3.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
    }

    void EnableCocoon()
    {
        cocoon.GetComponent<Renderer>().enabled = true;
        cocoon.GetComponent<CapsuleCollider>().isTrigger = false;
        cocoon.GetComponent<BoxCollider>().isTrigger = false;
    }

    void DisableCocoon()
    {
        cocoon.GetComponent<Renderer>().enabled = false;
        cocoon.GetComponent<CapsuleCollider>().isTrigger = true;
        cocoon.GetComponent<BoxCollider>().isTrigger = true;
    }

    void EnableAcidPools()
    {
        acidPoolAttack.gameObject.SetActive(true);
    }

    void DisableAcidPools()
    {
        acidPoolAttack.gameObject.SetActive(false);
    }
}
