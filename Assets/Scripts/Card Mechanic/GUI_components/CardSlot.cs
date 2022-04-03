using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour, IClickable
{
   [SerializeField] private GameObject Visual;
   private Card heldCard;

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
         heldCard = card.thisCard;
         heldCard.OnCardDestroyed += EmptySlot;
         Visual.SetActive(false);
         collider.enabled = false;
   }

   private void EmptySlot()
   {
      State = Slotstate.Empty;
      Visual.SetActive(true);
      collider.enabled = true;
      heldCard.OnCardDestroyed -= EmptySlot;
   }
}
