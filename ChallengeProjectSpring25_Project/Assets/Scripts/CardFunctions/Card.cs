using UnityEngine;

public class Card : MonoBehaviour
{
    
    public void IndicateCardShown()
    {
        FindFirstObjectByType<CardDrawingController>().CardShownAnim();
    }

    public void IndicateCardGone()
    {
        FindFirstObjectByType<CardDrawingController>().CardDiscardedAnim();
    }
}
