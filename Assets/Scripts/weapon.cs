using UnityEngine;
using System.Collections;

public class weapon : MonoBehaviour {

    public int weaponId = 0;
    Transform player;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < 10.0f)
        {
            if (Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < 90f)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    player.GetComponent<Character>().AddWeapon(weaponId);
                    Destroy(gameObject);
                }
            }
        }
    }
}
