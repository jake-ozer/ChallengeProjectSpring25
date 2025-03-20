using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardDrawingController : MonoBehaviour
{
    [SerializeField] private GameObject baseCardPrefab;
    [SerializeField] private Transform cardSpawnTransform;
    [SerializeField] private PlayerInput input;
    public List<GameObject> possibleCards;
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private PlayerMovement playerMovement;
    private GameObject curCardObj;
    private bool curCardShown = false;
    private bool curCardLock = false;

    

    private void Start()
    {
        StartCoroutine("StartDrawingCards");
    }

    private void Update()
    {
        //remove card from observation when player is done looking at it
        if (input.actions["ForwardCard"].triggered && curCardShown)
        {
            curCardObj.GetComponent<Animator>().SetTrigger("forward_card");
            curCardObj.GetComponent<EnvironmentCard>().SpawnEnvironmentEffect(); //this maybe should be custom for each card type, but this is a test so chill out
        }
    }

/*    private IEnumerator TestMultipleDrawings()
    {
        for (int i = 0; i < 3; i++)
        {
*//*            DrawCard();
            curCardLock = true;
            yield return new WaitUntil(()=> !curCardLock);*//*
        }
    }*/

    private IEnumerator StartDrawingCards()
    {
        //lock player at top of the map
        playerMovement.enabled = false;

        //draw terrain
        DrawCard(Card.CardType.terrain);
        curCardLock = true;
        yield return new WaitUntil(() => !curCardLock);
        navMeshSurface.BuildNavMesh();
        //draw environment
        DrawCard(Card.CardType.environment);
        curCardLock = true;
        yield return new WaitUntil(() => !curCardLock);
        //draw boss
        DrawCard(Card.CardType.boss);
        curCardLock = true;
        yield return new WaitUntil(() => !curCardLock);

        //unlock player
        playerMovement.enabled = true;
        this.gameObject.SetActive(false);
    }


    //spawns card and gives it data specified in param
    //eventually we will have a card data scriptable object as param
    private void DrawCard(Card.CardType type)
    {
        List<GameObject> filteredCards = possibleCards.Where(x=>x.GetComponent<Card>().cardType == type).ToList();
        GameObject randomlyPickedCard = filteredCards[Random.Range(0,filteredCards.Count)];

        GameObject cardObj = Instantiate(randomlyPickedCard, cardSpawnTransform);
        cardObj.transform.parent = cardSpawnTransform;
        curCardObj = cardObj;

        //spawn card
        /*GameObject cardObj = Instantiate(baseCardPrefab, cardSpawnTransform);
        cardObj.transform.parent = cardSpawnTransform;
        curCardObj = cardObj;*/


        //assign attributes from cardData
        //--not ready yet--

        //apply the effect of the card to the arena
        //--not ready yet--

    }

    private void ApplyCardEffect(Card.CardType type/*, CardData cardData*/)
    {
        switch (type)
        {
            case Card.CardType.boss:
                //boss spawn logic
                break;
            case Card.CardType.environment:
                //environment effect spawn logic
                break;
            case Card.CardType.terrain:
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
