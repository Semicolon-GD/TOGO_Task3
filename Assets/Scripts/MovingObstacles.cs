
using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
    enum MoveDirections
    {
        Null,
        Vertical,
        Horizontal
    }
    
    [SerializeField] private MoveDirections moveDirections;
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    
    private Vector3 _startPosition;
    private Vector3 _direction;
    private float _maxDistance;
    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Start()
    {
        if (moveDirections == MoveDirections.Horizontal)
        {
            speed = 2;
            MoveHorizontal();
        }
        if (moveDirections == MoveDirections.Vertical)
        {
            speed = 2;
            MoveVertical();
        }
    }

    private void Update()
    {
       
        transform.Translate(_direction.normalized * (Time.deltaTime * speed),Space.World);
        var distance= Vector3.Distance(_startPosition, transform.position);
        if(distance>_maxDistance)
        {
           // transform.position = _startPosition + (_direction.normalized * _maxDistance);
            _direction *= -1;
            transform.forward = _direction*-1;
        }
    }

    private void OnValidate()
    { 
        switch (moveDirections)
        {
            case MoveDirections.Null:
                if (_startPosition!=Vector3.zero)
                {
                    transform.position = _startPosition;
                    speed = 0;   
                }
                break;
            case MoveDirections.Vertical:
                speed = 3;
                MoveVertical();
                break;
            case MoveDirections.Horizontal:
                speed = 3;
                MoveHorizontal();
                break;
        }
    }

    private void MoveVertical()
    {
        if (_startPosition!=Vector3.zero)
        {
            transform.position = _startPosition;
            _direction= Vector3.forward;
            transform.forward = _direction*-1;
            _maxDistance = 7;
        } 
    }
    
    private void MoveHorizontal()
    {
        if (_startPosition!=Vector3.zero)
        {
            transform.position = _startPosition;
            _direction= Vector3.right;
            transform.forward = _direction*-1;
            _maxDistance = 5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.CompareTag("Player");
        if (player==false)   
            return;
        animator.SetTrigger("PlayerCollision");
        ScoreSystem.Add(-1);
        speed = 0;

    }
}


