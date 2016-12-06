using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Character : MonoBehaviour {

    #region Variables
    public Image HealthBar;

    static int CURR_WEAPON = 0;
    static bool[] UNLOCKED_WEAPONS = { true, false, false };
    static bool[] UNLOCKED_COMPANIONS = { false, false };

    private bool canTakeDamage = true;

    private float damageWaitTime = 0.7f;

    private GameObject player;

    public Transform checkPoint;

    private NavMeshAgent agent;

    private GameObject weaponsObject;
    private GameObject weaponsUIObject;

    public bool finishedLevel = false;

    public GameObject lever;
    public GameObject teleportToHere;
    #endregion

    #region Properties
    public bool[] GetUnlockedWeapons
    {
        get
        {
            return UNLOCKED_WEAPONS;
        }

        set
        {
            UNLOCKED_WEAPONS = value;
        }
    }

    public bool[] GetUnlockedCompanions
    {
        get
        {
            return UNLOCKED_COMPANIONS;
        }

        set
        {
            UNLOCKED_COMPANIONS = value;
        }
    }
    #endregion

    // Use this for initialization
    void Start () {
        agent = GetComponentInChildren<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        weaponsObject = GameObject.FindGameObjectWithTag("WeaponsObject");
        weaponsUIObject = GameObject.FindGameObjectWithTag("WeaponsUIObject");
        checkPoint = GameObject.FindWithTag("StartSpawn").transform;
        HealthBar.fillAmount = 1.0f;
        SetWeapon(CURR_WEAPON);
        UpdateWeaponUI();
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWeapon(2);
        }

        if (finishedLevel && lever != null)
        {
            lever.transform.position = teleportToHere.transform.position;
        }

        if (HealthBar.fillAmount == 0)
        {
            PlayerDead();
        }

        //When player completes a level reset the checkpoint to the start
        if(finishedLevel)
        {
            checkPoint = GameObject.FindWithTag("StartSpawn").transform;
        }
    }

    public void RemoveHealth(float amount)
    {
        //TODO: do some sort of animation
        HealthBar.fillAmount -= amount;
    }

    public void AddHealth(float amount)
    {
        //TODO: do some sort of animation
        HealthBar.fillAmount += amount;
    }

    public float GetHealth()
    {
        return HealthBar.fillAmount;
    }

    public void UpdateWeaponUI()
    {
        for (int i = 0; i < this.weaponsUIObject.transform.childCount; i++)
        {
            if (!UNLOCKED_WEAPONS[i])
            {
                this.weaponsUIObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                this.weaponsUIObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void AddWeapon(int index)
    {
        if (index <= UNLOCKED_WEAPONS.Length)
        {
            UNLOCKED_WEAPONS[index] = true;
        }
        UpdateWeaponUI();
    }

    public void AddCompanion(int index)
    {
        Debug.Log("Companion Found");
        if (index <= UNLOCKED_COMPANIONS.Length)
        {
            UNLOCKED_COMPANIONS[index] = true;
        }
    }

    void SetWeapon(int index)
    {
        if (UNLOCKED_WEAPONS[index])
        {
            CURR_WEAPON = index;
            for (int i = 0; i < this.weaponsObject.transform.childCount; i++)
            {
                if (i != CURR_WEAPON)
                {
                    this.weaponsObject.transform.GetChild(i).gameObject.SetActive(false);
                    this.weaponsUIObject.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                }
                else
                {
                    this.weaponsObject.transform.GetChild(i).gameObject.SetActive(true);
                    this.weaponsUIObject.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 0.3f);
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }

        if (col.gameObject.CompareTag("EnemyAttack") && canTakeDamage)
        {
            print("PLAYER HIT");
            RemoveHealth(.1f);
            StartCoroutine(damageDelay());
        }

        if(col.gameObject.CompareTag("Hazard") && canTakeDamage)
        {
            print("PLAYER HIT");
            RemoveHealth(.2f);
            StartCoroutine(damageDelay());
        }

        if (col.gameObject.CompareTag("Trap") && canTakeDamage)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            print("PLAYER HIT");
            RemoveHealth(.8f);
            StartCoroutine(damageDelay());
        }

        if (col.gameObject.CompareTag("OutOfBounds"))
        {
            print("OutOfBounds");
            PlayerDead();
        }

        if (col.gameObject.CompareTag("Projectile"))
        {
            print("PLAYER HIT PROJECTILE");
            RemoveHealth(.1f);
            StartCoroutine(damageDelay());
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Hazard") && canTakeDamage)
        {
            print("PLAYER HIT");
            RemoveHealth(.2f);
            StartCoroutine(damageDelay());
        }

        if (col.gameObject.CompareTag("Trap") && canTakeDamage)
        {
            print("PLAYER HIT");
            RemoveHealth(.8f);
            StartCoroutine(damageDelay());
        }

        if (col.gameObject.CompareTag("EnemyAttack") && canTakeDamage)
        {
            print("PLAYER HIT");
            RemoveHealth(.1f);
            StartCoroutine(damageDelay());
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("CheckPoint"))
        {
            checkPoint = col.transform;
        }

        if (col.gameObject.CompareTag("Hazard") && canTakeDamage)
        {
            print("PLAYER HIT");
            RemoveHealth(.2f);
            StartCoroutine(damageDelay());
        }
    }

    private void PlayerDead()
    {
        player.transform.position = checkPoint.position;
        //Player is at full health again
        AddHealth(1.0f);
    }

    IEnumerator damageDelay()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageWaitTime);
        canTakeDamage = true;
    }
}
