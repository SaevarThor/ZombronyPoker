using UnityEngine;
using System;

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
    public string CardId;
    public string CardName;
    public string Description;
    public int Health;
    public int Damage;
    public int Cost;
    public CardState state; 
    public CardType type; 
    public CardFaction faction; 
    public CardGender gender;

    public int CurrentHealth;

    //Events
    public delegate void OnCardDestroyedDelegate();
    public OnCardDestroyedDelegate OnCardDestroyed;
    public delegate void OnCardWasPlayedDelegate();
    public OnCardWasPlayedDelegate OnCardWasPlayed;
    
    public Card(string _cardName, 
    string _description, 
    int _health, 
    int _damage, 
    CardGender _gender, 
    CardFaction _faction, 
    CardType _type = CardType.Companion
    ){
        CardName = _cardName;
        Description = _description;
        Health = _health;
        Damage = _damage;
        state = CardState.InPool;
        gender = _gender;
        faction = _faction;
        type = _type;
        CurrentHealth = Health;
        CardId = Guid.NewGuid().ToString();
        calculateCost();
        
    }

    public void setCardState(CardState _state){
        state = _state;
        //Debug.Log(string.Format("{0} The state of {1} is now {2}", CardId, CardName, state));
    }
    // Give the card a cost based on its attributes
    private void calculateCost(){
        Cost = (int)Mathf.Floor(2f*Health + 2.5f*Damage);
    }

    public int Attack(Card targetCard){
        if (state == CardState.OnBoard){
            Debug.Log(string.Format("{0} attacked {1} for {2} damage",CardName, targetCard.CardName, Damage));
            targetCard.TakeDamage(Damage);
            // send a signal
            return 0;
        }
        //send a signal
        return -1;
    }

    public int TakeDamage(int damage){
        if (state == CardState.OnBoard) {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0){
                Debug.Log(string.Format("{0} took terminal damage", CardName));
                setCardState(CardState.Destroyed);

                //send a signal
                OnCardDestroyed();
            }
            // send a signal
            return 0;
        }
        // send a signal
        return -1;
    }

}
