using UnityEngine;
using System.Collections.Generic;

public class DeckController : MonoBehaviour {
        public static DeckController Instance;

        public List<Card> PlayerDeck = new List<Card>();
        public List<Card> OpponentDeck = new List<Card>();

        public List<Card> PlayerCardPool = new List<Card>();
        public List<Card> OpponentCardPool = new List<Card>();

        private string[] firstNames = {"Larry", "Davis", "Sævar", "Matias", "Ace", "Harald", "Gunther", "Heimdall", "Horson"};
        private string[] lastNames = {"Williams", "Paulson", "Sveinson", "Bye", "Boa", "Hundall", "California"};
        private string[] flair = {"Of the flesh", "The destroyer", "The cuckholded"};

        private int deckLimit = 4;

        private void Awake() {
            if (Instance != null && Instance != this){
                Destroy(this);
            } else {
                Instance = this;
            }

            PlayerCardPool = generateCardPool(5);
            OpponentCardPool  = generateOpponentPool(5);

            //For debug reasons we build a random deck 
            foreach (Card card in PlayerCardPool){
                AddCardToDeck(card);
            }
            foreach (Card card in OpponentCardPool){
                AddCardToDeck(card);
            }
        }

        private void Start() {
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
        private List<Card> generateOpponentPool(int poolSize) {

            List<Card> pool = new List<Card>();
            int cardsLeft = poolSize;
            int id = 0;
            while (cardsLeft > 0){
                pool.Add(generateEnemyCard(id));
                cardsLeft --;
                id++; 
            }

            return pool;
        }

        private List<Card> generateCardPool (int poolSize){
            
            List<Card> pool = new List<Card>();
            int cardsLeft = poolSize;
            int id = 0;
            while(cardsLeft > 0){
               pool.Add(generateCard(id));
               cardsLeft --;
               id ++;
            }

            return pool;
        }
        // generate random player cards
        private Card generateCard(int id){
            //Generate a random name for the card
            string name = string.Format("{0} {1} {2}", 
            firstNames[Random.Range(0,firstNames.Length)],
            lastNames[Random.Range(0,lastNames.Length)],
            flair[Random.Range(0,flair.Length)]);

            // Create a new card with random stats 
            return new Card(name, "Generic card", Random.Range(1,10),Random.Range(1,10),CardGender.man, CardFaction.player);
            
        }

        private Card generateEnemyCard(int id){
            //Generate a random name for the card
            string name = string.Format("{0} {1} {2}", 
            firstNames[Random.Range(0,firstNames.Length)],
            lastNames[Random.Range(0,lastNames.Length)],
            flair[Random.Range(0,flair.Length)]);

            // Create a new card with random stats 
            
            Card gen = new Card(name, "Generic card", Random.Range(1,10),Random.Range(1,4),CardGender.man, CardFaction.Enemy);
            return gen; 
        }
    
    public void removeCardFromPool(Card card){
        if (PlayerCardPool.Contains(card))
            PlayerCardPool.Remove(card);
    }

    public void removeCardFromDeck(Card card){
        if (PlayerDeck.Contains(card))
            PlayerDeck.Remove(card);
    }
}