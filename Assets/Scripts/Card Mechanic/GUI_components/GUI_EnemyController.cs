using System.Collections.Generic;
using UnityEngine;

public class GUI_EnemyController : MonoBehaviour {

    public GUI_PlayArea playArea;
    public GUI_HandInteractions GUIHand;
    private List<GameObject> allCards = new List<GameObject>();
    // State
    private List<Card> playerCardOnBoard = new List<Card>();
    private List<Card> myCardsOnBoard = new List<Card>();
    private List<Card> myCardsInHand = new List<Card>();
    private void Start() {

    }

    private void Update() {

        // Simple AI to make the enemy draw a card, play a card and attack a random card if any. 
        if(BoardController.Instance.OpponentTurn){
            //update state
            updateState();

            //draw card
            Card drawn = BoardController.Instance.DrawCard(CardFaction.Enemy);
            if (drawn != null){
                Debug.Log(string.Format("the enemy drew {0}", drawn.CardName));
                if (GUIHand != null)
                   allCards.Add(GUIHand.InstansiateNewCard(drawn));
            } else {
                Debug.LogWarning("Trying to do gui-draw card but no card was drawn");
            }

            updateState();
            //Play card
            int randomIndex = Random.Range(0,myCardsInHand.Count);
            if (myCardsInHand.Count > 0) {
                Debug.Log(string.Format("the enemy played {0}", myCardsInHand[randomIndex].CardName));
                // we place the Visuals first for a reason so the state is correct for updating the data
                GameObject cardObj = CardToObj(myCardsInHand[randomIndex].CardId);
                playArea.PlaceInPlayArea(cardObj.transform);
                GUIHand.RemoveFromHand(cardObj.transform);

                BoardController.Instance.PlayCard(myCardsInHand[randomIndex]);
            } else {
                Debug.Log("could not play a card");
            }

            updateState();
            //Attack
            if (playerCardOnBoard.Count > 0 && myCardsOnBoard.Count > 0){
                
                Card target = playerCardOnBoard[Random.Range(0,playerCardOnBoard.Count)];
                Card attacker = myCardsOnBoard[Random.Range(0,myCardsOnBoard.Count)];

                ////Debug.Log(string.Format("The opponent used {0} to attack {1}", attacker.CardName, target.CardName));
                BoardController.Instance.Attack(attacker, target);
            }

            BoardController.Instance.endTurn(CardFaction.Enemy);
        }
    }

    private void updateState(){
        playerCardOnBoard.Clear();
        foreach(Card card in DeckController.Instance.PlayerDeck){
            if (card.state == CardState.OnBoard){
                playerCardOnBoard.Add(card);
            }
        }

        myCardsOnBoard.Clear();
        foreach(Card card in DeckController.Instance.OpponentDeck){
            if (card.state == CardState.OnBoard){
                myCardsOnBoard.Add(card);
            }
        }


        myCardsInHand.Clear();
        foreach(Card card in DeckController.Instance.OpponentDeck){
            if (card.state == CardState.OnHand){
                myCardsInHand.Add(card);
            }
        }

       List<GameObject> allCardsNew = new List<GameObject>();
        foreach (GameObject card in allCards){
            if (card != null){
                allCardsNew.Add(card);
            }
        }
        allCards = allCardsNew;
    }

    private void placeCard(){

    }
    // Animate the drawing of a card
    private void drawCard(){
        Debug.Log("drawing card");
        Card Drawn = BoardController.Instance.DrawCard(CardFaction.Enemy);
        if (Drawn != null){
            // Instansiate a card in the hand
            if (GUIHand != null)
            GUIHand.InstansiateNewCard(Drawn);
        } else {
            Debug.LogWarning("Trying to do gui-draw card but no card was drawn");
        }
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