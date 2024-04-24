using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWonUI;
    [SerializeField] private Transform player; 
    [SerializeField] private GameObject gameCamera;
    [SerializeField] private GameObject finishCam;
    [SerializeField] private GameObject gameOverCam;

    public static event Action OnGameOver;
    
    private float _score;
    private Vector3 _cameraPosition;
    private Vector3 _cameraRotation;
    private CinemachineBrain _cinemachineBrain;
    private void Start()
    {
        _cinemachineBrain = gameCamera.GetComponent<CinemachineBrain>();
        _cameraPosition = gameCamera.transform.position;
        _cameraRotation = gameCamera.transform.eulerAngles;
        CheckScore(ScoreSystem.Score);
    }

    private void OnEnable()
    {
        ScoreSystem.OnScoreChanged += CheckScore;
        FinishLine.OnFinishLinePassed += GameWon;
    }

    void OnDisable()
    {
        ScoreSystem.OnScoreChanged -= CheckScore;
        FinishLine.OnFinishLinePassed -= GameWon;
    }

    private void CheckScore(int score)
    {
        _score=score;
        if (_score<=-1)
        {
           // Time.timeScale = 0;
           OnGameOver?.Invoke();
           gameOverCam.SetActive(true);
           _cinemachineBrain.enabled = true;
            inGameUI.SetActive(false);
            gameOverUI.SetActive(true);
        }
        
    }

    public void RestartGame()
    {
        ScoreSystem.Reset();
        gameOverUI.SetActive(false);
        inGameUI.SetActive(true);
        _cinemachineBrain.enabled = false;
        gameCamera.transform.position = _cameraPosition;
        gameCamera.transform.eulerAngles = _cameraRotation;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void GameWon()
    {
        _cinemachineBrain.enabled = true;
        finishCam.SetActive(true);
        inGameUI.SetActive(false);
        gameWonUI.SetActive(true);
    }
  
}
