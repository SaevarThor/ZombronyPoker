using UnityEngine;

public class BoardController : MonoBehaviour {
    int turn;

    Card[] playerHand = new Card[10];
    Card[] playerDeck = new Card[25];
    Card[] playeBoard = new Card[10];

    Card[] oponentHand = new Card[10];
    Card[] opponentBoard = new Card[10];

    private void Start() {
        
    }
}