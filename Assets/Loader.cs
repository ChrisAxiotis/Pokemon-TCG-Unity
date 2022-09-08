using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    GameData data;
    public TextMeshProUGUI ui;
    int progress = 0;

    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<GameData>();

        StartCoroutine(CheckDeck());
    }

    private void Update()
    {
        progress = data.full_deck.Count;
        ui.text = "Loading Deck " + progress + "/102";
    }

    IEnumerator CheckDeck()
    {
        
        if(data.full_deck.Count < 1)
        {
            yield return data.GetFullDeck();
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }



}
