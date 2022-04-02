using UnityEngine;
using System.Collections.Generic;

public class DeckController : MonoBehaviour {
        public static DeckController Instance;

        public List<Card> PlayerDeck = new List<Card>();
        public List<Card> OpponentDeck = new List<Card>();

        public List<Card> PlayerCardPool = new List<Card>();
        public List<Card> OpponentCardPool = new List<Card>();

        private string[] firstNames = {"Larry", "Davis"};
        private string[] lastNames = {"Williams", "Paulson"};
        private string[] flair = {"Of the flesh", "The destroyer", "The cuckholded"};

        private int deckLimit = 20;

        private void Awake() {
            if (Instance != null && Instance != this){
                Destroy(this);
            } else {
                Instance = this;
            }
        }

        private void Start() {
            PlayerCardPool = generateCardPool(10);
            OpponentCardPool  = generateCardPool(10);
            buildOpponentDeck();
        }

        public int AddCardToDeck (Card card){
            if (card.state == CardState.InPool){
                // Add card to deck based on faction
                if (card.faction == CardFaction.player) {
                    PlayerDeck.Add(card);
                    card.setCardState(CardState.InDeck);
                } else{
                    OpponentDeck.Add(card);
                    card.setCardState(CardState.InDeck);
                }
                return 0;
            } else {
                // return error
                return -1;
            }
        }
        // intitialize the opponents deck
        private void buildOpponentDeck() {
                
        }

        private List<Card> generateCardPool (int poolSize){
            
            List<Card> pool = new List<Card>();
            int cardsLeft = poolSize;

            while(cardsLeft > 0){
               pool.Add(generateCard());
               cardsLeft --;
            }

            return pool;
        }
        // generate random player cards
        private Card generateCard(){
            //Generate a random name for the card
            string name = string.Format("{0} {1} {2}", 
            firstNames[Random.Range(0,firstNames.Length)],
            lastNames[Random.Range(0,lastNames.Length)],
            flair[Random.Range(0,flair.Length)]);

            // Create a new card with random stats 
            return new Card(name, "Generic card", Random.Range(1,10),Random.Range(1,10),CardGender.man, CardFaction.player, CardType.Companion);
            
        }
}