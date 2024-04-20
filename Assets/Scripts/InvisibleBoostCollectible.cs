using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBoostCollectible : CollectibleBehaviour
{
    protected override void CollectibleEffect(Collider other)
    {
        base.OnCollect(); 
      
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ApplyInvincibilityBoost(); 
        }
    }
}
