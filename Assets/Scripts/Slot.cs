using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

    public GameData.Card card;

    public void SetInfo(GameData.Card info)
    {
        card = info;
    }


    public void ClickedSlot()
    {
        FindObjectOfType<GameData>().AddCardToDeck(card);
    }

}
