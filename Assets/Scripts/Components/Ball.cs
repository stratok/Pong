using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    protected Vector2 m_TableSize;
    protected BallSettings m_Settings;
    protected Rigidbody2D m_Rigidbody;
    protected Transform m_Transform;
    protected Coroutine m_ControlRoutine;

    protected readonly string PaddleTag = "Paddle";

    public virtual void Init(BallSettings settings, Vector2 tableSize) 
    {
        m_Settings = settings;
        m_TableSize = tableSize;
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Transform = transform;
    }

    public void PrepareGame() 
    {
        var scale = Random.Range(m_Settings.MinSize, m_Settings.MaxSize);
        transform.localScale = Vector3.one * scale;
        transform.position = Vector2.zero;
    }

    public void Activate()
    {
        if (m_ControlRoutine != null)
            return;

        Vector2 randomVector;
        float angleToX;

        do { 
            randomVector = Random.insideUnitCircle.normalized;
            angleToX = Vector2.Angle(randomVector, Vector2.right);
        }
        while (angleToX < m_Settings.MinAngleToX || angleToX > 180 - m_Settings.MinAngleToX);

        var randomSpeed = Random.Range(m_Settings.MinSpeed, m_Settings.MaxSpeed);
        m_Rigidbody.velocity = randomVector * randomSpeed;
        m_ControlRoutine = StartCoroutine(DoTask());
    }

    public void Deactivate()
    {
        if (m_ControlRoutine == null)
            return;

        StopCoroutine(m_ControlRoutine);
        m_Rigidbody.velocity = Vector2.zero;
        m_ControlRoutine = null;
    }

    private IEnumerator DoTask() 
    {
        while (true)
        {
            if (m_Transform.position.y + m_Transform.localScale.x * 0.5f <= -m_TableSize.y)
            {
                EventController.HandleStopGame(PlayerNumber.Player2);
                yield break;
            }
            else if (m_Transform.position.y - m_Transform.localScale.x * 0.5f >= m_TableSize.y)
            {
                EventController.HandleStopGame(PlayerNumber.Player1);
                yield break;
            }

            yield return null;
        }
    }

    protected void IncreaseVelocity()
    {
        m_Rigidbody.velocity *= 1 + m_Settings.SpeedStep;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PaddleTag))
            IncreaseVelocity();
    }
}