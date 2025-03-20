using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardName;
    [TextArea(3, 10)]
    public string cardDescription;
    public CardType cardType;

    public enum CardType
    {
        boss,
        environment,
        terrain
    }

    public void IndicateCardShown()
    {
        FindFirstObjectByType<CardDrawingController>().CardShownAnim();
    }

    public void IndicateCardGone()
    {
        FindFirstObjectByType<CardDrawingController>().CardDiscardedAnim();
    }
}
