using UnityEngine;
using System.Collections;

public class WillyAI : MonoBehaviour
{

    public float angle = 90f;

    GameObject player;

    Dialogue dialog;

    Animator anim;

    private bool completedLevelOne;

    private bool completedLevelTwo;

    private bool completedLevelThree;

    private bool completedGame;

    private bool playOnce;

    private string[] startGameDialog = { "Oi!", "So you're here to become a knight Morpheus?", "Of course you are!", "My basement has a terrible spider infestation.", "Clear that up for me would you?", "Good lad!" };

    private string[] levelOneCompletedDialog = { "Back so soon?", "I suppose you want to become a knight now?", "WELL TOO BAD!", "I still have some chores that need doing!", "My old vacation spot has been taken over by one of my creations.",
        "I think I deserve a nice vacation, don't you?", "Go clear out the rabble please.", "Cheerio!" };

    private string[] levelTwoCompletedDialog = { "Did you bring me a souvenir?", "Oh.....", "Well I guess that means SOMEONE doesn’t get to become a knight.", "You know, I miss my childhood toy, Ellie.", "I think you should bring her to me.",
    "UNHARMED, please.", "I’ll just send you over to my old house...", "You aren’t afraid of time travel are you?", "Well, too late to think of that now..", "TA TA!"};

    private string[] levelThreeCompletedDialog = { "Did you find Ellie?!", "You killed her?", "Why...why would you do that?", "Oh, she attacked you? Well she always was spirited.", "I suppose I should make you a knight now…", "Buut I still have more chores that need doing!"
    , "I think you're going to be hanging around for a while… that is if you want to become a knight!", "HO HO, HA HA!"};

    private GameObject portal1;

    private GameObject portal2;

    private GameObject portal3;

    private GameObject portalToOpen;


    void Awake()
    {
        //Disabling all portals to ensure that portals are only activated when player is done talking to willy
        portal1 = GameObject.FindGameObjectWithTag("portal1");
        portal2 = GameObject.FindGameObjectWithTag("portal2");
        portal3 = GameObject.FindGameObjectWithTag("portal3");

        portal1.SetActive(false);
        portal2.SetActive(false);
        portal3.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        //REMOVE FOR TESTING ONLY

        player = GameObject.FindGameObjectWithTag("Player");
        dialog = this.GetComponentInChildren<Dialogue>();

        //get animator on character
        anim = GetComponent<Animator>();

        completedLevelOne = player.GetComponent<Character>().GetCompletedLevels[0];
        completedLevelTwo = player.GetComponent<Character>().GetCompletedLevels[1];
        completedLevelThree = player.GetComponent<Character>().GetCompletedLevels[2];

        if (!completedLevelOne && !completedLevelTwo && !completedLevelThree)
        {
            dialog.messages = startGameDialog;
            portalToOpen = portal1;
            dialog.NextMessage();
        }

        if (completedLevelOne && !completedLevelTwo && !completedLevelThree)
        {
            dialog.messages = levelOneCompletedDialog;
            portalToOpen = portal2;
            dialog.NextMessage();
        }

        if (completedLevelOne && completedLevelTwo && !completedLevelThree)
        {
            dialog.messages = levelTwoCompletedDialog;
            portalToOpen = portal3;
            dialog.NextMessage();
        }

        if (completedLevelOne && completedLevelTwo && completedLevelThree)
        {
            completedGame = true;
            dialog.messages = levelThreeCompletedDialog;
            dialog.NextMessage();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < angle)
        {
            if (!dialog.finishedTalking)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    anim.SetTrigger("Talking");
                    dialog.NextMessage();
                }
            }
        }

        if (dialog.finishedTalking && !completedGame)
        {
            Debug.Log("Open Portal");
            anim.SetTrigger("OpenPortal");
            Debug.Log(portalToOpen);
            portalToOpen.SetActive(true);
        }

        if (dialog.finishedTalking && completedGame && !playOnce)
        {
            anim.SetTrigger("CompletedGame");
            playOnce = true;
        }

    }

    public void PortalOpen()
    {
        anim.SetTrigger("PortalOpened");
    }
}