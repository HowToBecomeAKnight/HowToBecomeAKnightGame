using UnityEngine;
using System.Collections;

public class particleGrid : MonoBehaviour {

    public GameObject particle;
    
    // Use this for initialization
	void Start () {

        for (int i = 0; i < 10; i ++)
        {
            Instantiate(particle, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
        }
	
	}
	
}
