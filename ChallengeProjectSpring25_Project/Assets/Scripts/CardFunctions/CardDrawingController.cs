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
    private bool canSpawnCardObj = true;
    
    private void Start()
    {
        StartCoroutine("StartDrawingCards");
    }

    private void Update()
    {
        //remove card from observation when player is done looking at it
        if (input.actions["ForwardCard"].triggered && curCardShown && canSpawnCardObj)
        {
            GetComponent<CardDrawingUIController>().HideCardInfoUI();
            curCardObj.GetComponent<Animator>().SetTrigger("forward_card");
            curCardObj.GetComponent<CardSpawner>().SpawnCardObj();
            canSpawnCardObj = false;
        }
    }

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
    private void DrawCard(Card.CardType type)
    {
        List<GameObject> filteredCards = possibleCards.Where(x=>x.GetComponent<Card>().cardType == type).ToList();
        GameObject randomlyPickedCard = filteredCards[Random.Range(0,filteredCards.Count)];

        GameObject cardObj = Instantiate(randomlyPickedCard, cardSpawnTransform);
        cardObj.transform.parent = cardSpawnTransform;
        curCardObj = cardObj;
    }

    //used by animation event to indicate that the current card is shown
    public void CardShownAnim()
    {
        curCardShown = true;
        canSpawnCardObj = true;

        GetComponent<CardDrawingUIController>().ShowCardInfoUI(curCardObj.GetComponent<Card>().cardName, curCardObj.GetComponent<Card>().cardDescription);
    }

    //used by animation event to indicate that current card is discarded
    public void CardDiscardedAnim()
    {
        Destroy(curCardObj);
        curCardLock = false;
        curCardShown = false;
    }
   
}
