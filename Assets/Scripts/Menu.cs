using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject menuObj;
    public GameObject aboutObj;


    public Animator fader;

    private void Start()
    {
        fader.SetTrigger("in");
        OpenMenu();
    }


    public void OpenMenu()
    {
        menuObj.SetActive(true);
        aboutObj.SetActive(false);
    }
    public void OpenAbout()
    {
        menuObj.SetActive(false);
        aboutObj.SetActive(true);
    }

    public void StartBuilder()
    {
        StartCoroutine(FadeToLevel(2,1));
    }

    IEnumerator FadeToLevel(int stage, float delay)
    {
        fader.SetTrigger("out");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(stage);
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }

}
