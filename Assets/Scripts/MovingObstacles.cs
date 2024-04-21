
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
    
    private Vector3 _startPosition;
    private Vector3 _direction;
    private float _maxDistance;
    private void Awake()
    {
        _startPosition = transform.position;
    }
    
    private void Update()
    { 
        transform.Translate(_direction.normalized * (Time.deltaTime * speed));
        var distance= Vector3.Distance(_startPosition, transform.position);
        if(distance>_maxDistance)
        {
            transform.position = _startPosition + (_direction.normalized * _maxDistance);
            _direction *= -1;
        }
       
        
    }

    private void OnValidate()
    { 
        switch (moveDirections)
        {
            case MoveDirections.Null:
                break;
            case MoveDirections.Vertical:
                MoveVertical();
                break;
            case MoveDirections.Horizontal:
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
            _maxDistance = 10;
        } 
    }
    
    private void MoveHorizontal()
    {
        if (_startPosition!=Vector3.zero)
        {
            transform.position = _startPosition;
            _direction= Vector3.right;
            _maxDistance = 2.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.CompareTag("Player");
        if (player==false)   
            return;
        ScoreSystem.Add(-1);
        this.gameObject.SetActive(false);
        
    }
}


