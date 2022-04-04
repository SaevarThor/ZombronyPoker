using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour, IClickable
{
   [SerializeField] private GameObject Visual;
   private Card heldCard;
   private GameObject alsoHeldCard;

   [SerializeField] private BoxCollider collider;
   
   public enum Slotstate
   {
      Empty,
      HasCard,
      Blocked,
   }

   public Slotstate State = Slotstate.Empty;

   public void Click(GUI_CardInteraction card)
   {
      if (card == null) return;
      
         State = Slotstate.HasCard;
         card.thisCard.state = CardState.OnBoard;
         heldCard = card.thisCard;
         heldCard.OnCardDestroyed += EmptySlot;
         Visual.SetActive(false);
         collider.enabled = false;
         Transform cardTrans = card.transform; 
         cardTrans.position = transform.position;
         cardTrans.rotation = transform.rotation;
         alsoHeldCard = cardTrans.gameObject;
         
         BoardController.Instance.endPlayerTurn();
   }

   private void EmptySlot()
   {
      State = Slotstate.Empty;
      heldCard.state = CardState.Destroyed;
      if (Visual == null)
         Visual = transform.GetChild(0).gameObject;
      Visual.SetActive(true);
      
      if (collider == null) 
         collider = GetComponent<BoxCollider>(); 
      
      collider.enabled = true;
      heldCard.OnCardDestroyed -= EmptySlot;
      Destroy(alsoHeldCard);
   }
}
