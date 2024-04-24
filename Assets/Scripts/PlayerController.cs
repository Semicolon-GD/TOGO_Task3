using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float verticalMovementSensitivity = 1;
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject deathCam;
    [SerializeField] private GameObject tutorialUI;

   
    
    private GameObject _player;
    private Collider _playerCollider;
    private Material _playerMaterial;
    private Vector3 _deathPosition;
    private float _verticalSpeed=0;
    private void Awake()
    {
        _player = this.gameObject;
        _playerCollider = _player.GetComponent<Collider>();
        _playerMaterial = playerRenderer.material;
    }
    private void Update()
    {
        _player.transform.position+=Vector3.forward * (_verticalSpeed * Time.deltaTime);
    }

    #region Event Subscription

    private void OnEnable()
    {
        InputController.OnFirstClick += StartMovement;
        InputController.Dragging += HorizontalMovement;
        GameManager.OnGameOver += GameOver;
        FinishLine.OnFinishLinePassed += GameWon;
    }
    

    private void OnDisable()
    {
        InputController.OnFirstClick -= StartMovement;
        InputController.Dragging -= HorizontalMovement;
        GameManager.OnGameOver -= GameOver;
        FinishLine.OnFinishLinePassed -= GameWon;
    }

    
    #endregion

    #region Event Methods

    private void StartMovement()
    {
        tutorialUI.SetActive(false);
        _verticalSpeed = 10;
        playerAnimator.SetBool("isGameStarted",true);
    }
    private void HorizontalMovement(float horizontal)
    {
        _player.transform.position += Vector3.right * (horizontal * verticalMovementSensitivity * Time.deltaTime);
    }

    private void GameOver()
    {
        playerAnimator.SetTrigger("Death");
        _verticalSpeed = 0;
        _deathPosition = _player.transform.position;
        deathCam.transform.position = new Vector3(deathCam.transform.position.x, deathCam.transform.position.y, _deathPosition.z);
        InputController.Dragging -= HorizontalMovement;
    }
    
    public void GameWon()
    {
        
            playerAnimator.SetTrigger("Win");
        
        _verticalSpeed = 0;
        InputController.Dragging -= HorizontalMovement;
    }

    
    #endregion

    #region Collectible Metods

    public void ApplySpeedBoost(float speedMultiplier)
    {
        StartCoroutine(SpeedBoost(speedMultiplier));
    }   
    
    public void ApplyInvincibilityBoost()
    {
        StartCoroutine(Invincibility());
    }

    public void AddScore()
    {
        Debug.Log("Score Added!");
    }
    
   
    #endregion

    #region Coroutines
 
    IEnumerator SpeedBoost(float speedMultiplier)
    {
        _verticalSpeed *= speedMultiplier;
        yield return new WaitForSeconds(2);
        _verticalSpeed /= speedMultiplier;
    }
    
    IEnumerator Invincibility()
    {
        _playerCollider.enabled = false;
        _playerMaterial.color = Color.blue;
        yield return new WaitForSeconds(2);
        _playerCollider.enabled = true;
        _playerMaterial.color = Color.white;
    }
    
    #endregion
    
}
