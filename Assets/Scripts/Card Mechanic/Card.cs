using UnityEngine;

public enum CardState : int {
    InPool = 0,
    InDeck = 1,
    OnHand = 2,
    OnBoard = 3, 
    Destroyed = 4,
}

public enum CardFaction : int {
    player = 0,
    Enemy = 1,
}

public enum CardType : int {
    Companion = 0,
    Object = 1,
}

public enum CardGender : int {
    man = 0,
    woman = 1,
}

public class Card {
    public int CardId;
    public string CardName;
    public string Description;
    public int Health;
    public int Damgae;
    public int Cost;
    public CardState state; 
    public CardType type; 
    public CardFaction faction; 
    public CardGender gender;
    public int DamageTaken;

    public Card(string _cardName, 
    string _description, 
    int _health, 
    int _damage, 
    CardGender gender, 
    CardFaction _faction = CardFaction.player, 
    CardType _type = CardType.Companion){
        CardName = _cardName;
        Description = _description;
        Health = _health;
        Damgae = _damage;
        state = CardState.InPool;
        DamageTaken = 0;
        calculateCost();
    }

    public void setCardState(CardState _state){
        state = _state;
    }
    // Give the card a cost based on its attributes
    private void calculateCost(){
        Cost = (int)Mathf.Floor(2f*Health + 2.5f*Damgae);
    }

}