using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IAttacker, IDamageable
{
    [SerializeField] private BaseStatData statData;

    public Transform Transform => transform;

    [Header("Stats")]
    public float maxHp;
    public float curHp;
    public float atk;
    public float atkSpd;
    public float def;

    [Header("Target")]
    public IDamageable target;
    public LayerMask targetMask;

    [Header("Hit")]
    private SpriteRenderer sr;
    WaitForSeconds wait = new WaitForSeconds(0.05f);
    private Color orgColor = Color.white;
    private Color hitColor = Color.red;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }


    void Start()
    {
        Init();
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
        gameObject.SetActive(false);
    }

    private IEnumerator HitFlashCoroutine()
    {
        sr.color = hitColor;
        yield return wait;
        sr.color = orgColor;
    }
}
