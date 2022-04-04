using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DeckController : MonoBehaviour {
        public static DeckController Instance;

        public List<Card> PlayerDeck = new List<Card>();
        public List<Card> OpponentDeck = new List<Card>();

        public List<Card> PlayerCardPool = new List<Card>();
        public List<Card> OpponentCardPool = new List<Card>();

        private string[] firstNames = {"Larry", "Davis", "Sævar", "Matias", "Ace", "Harald", "Gunther", "Heimdall", "Horson"};
        private string[] lastNames = {"Williams", "Paulson", "Sveinson", "Bye", "Boa", "Hundall", "California"};
        private string[] flair = {"Of the flesh", "The destroyer", "The cuckholded", "The sickly", "Of the valley", "Certified badass", "Of many moons", "Of the twin sun"};

        private int deckLimit = 5;

        public CardSwag Swag;

        private void Awake() {
            if (Instance != null && Instance != this){
                Destroy(this);
            } else {
                Instance = this;
            }

            PlayerCardPool = generateCardPool(deckLimit);
            OpponentCardPool  = generateOpponentPool(deckLimit);

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

        //generate an entire deck of cards for the opponent
        public List<Card> generateOpponentDeck (int size, bool isBossDeck = false)
        {
            List<Card> deck = new List<Card>();

            if (isBossDeck){
                size = 1; 
                deck.Add(CreateBossCard("The Player :O"));
                BoardController.Instance.CardsLeftOpponent = size;
                return deck;
            }
            
            BoardController.Instance.CardsLeftOpponent = size;
            
            for (int i = 0; i < size; i++)
            {
                Card card = generateEnemyCard();
                card.state = CardState.InDeck;
                deck.Add(card);
            }

            return deck;
        }

        // intitialize the opponents deck
        private List<Card> generateOpponentPool(int poolSize) {

            List<Card> pool = new List<Card>();
            int cardsLeft = poolSize;
            int id = 0;
            while (cardsLeft > 0){
                pool.Add(generateEnemyCard());
                cardsLeft --;
                id++; 
            }

            return pool;
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
        public Card generateCard(){
            //Generate a random name for the card
            string name = string.Format("{0} {1} {2}", 
            Swag.FirstNames[Random.Range(0,Swag.FirstNames.Length)],
            Swag.LastNames[Random.Range(0,Swag.LastNames.Length)],
            Swag.Flairs[Random.Range(0,Swag.Flairs.Length)]);

            string description = Swag.Description[Random.Range(0, Swag.Description.Length)];
            
            // Create a new card with random stats 
            return new Card(name, description, Random.Range(1,10),Random.Range(1,10),
                Swag.Pictures[Random.Range(0, Swag.Pictures.Length)],CardGender.man, CardFaction.player);
            
        }

        private Card generateEnemyCard(){
            //Generate a random name for the card
            string name = string.Format("{0} {1} {2}", 
            Swag.ZombieNames[Random.Range(0,Swag.ZombieNames.Length)],
            Swag.ZombieLastNames[Random.Range(0,Swag.ZombieLastNames.Length)],
            Swag.ZombieFliars[Random.Range(0,Swag.ZombieFliars.Length)]);

            string description = Swag.ZombieDescription[Random.Range(0, Swag.ZombieDescription.Length)];
            // Create a new card with random stats 
            
            Card gen = new Card(name, description, Random.Range(1,10),Random.Range(1,4),
                Swag.ZombiePictures[Random.Range(0, Swag.ZombiePictures.Length)],CardGender.man, CardFaction.Enemy);
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

    public Card CreateBossCard(string name){
        return new Card(name, 
        "The hunger has overcome you and now those you loved are now looking very much like your next meal", 
        20, 2,
        Swag.ZombiePictures[0], 
        CardGender.man, 
        CardFaction.Enemy);
    }
}