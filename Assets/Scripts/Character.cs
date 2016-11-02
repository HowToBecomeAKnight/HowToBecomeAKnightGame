using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Character : MonoBehaviour {

    public Image HealthBar;

	// Use this for initialization
	void Start () {
	    HealthBar.fillAmount = 1f;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void RemoveHealth(int amount)
    {
        //TODO: do some sort of animation
        HealthBar.fillAmount -= amount;
    }

    public void AddHealth(int amount)
    {
        //TODO: do some sort of animation
        HealthBar.fillAmount += amount;
    }
}
