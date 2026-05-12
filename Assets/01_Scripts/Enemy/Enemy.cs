using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IAttacker, IDamageable
{
    [SerializeField] private BaseStatData statData;

    public Transform Transform => transform;

    public bool IsDead {  get; private set; }

    [Header("Stats")]
    public float maxHp;
    public float curHp;
    public float atk;
    public float atkSpd;
    public float def;

    [Header("Target")]
    public IDamageable target;
    public LayerMask targetMask;

    [Header("Reward")]
    [SerializeField] private int exp = 1;
    [SerializeField] private int gold = 1;

    [Header("Hit")]
    private SpriteRenderer sr;
    private Collider2D col;
    WaitForSeconds wait = new WaitForSeconds(0.05f);
    private Color orgColor = Color.white;
    private Color hitColor = Color.red;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }
    void OnEnable()
    {
        IsDead = false;
        Init();

        StartCoroutine(OnCollider());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    void Init()
    {
        var data = statData.levelStats[0];
        maxHp = data.maxHp;
        curHp = maxHp;
        atk = data.atk;
        atkSpd = data.atkSpeed;
        def = data.def;

        EnemyMove move = GetComponentInChildren<EnemyMove>();
        move.Init(data.moveSpeed);
    }

    public void Attack(IDamageable target)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        curHp -= damage;


        if (curHp < 0)
        {
            Die();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(HitFlashCoroutine());
        }
    }

    private void Die()
    {
        if (IsDead) return;

        IsDead = true;

        gameObject.SetActive(false);
        GameManager.OnKillEnemy?.Invoke(exp, gold);
    }

    private IEnumerator HitFlashCoroutine()
    {
        sr.color = hitColor;
        yield return wait;
        sr.color = orgColor;
    }
    
    private IEnumerator OnCollider()
    {
        yield return wait;
        col.enabled = true;
    }

}
