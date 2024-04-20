using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float verticalMovementSensitivity = 1;
    
    private GameObject _player;
    private Collider _playerCollider;
    private Material _playerMaterial;

    private float _verticalSpeed=0;
    private void Awake()
    {
        _player = this.gameObject;
        _playerCollider = _player.GetComponent<Collider>();
        _playerMaterial = _player.GetComponent<Renderer>().material;
    }

    #region Event Subscription

    private void OnEnable()
    {
        InputController.OnFirstClick += StartMovement;
        InputController.Dragging += HorizontalMovement;
    }
    

    private void OnDisable()
    {
        InputController.OnFirstClick -= StartMovement;
        InputController.Dragging -= HorizontalMovement;
    }

    
    #endregion

    #region Event Methods

    private void StartMovement()
    {
        _verticalSpeed = 3;
    }
    private void HorizontalMovement(float horizontal)
    {
        _player.transform.position += Vector3.right * (horizontal * verticalMovementSensitivity * Time.deltaTime);
    }

    private void Update()
    {
        _player.transform.position+=Vector3.forward * (_verticalSpeed * Time.deltaTime);
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
        _playerMaterial.color = Color.red;
        yield return new WaitForSeconds(2);
        _playerCollider.enabled = true;
        _playerMaterial.color = Color.white;
    }
    
    #endregion
    
}
