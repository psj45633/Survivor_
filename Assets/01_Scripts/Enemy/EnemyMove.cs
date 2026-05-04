using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private float speed;

    Rigidbody2D rb;
    SpriteRenderer sr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

    }

    public void Init(float spd)
    {
        speed = spd;
    }

    private void FixedUpdate()
    {
        targetPos = Player.Instance.transform.position;
        Vector2 dir = (targetPos - rb.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);

        sr.flipX = dir.x < 0;
    }
}
