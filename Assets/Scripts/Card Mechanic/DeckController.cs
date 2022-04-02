using UnityEngine;
using System.Collections.Generic;

public class DeckController : MonoBehaviour {
        public List<Card> PlayerCardPool = new List<Card>();
        public List<Card> EnemyCardPool = new List<Card>();

        private string[] firstNames = {"Larry", "Davis"};
        private string[] lastNames = {"Williams", "Paulson"};
        private string[] flair = {"Of the flesh", "The destroyer", "The cuckholded"};

        private void Start() {
            PlayerCardPool = generateCardPool(10);
            EnemyCardPool  = generateCardPool(10);
            
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

        private Card generateCard(){
            //Generate a random name for the card
            string name = string.Format("{0} {1} {2}", firstNames[Random.Range(0,firstNames.Length)],
            lastNames[Random.Range(0,lastNames.Length)],
            flair[Random.Range(0,flair.Length)]);

            Debug.Log(name);
            return new Card(name, "Generic card", 1,1);
        }
}