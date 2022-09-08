using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DeckSlot : MonoBehaviour
{
    public void RemoveCard()
    {
        FindObjectOfType<GameData>().RemoveCardFromDeck(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
    }
}
