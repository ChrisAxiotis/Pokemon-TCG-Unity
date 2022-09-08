using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class GetCards : MonoBehaviour
{

    public Transform ui_slot_parent;
    public Animator fader;

    public ScrollRect myScrollRect;

    int sorted_index = 0;

    public int deck_index = 0;

    public Transform deck_panel;
    public TextMeshProUGUI deck_count;

    GameData data;

    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<GameData>();
        GetData();

    }

    public IEnumerator FadeTrigger(int state, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (state == 0) fader.SetTrigger("out");
        if (state == 1) fader.SetTrigger("in");
    }

    public void ChangeSortOrder(int value)
    {

        switch (value)
        {
            case 0:
                sorted_index = 0;
                break;
            case 1:
                sorted_index = 1;
                break;
            case 2:
                sorted_index = 2;
                break;
            case 3:
                sorted_index = 3;
                break;
        }

            Display();
    }

    public void ChangeDeck(int value)
    {
        deck_index = value;
        DisplayDeck();
    }

    public List<GameData.Card> default_deck = new List<GameData.Card>();
    public List<GameData.Card> deck_one = new List<GameData.Card>();

    public void Display()
    {

        List<GameData.Card> temp = new List<GameData.Card>(default_deck);

        if (sorted_index == 0)
            temp.Sort((p1, p2) => p1.number.CompareTo(p2.number));
        if (sorted_index == 1)
            temp.Sort((p1, p2) => p1.hp.CompareTo(p2.hp));
        if (sorted_index == 2)
            temp.Sort((p1, p2) => p1.type.CompareTo(p2.type));
        if (sorted_index == 3)
            temp.Sort((p1, p2) => p1.rarity.CompareTo(p2.rarity));


        foreach (Transform t in ui_slot_parent)
        {
            t.GetComponent<Slot>().card.name = null;
            t.GetChild(0).GetComponent<RawImage>().texture = null;
        }

            int index = 0;
        foreach (Transform t in ui_slot_parent)
        {
            t.GetComponent<Slot>().card = temp[index];
            t.GetChild(0).GetComponent<RawImage>().texture = temp[index].tex;
            index++;
        }

    }

    //Get Deck from memory
    public void GetData()
    {
     
        foreach (GameData.Card item in data.full_deck)
        {
            default_deck.Add(item);
        }
        Display();
        DisplayDeck();
    }



    public void DisplayDeck()
    {
        foreach (Transform item in deck_panel)
        {
            item.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Empty";
        }

        if (deck_index == 0)
        {
            for (int i = 0; i < data.deck_one.Count; i++)
            {
                deck_panel.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = data.deck_one[i].name;
            }

            deck_count.text = data.deck_one.Count.ToString() + "/25";
        }
        else if (deck_index == 1)
        {
            for (int i = 0; i < data.deck_two.Count; i++)
            {
                deck_panel.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = data.deck_two[i].name;
            }

            deck_count.text = data.deck_two.Count.ToString() + "/25";
        }
        else
        {
            for (int i = 0; i < data.deck_three.Count; i++)
            {
                deck_panel.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = data.deck_three[i].name;
            }

            deck_count.text = data.deck_three.Count.ToString() + "/25";
        }
    }

   



}
