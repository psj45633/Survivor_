using System.Collections;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Init(Vector2 position)
    {
        transform.position = position;

        ps.Clear();
        ps.Play();
        StopAllCoroutines();
        StartCoroutine(HideCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        yield return new WaitForSeconds(ps.main.startLifetime.constantMax);

        gameObject.SetActive(false);
    }
}
