using UnityEngine;
using System.Collections;

public class CompanionAI : MonoBehaviour {

    Animator anim;

    private bool colliding = false;

    private bool activated = false;

    private Transform player;

    private NavMeshAgent navMesh;

    private float distanceToPlayer;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMesh = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.G) && colliding && !activated)//if g is pressed and objects are colliding
        {
            print("Active");
            activated = true;
            colliding = false;
        }

        if(activated)
        {
           distanceToPlayer = navMesh.remainingDistance;
           MoveCompanion(player.position);
           anim.SetBool("PlayerObtained", true);
           anim.SetTrigger("FollowPlayer");
        }

    }

    void OnTriggerEnter(Collider other)//true when objects are colliding
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("colliding = true");
            colliding = true;
        }
    }

    void OnTriggerExit(Collider other)//false when objects arent 
    {
        Debug.Log("Colliding = false");
        colliding = false;
    }

    void MoveCompanion(Vector3 position)
    {
        navMesh.SetDestination(position);
    }
}
