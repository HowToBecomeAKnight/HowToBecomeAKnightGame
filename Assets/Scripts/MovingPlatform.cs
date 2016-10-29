using UnityEngine;
using System.Collections;

public enum CurrentState
{
    None,

    FirstPosition,

    SecondPosition
}

public class MovingPlatform : MonoBehaviour {

    public Transform platformToMove;

    public Transform firstPosition;

    public Transform secondPosition;

    public Vector3 targetPosition;

    public float speed;

    public float resetDelay;

    public CurrentState currentState = CurrentState.None;

	// Use this for initialization
	void Start () {
        ChangeCurrentState();
	}
	
	// Update is called once per frame
	void Update () {
        //Interpolated the movement for the platform from is current position to target position
        platformToMove.position = Vector3.Lerp(platformToMove.position, targetPosition, speed * Time.deltaTime);
	}

    //States that the plateform can be in, So that it is able to move between start and end states
    void ChangeCurrentState ()
    {
        switch (currentState)
        {
            case CurrentState.FirstPosition:
                currentState = CurrentState.SecondPosition;
                targetPosition = secondPosition.position;
                Invoke("ChangeCurrentState", resetDelay);
                return;

            case CurrentState.SecondPosition:
                currentState = CurrentState.FirstPosition;
                targetPosition = firstPosition.position;
                Invoke("ChangeCurrentState", resetDelay);
                return;

            case CurrentState.None:
                currentState = CurrentState.SecondPosition;
                targetPosition = secondPosition.position;
                Invoke("ChangeCurrentState", resetDelay);
                return;

            default:
                Invoke("ChangeCurrentState", resetDelay);
                return;
        }
    }
}
