using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Vector2 inputVec;
    private float speed;

    Rigidbody2D rb;
    SpriteRenderer[] srs;
    Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        srs = GetComponentsInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        //speed = Player.Instance.Stats.MoveSpeed;
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec * Player.Instance.Stats.MoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }

    private void LateUpdate()
    {
        bool flip = inputVec.x < 0;

        anim.SetBool("isMove", inputVec != Vector2.zero);

        if (Mathf.Abs(inputVec.x) < 0.01f) return;

        srs[0].flipX = srs[1].flipX = flip;
        srs[1].transform.localPosition = new Vector3(flip ? 0.17f : -0.17f, -0.28f, 0);

    }
}
