using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    Transform player;
    public Image healthBar;
    GameObject enemy;
    EnemyInterface script;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = transform.parent.gameObject;
        script = enemy.GetComponent(typeof(EnemyInterface)) as EnemyInterface;
    }

    // Update is called once per frame
    void Update()
    {
        if (script.getCurrHealth() == script.getMaxHealth())
        {
            this.GetComponent<CanvasGroup>().alpha = 0;
            return;
        }
        this.GetComponent<CanvasGroup>().alpha = 1;
        Vector3 targetPostition = new Vector3(player.position.x,
                                        this.transform.position.y,
                                        player.position.z);
        transform.LookAt(targetPostition);
        Debug.Log(script.getCurrHealth() / script.getMaxHealth());
        healthBar.fillAmount = (script.getCurrHealth()/script.getMaxHealth());
    }
}
