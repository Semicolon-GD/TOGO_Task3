using System;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public static event Action OnFinishLinePassed;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.CompareTag("Player");
        if (player==false)   
            return; 
        OnFinishLinePassed?.Invoke();
    }
}
