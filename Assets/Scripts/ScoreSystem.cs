using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;
    
    public static int Score { get; private set; }
    
    void Start()
    {
        Score = 0;
    }
    
    public static void Add(int points)
    {
        Score += points;
        OnScoreChanged?.Invoke(Score);
    }
    
    public static void Reset()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);
    }
}
