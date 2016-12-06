using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour {

    #region Variables
    public float heightAboveCharacter = 5.0f;
    public float timeBetweenLetters = 0.04f;
    public float distanceToShow = 15.0f;
    public float fadeSpeed = 2.0f;
    public float angle = 45f;

    public string[] messages;

    List<string> gameStart;

    List<string> levelOneFinished;

    List<string> levelTwoFinished;

    List<string> levelThreeFinished;

    public bool finishedTalking;

    private Text DialogueText;
    float distance;
    GameObject player;
    bool isFading;
    bool isTyping;
    CanvasGroup canvasGroup;

    int currMessage;
    #endregion

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        canvasGroup = this.GetComponent<CanvasGroup>();
        isFading = false;
        DialogueText = this.GetComponentInChildren<Text>();
        currMessage = -1;
    }
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(transform.parent.position, player.transform.position);
        if (distance < distanceToShow) {

            if (canvasGroup.alpha < 1f && !isFading)
            {
                isFading = true;
                StartCoroutine(FadeDialogue());
            }
      
            Vector3 targetPostition = new Vector3(player.transform.position.x,
                                            this.transform.position.y,
                                            player.transform.position.z);
            transform.LookAt(targetPostition);
        }
        else
        {
            if (canvasGroup.alpha > 0 && !isFading)
            {
                isFading = true;
                StartCoroutine(FadeDialogue());
            }
        }
    }

    public void NextMessage()
    {
        if (isTyping)
        {
            DialogueText.text = messages[currMessage];
            isTyping = false;
        }
        else
        {
           DialogueText.text = "";
           isTyping = true;
           if (++currMessage >= messages.Length)
            {
                finishedTalking = true;
                isFading = true;
                StartCoroutine(FadeDialogue());
                this.enabled = false;
            }
           else
            {
                StartCoroutine(TypeTextLetters(currMessage));

            }
        }
    }

    IEnumerator TypeTextLetters(int currMessage)
    {
        foreach (char letter in messages[currMessage].ToCharArray())
        {
            if (isTyping)
            {
                DialogueText.text += letter;
                yield return new WaitForSeconds(timeBetweenLetters);
            }
            else
            {
                yield return null;
            }
        }
        isTyping = false;       
    }

    IEnumerator FadeDialogue()
    {
        // fade in or fade out dialogue
        if (canvasGroup.alpha == 1f)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
                yield return null;
            }
            isFading = false;
        }
        else if (canvasGroup.alpha == 0)
        {
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += Time.deltaTime * fadeSpeed;
                yield return null;
            }
            isFading = false;
        }
    }
}
