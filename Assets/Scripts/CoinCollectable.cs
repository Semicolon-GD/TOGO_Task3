
using UnityEngine;

public class CoinCollectable : CollectibleBehaviour
{
    private readonly float[] _xLocations ={ -6f, 0f, 6f };
    
    private void Start()
    { 
        InvokeRepeating("PositionChange",1f,1f);
    }


    protected override void CollectibleEffect(Collider other)
    {
        base.OnCollect(); 
      
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            ScoreSystem.Add(1);
        }
    }
    
    
    private void PositionChange()
    {
        int randomIndex = Random.Range(0, _xLocations.Length);
        transform.position = new Vector3(_xLocations[randomIndex], transform.position.y, transform.position.z);
    }
}
