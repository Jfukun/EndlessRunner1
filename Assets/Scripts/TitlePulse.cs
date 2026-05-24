using UnityEngine;

public class TitlePulse : MonoBehaviour
{
    public float m_ScaleSpeed = 2f;
    public float m_MinScale = 0.9f;
    public float m_MaxScale = 1.15f;

    private Vector3 m_OriginalScale;

    void Start()
    {
        m_OriginalScale = transform.localScale;
    }

    void Update()
    {
        float pulse = (Mathf.Sin(Time.time * m_ScaleSpeed) + 1f) / 2f;

        float currentScaleMultiplier = Mathf.Lerp(m_MinScale, m_MaxScale, pulse);

        transform.localScale = m_OriginalScale * currentScaleMultiplier;
    }
}