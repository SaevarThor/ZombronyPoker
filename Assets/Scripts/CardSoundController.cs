using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSoundController : MonoBehaviour
{
   public static CardSoundController Instance;
   public AudioSource CardHit;
   public AudioSource PlaceCard;
   
   private void Awake()
   {
      if (Instance != null && Instance != this)
         Destroy(this);
      else
         Instance = this;
   }
}
