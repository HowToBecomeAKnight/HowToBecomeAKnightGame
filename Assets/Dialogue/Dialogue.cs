using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    public float heightAboveCharacter = 5.0f;
    public float timeBetweenLetters = 0.04f;
    public float distanceToShow = 50.0f;
    public float fadeSpeed = 2.0f;

    public string[] messages = { "this is the first message", "this is the second message", "this is the third message" };

    private Text DialogueText;
    string message;
    float distance;
    Transform player;
    bool isFading;
    bool isTyping;
    CanvasGroup canvasGroup;
    float posTopObject;

    int currMessage;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        canvasGroup = this.GetComponent<CanvasGroup>();
        isFading = false;
        DialogueText = this.GetComponentInChildren<Text>();
        currMessage = 0;
        isTyping = true;
        StartCoroutine(TypeTextLetters(currMessage));
    }
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(transform.parent.position, player.position);
        if (distance < distanceToShow)
        {
            if (canvasGroup.alpha < 1f && !isFading)
            {
                isFading = true;
                StartCoroutine(FadeDialogue());
            }
      
            Vector3 targetPostition = new Vector3(player.position.x,
                                            this.transform.position.y,
                                            player.position.z);
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
           StartCoroutine(TypeTextLetters(++currMessage));
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
