using UnityEngine;

public class Card {
    public string CardName;
    public string Description;
    public int Health;
    public int Damgae;
    public int Cost;

    public Card(string _cardName, string _description, int _health, int _damage){
        CardName = _cardName;
        Description = _description;
        Health = _health;
        Damgae = _damage;

        calculateCost();
    }

    // Give the card a cost based on its attributes
    private void calculateCost(){
        Cost = (int)Mathf.Floor(2f*Health + 2.5f*Damgae);
    }
}