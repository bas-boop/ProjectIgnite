using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start() => rb = GetComponent<Rigidbody2D>();

    private void Update() => movement = new (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    private void FixedUpdate() => rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
}