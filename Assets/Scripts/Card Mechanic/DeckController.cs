using UnityEngine;
using System.Collections.Generic;

public class DeckController : MonoBehaviour {
        public List<Card> PlayerCardPool = new List<Card>();
        public List<Card> EnemyCardPool = new List<Card>();

        private string[] firstNames = new string[10];
        private string[] lastNames = new string[10];

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

            return new Card("place", "holder", 1,1);
        }
}