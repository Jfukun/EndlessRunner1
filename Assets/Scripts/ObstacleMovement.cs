using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public enum MoveAxis { UpDown, LeftRight }

    [Header("Movement")]
    public MoveAxis m_Axis = MoveAxis.UpDown;
    public float m_Speed = 3f;
    public float m_Range = 2f;   

    private Vector3 m_StartPosition;

    private void OnEnable()
    {
        m_StartPosition = transform.localPosition;
    }

    private void Update()
    {
        float offset = Mathf.Sin(Time.time * m_Speed) * m_Range;

        if (m_Axis == MoveAxis.UpDown)
            transform.localPosition = m_StartPosition + Vector3.up * offset;
        else
            transform.localPosition = m_StartPosition + Vector3.right * offset;
    }
}
