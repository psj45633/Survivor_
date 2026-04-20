using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Vector2 inputVec;
    [SerializeField] private float speed;

    Rigidbody2D rb;
    SpriteRenderer sr;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }

    private void LateUpdate()
    {
        if (Mathf.Abs(inputVec.x) < 0.01f) return;

        sr.flipX = inputVec.x < 0;
    }
}
