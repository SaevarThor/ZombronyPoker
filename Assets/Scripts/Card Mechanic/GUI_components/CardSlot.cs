using System;
using UnityEngine;

public class CardSlot : MonoBehaviour, IClickable
{
   public GameObject Outlines;
   private Card heldCard;
   private GameObject alsoHeldCard;

   public bool IsEnemy;

   private GameObject curVis;

   public BoxCollider collider;

   private void Start()
   {
      if (IsEnemy) return;

      curVis = Instantiate(Outlines, transform.position, transform.rotation);
   }

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

         if (!IsEnemy)
         {
            Destroy(curVis);
            collider.enabled = false;
         }
         
         
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
      
      VfxManager.Instance.SpawnSkull(transform.position, transform.rotation);

      if (!IsEnemy)
      {
         if (Outlines == null)
         {
            Debug.LogError($"{gameObject.name} under {GetComponentInParent<Transform>().name} cant find outlines");
         }
         curVis = Instantiate(Outlines, transform.position, transform.rotation);
         if (collider == null && this.GetComponent<BoxCollider>() != null) 
            collider = GetComponent<BoxCollider>();
      }
      
      collider.enabled = true;
      heldCard.OnCardDestroyed -= EmptySlot;
      Destroy(alsoHeldCard);
   }

   public void OnDisable()
   {
      if (heldCard != null)
         heldCard.OnCardDestroyed -= EmptySlot;
   }
}
