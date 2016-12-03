using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    Rigidbody rigidbody;

    public Rigidbody rb
    {
        get
        {
            return rigidbody;
        }

        set
        {
            rigidbody = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(DestroySelf());
    }

    void OnCollisionEnter(Collision other)
    {
        //If the bullet his any object that isnt the enemy it is destoryed
        if(!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    //Bullet is destoryed after 5 seconds to ensure no infinite spawning bullets
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
