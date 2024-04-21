using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Transform player;
    [SerializeField] private Transform camera;
    private float _score;
    private void Start()
    {
        ScoreSystem.OnScoreChanged += CheckScore;
        CheckScore(ScoreSystem.Score);
    }
    
    void OnDestroy()
    {
        ScoreSystem.OnScoreChanged -= CheckScore;
    }

    private void CheckScore(int score)
    {
        _score=score;
        if (_score<=-1)
        {
            Time.timeScale = 0;
            inGameUI.SetActive(false);
            gameOverUI.SetActive(true);
        }
        
    }

    public void RestartGame()
    {
        ScoreSystem.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverUI.SetActive(false);
        inGameUI.SetActive(true);
        Time.timeScale = 1;
    }
    
    
}
