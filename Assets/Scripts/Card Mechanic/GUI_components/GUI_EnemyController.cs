using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GUI_EnemyController : MonoBehaviour {

    public GUI_PlayArea playArea;
    public GUI_HandInteractions GUIHand;
    public int cardSize = 5;
    
    private List<GameObject> allCards = new List<GameObject>();
    // State
    private List<Card> playerCardOnBoard = new List<Card>();
    private List<Card> myCardsOnBoard = new List<Card>();
    private List<Card> myCardsInHand = new List<Card>();

    [SerializeField] private CardSlot[] Slots;
    [SerializeField] private bool bossFight = false;

    private bool isDoing; 
     
    private void Start()
    {
        myCardsInHand = DeckController.Instance.generateOpponentDeck(cardSize, bossFight);
        Debug.Log($"Recieiving {myCardsInHand.Count} cards");

        foreach (var card in myCardsInHand)
        {
            card.state = CardState.OnHand;
            GameObject g = GUIHand.InstansiateNewCard(card);
            g.transform.tag = "OpponentCard";
            allCards.Add(g);
        }

        GUIHand.spaceCards();
        
        BoardController.Instance.InitializeTurn(this);
    }

    public void EnemyTurn()
    {
        if (isDoing) return;
        isDoing = true;
        float thinkTimer = Random.Range(1, 3);
        //Set zombie state to thinking

        StartCoroutine(ThinkAndDo(thinkTimer));
    }

    private IEnumerator ThinkAndDo(float thinkTimer)
    {
        yield return new WaitForSeconds(thinkTimer);
        updateState();
        Debug.Log($"Cum {myCardsInHand.Count}");
        
        CardSlot[] emptySlots = Slots.Where(s => s.State == CardSlot.Slotstate.Empty).ToArray();

        if (!ShouldAttack(emptySlots.Length))
        {
            Debug.Log("I wanna place a card");
            int randomIndex = Random.Range(0,myCardsInHand.Count);
            Card randomCard = myCardsInHand[randomIndex];
            GameObject cardObj = CardToObj(randomCard.CardId);
            emptySlots[0].Click(cardObj.GetComponent<GUI_CardInteraction>());
            GUIHand.RemoveFromHand(cardObj.transform);
            
            BoardController.Instance.PlayCard(randomCard);
            
            myCardsOnBoard.Add(randomCard);
            myCardsInHand.Remove(randomCard);
            
            CardSoundController.Instance.PlaceCard.Play();
        }
        else
        {
            Debug.Log($"target = {playerCardOnBoard.Count} vs attackers = {myCardsOnBoard.Count}");
            if (playerCardOnBoard.Count > 0 && myCardsOnBoard.Count > 0)
            {
                Card target = playerCardOnBoard[Random.Range(0,playerCardOnBoard.Count)];
                Card attacker = myCardsOnBoard[Random.Range(0,myCardsOnBoard.Count)];
                BoardController.Instance.Attack(attacker, target);
                CardSoundController.Instance.CardHit.Play();

            } else 
                Debug.LogError("No target to attack");
        }

        isDoing = false;
        BoardController.Instance.endTurn(CardFaction.Enemy);
    }

    private void updateState(){
        playerCardOnBoard.Clear();
        foreach(Card card in DeckController.Instance.PlayerDeck){
            
            if (card.state == CardState.OnBoard)
            {
                playerCardOnBoard.Add(card);
            }
        }

        List<Card> tempList = myCardsOnBoard.Where(c => c.state == CardState.OnBoard).ToList();
        myCardsOnBoard = tempList;

        List<GameObject> allCardsNew = new List<GameObject>();
        foreach (GameObject card in allCards){
            if (card != null)
            {
                allCardsNew.Add(card);
            }
        }
        allCards = allCardsNew;
    }

    private bool ShouldAttack(int emptySlots)
    {
        if (myCardsInHand.Count == 0 || emptySlots == 0) return true;
        
        if (emptySlots == 3) return false;
        
        int random = Random.Range(1, 5);
        return random > emptySlots;
    }

    private void placeCard(){

    }
    // Animate the drawing of a card
    private void drawCard()
    {
   /*     Debug.Log("drawing card");
        Card Drawn = BoardController.Instance.DrawCard(CardFaction.Enemy);
        if (Drawn != null){
            // Instansiate a card in the hand
            if (GUIHand != null)
            GUIHand.InstansiateNewCard(Drawn);
        } else {
            Debug.LogWarning("Trying to do gui-draw card but no card was drawn");
        }*/
    }

    //find a cardobj from cardID
    private GameObject CardToObj(string id){

        foreach (GameObject obj in allCards){
            Card card = obj.GetComponent<GUI_CardInteraction>().thisCard;
            if (card.CardId == id){
                return obj;
            }
        }

        return null;
    }
}