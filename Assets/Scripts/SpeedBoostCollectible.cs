using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedBoostCollectible : CollectibleBehaviour
{
   [SerializeField] private float _speedMultiplier = 2;

   protected override void CollectibleEffect(Collider other)
   {
      base.OnCollect(); 
      
      PlayerController playerController = other.GetComponent<PlayerController>();
      if (playerController != null)
      {
         playerController.ApplySpeedBoost(_speedMultiplier); 
      }
   }
}
