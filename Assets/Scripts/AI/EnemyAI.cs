using UnityEngine;
using System.Collections;
using System;

public abstract class EnemyAI : MonoBehaviour {

    public Transform player;

    private NavMeshAgent navMesh;

    private Vector3 startPosition;

    // Use this for initialization
    protected virtual void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMesh = GetComponent<NavMeshAgent>();
        startPosition = this.transform.position;
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        MoveEnemy(player.position);
        navMesh.updatePosition = true;
        navMesh.updateRotation = true;  
    }

    protected void MoveEnemy(Vector3 position)
    {
        navMesh.SetDestination(position);
    }

    public NavMeshAgent NavMesh
    {
        get
        {
            return this.navMesh;
        }
        set
        {
            this.navMesh = value;
        }
    }
}
