using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardDrawingController : MonoBehaviour
{
    [SerializeField] private GameObject baseCardPrefab;
    [SerializeField] private Transform cardSpawnTransform;
    [SerializeField] private PlayerInput input;
    private GameObject curCardObj;
    private bool curCardShown = false;
    private bool curCardLock = false;

    public enum CardType
    {
        boss,
        environment,
        terrain
    }

    private void Start()
    {
        StartCoroutine("TestMultipleDrawings");
    }

    private void Update()
    {
        //remove card from observation when player is done looking at it
        if (input.actions["ForwardCard"].triggered && curCardShown)
        {
            curCardObj.GetComponent<Animator>().SetTrigger("forward_card");
        }
    }

    private IEnumerator TestMultipleDrawings()
    {
        for (int i = 0; i < 3; i++)
        {
            DrawCard();
            curCardLock = true;
            yield return new WaitUntil(()=> !curCardLock);
        }
    }

    //spawns card and gives it data specified in param
    //eventually we will have a card data scriptable object as param
    private void DrawCard(/*CardData cardData*/)
    {
        //spawn card
        GameObject cardObj = Instantiate(baseCardPrefab, cardSpawnTransform);
        cardObj.transform.parent = cardSpawnTransform;
        curCardObj = cardObj;


        //assign attributes from cardData
        //--not ready yet--

        //apply the effect of the card to the arena
        //--not ready yet--

    }

    private void ApplyCardEffect(CardType type/*, CardData cardData*/)
    {
        switch (type)
        {
            case CardType.boss:
                //boss spawn logic
                break;
            case CardType.environment:
                //environment effect spawn logic
                break;
            case CardType.terrain:
                //terrain logic
                break;
        }
    }

    //used by animation event to indicate that the current card is shown
    public void CardShownAnim()
    {
        curCardShown = true;
    }

    //used by animation event to indicate that current card is discarded
    public void CardDiscardedAnim()
    {
        Destroy(curCardObj);
        curCardLock = false;
        curCardShown = false;
    }
   
}
