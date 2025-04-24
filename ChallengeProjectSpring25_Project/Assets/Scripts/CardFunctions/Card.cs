using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardName;
    [TextArea(3, 10)]
    public string cardDescription;

    public enum CardType
    {
        boss,
        environment,
        terrain
    }

    public CardType cardType;
    public TarotCard tarotData;

    public void IndicateCardShown()
    {
        FindFirstObjectByType<CardDrawingController>().CardShownAnim();
    }

    public void IndicateCardGone()
    {
        FindFirstObjectByType<CardDrawingController>().CardDiscardedAnim();
    }
}
