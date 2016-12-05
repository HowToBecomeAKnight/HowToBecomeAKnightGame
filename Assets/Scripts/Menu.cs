using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject character;
    public GameObject characterUI;
    public Button startText;
    public Button quitText;

    // Use this for initialization
    void Start()
    {
        characterUI.SetActive(false);
        startText = startText.GetComponent<Button>();
        quitText = quitText.GetComponent<Button>();
        character.GetComponent<NormalMovement>().enabled = false;
        character.GetComponent<Animations>().enabled = false;
        character.GetComponent<Character>().enabled = false;


    }

    public void onStartPressed()
    {
        Destroy(startText.gameObject);
        Destroy(quitText.gameObject);
        characterUI.SetActive(true);
        character.GetComponent<NormalMovement>().enabled = true;
        character.GetComponent<Animations>().enabled = true;
        character.GetComponent<Character>().enabled = true;
    }

    public void onExitGame()
    {
        Application.Quit();
    }
}
