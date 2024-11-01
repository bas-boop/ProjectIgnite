using Framework.Gameplay.MiniGames.Objects;
using UnityEngine;
using UnityEngine.Events;

public class SidewaysMovingCat : MonoBehaviour
{
    [SerializeField] private float movementStep = 0.5f;
    [SerializeField] private UnityEvent onMove = new();
    [SerializeField] private UnityEvent onComplete = new();

    private Vector3 _startPosition;
    private bool _canMove;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void OnReset()
    {
        transform.position = _startPosition;
    }
    public void ToggleMinigameActive(bool target)
    {
        _canMove = target;
    }
    public void PerformStep()
    {
        if (_canMove)
        {
            print("moving");
            //todo:  fix movement en visual turning
            //Vector3 nextStepPosition = new Vector3(transform.position.x - movementStep, transform.position.y , transform.position.z);
            onMove?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TurningEnemy turningEnemy = collision.GetComponent<TurningEnemy>();
        if (collision != null)
            onComplete?.Invoke();       
    }
}
