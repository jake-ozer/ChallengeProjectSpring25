using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{
    public GameObject cardContainer;
    public GameObject cardDisplayPrefab;
    public GameObject deathCanvas;
    public void DisplayDeathMenu(List<Card> cardList)
    {
        Time.timeScale = 0f;

        deathCanvas.SetActive(true);
        foreach (Card card in cardList)
        {
            GameObject cardObject = Instantiate(cardDisplayPrefab);
            cardObject.transform.SetParent(cardContainer.transform);
            cardObject.GetComponent<Image>().sprite = card.cardImage;
        }

    }

    //public void FailureUI()
    //{
    //    if(curWins < 3)
    //    {
    //        //Displays Death Card
    //    }
    //}
}
