using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameData : MonoBehaviour
{

    public string default_deck = "https://api.pokemontcg.io/v2/cards?q=set.id:base1";

    [System.Serializable]
    public struct Card
    {
        public string name;
        public int hp;
        public int number;
        public string type;
        public string rarity;
        public Texture tex;
    }
    public List<Card> full_deck = new List<Card>();

    public List<Card> deck_one = new List<Card>();
    public List<Card> deck_two = new List<Card>();
    public List<Card> deck_three = new List<Card>();

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //StartCoroutine(GetFullDeck());
    }

    public IEnumerator GetFullDeck()
    {
        string url = default_deck;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();


            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                JSONNode jsonInfo = JSON.Parse(webRequest.downloadHandler.text);

                for (int i = 0; i < 102; i++)
                {

                    Card card = new Card();
                    card.name = jsonInfo["data"][i]["name"];
                    card.hp = jsonInfo["data"][i]["hp"];
                    card.number = jsonInfo["data"][i]["number"];
                    card.rarity = jsonInfo["data"][i]["rarity"];
                    card.type = jsonInfo["data"][i]["types"][0];


                    if (card.type == null)
                    {
                        card.type = "N/A";
                    }

                    string img_loc = jsonInfo["data"][i]["images"]["small"];

                    UnityWebRequest www = UnityWebRequestTexture.GetTexture(img_loc);
                    yield return www.SendWebRequest();

                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                        card.tex = myTexture;
                    }

                    full_deck.Add(card);

                }
            }
            else print(webRequest.error);

        }

    }

    public void AddCardToDeck(Card card)
    {

        int deck_index = FindObjectOfType<GetCards>().deck_index;


        if(deck_index == 0)
        {
            if (deck_one.Count >= 25) return;

            if (deck_one.Contains(card)) return;

            deck_one.Add(card);
        }
       else if(deck_index == 1)
        {
            if (deck_two.Count >= 25) return;

            if (deck_two.Contains(card)) return;

            deck_two.Add(card);
        }
        else
        {
            if (deck_three.Count >= 25) return;

            if (deck_three.Contains(card)) return;

            deck_three.Add(card);
        }




        FindObjectOfType<GetCards>().DisplayDeck();
    }

    public void RemoveCardFromDeck(string str)
    {
        int deck_index = FindObjectOfType<GetCards>().deck_index;

        if (deck_index == 0)
        {
            foreach (var item in deck_one)
            {
                if (str == item.name)
                {
                    deck_one.Remove(item);
                    break;
                }
            }
        }
        else if (deck_index == 1)
        {
            foreach (var item in deck_two)
            {
                if (str == item.name)
                {
                    deck_two.Remove(item);
                    break;
                }
            }
        }
        else
        {
            foreach (var item in deck_three)
            {
                if (str == item.name)
                {
                    deck_three.Remove(item);
                    break;
                }
            }
        }


        FindObjectOfType<GetCards>().DisplayDeck();
    }

}
