using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    bool isInitialized;

    Vector2 dir;
    float damage;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask targetMask;

    float offset = -90f;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 pos, Vector2 dir, float damage, float lifeTime = 3f)
    {
        transform.position = pos;
        rb.position = pos;
        this.dir = dir.normalized;
        this.damage = damage;

        float angle = Mathf.Atan2(this.dir.y, this.dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

        isInitialized = true;

    }

    private void OnEnable()
    {
        isInitialized = false;
        dir = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
    }

    private void OnDisable()
    {
        isInitialized = false;
        dir = Vector2.zero;
        damage = 0f;
    }

    void FixedUpdate()
    {
        if (!isInitialized) return;

        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetMask) == 0)
        {
            return;
        }

        if (collision.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(damage);
        }

        gameObject.SetActive(false);
    }


}
