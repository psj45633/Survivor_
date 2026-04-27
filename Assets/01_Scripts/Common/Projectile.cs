using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    

    BoxCollider2D col;
    Rigidbody2D rb;

    [Header("Shotgun")]
    public float distance;
    public float angle;
    public float speed;

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        
    }

    
}
