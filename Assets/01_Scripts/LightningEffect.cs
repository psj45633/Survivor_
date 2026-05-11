using System.Collections;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private float lifeTime = 0.08f;

    [SerializeField] private int segmentCount = 6;
    [SerializeField] private float randomOffset = 0.5f;

    WaitForSeconds wait;

    [SerializeField] private Gradient[] gradients;
    private void Start()
    {
        wait = new WaitForSeconds(lifeTime);
    }
    public void Init(Vector2 start, Vector2 end)
    {
        line.positionCount = segmentCount;

        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)(segmentCount - 1);

            Vector2 pos = Vector2.Lerp(start, end, t);

            // 시작/끝 제외하고 흔들기
            if (i != 0 && i != segmentCount - 1)
            {
                pos += Random.insideUnitCircle * randomOffset;
            }

            line.SetPosition(i, pos);
        }

        SetRandomColor();

        StopAllCoroutines();
        StartCoroutine(DisableAfter());
    }

    private IEnumerator DisableAfter()
    {
        yield return wait;
        gameObject.SetActive(false);
    }
    private void SetRandomColor()
    {
        if (gradients == null || gradients.Length == 0) return;

        line.colorGradient = gradients[Random.Range(0, gradients.Length)];
    }
}
