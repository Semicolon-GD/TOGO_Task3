
using UnityEngine;

public abstract class CollectibleBehaviour : MonoBehaviour
{
  
   private void OnTriggerEnter(Collider other)
   {
      var player = other.CompareTag("Player");
      if (player==false)   
         return;
      OnCollect();
      CollectibleEffect(other);
   }

   protected virtual void OnCollect()
   {
      GetComponent<Collider>().enabled = false;
      Destroy(gameObject);
   }

   protected abstract void CollectibleEffect(Collider other);
}
