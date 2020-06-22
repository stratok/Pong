using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Paddle : MonoBehaviour
{
    protected Rigidbody2D m_Rigidbody;
    protected Transform m_Transform;
    protected Camera m_Camera;
    protected PaddleSettings m_Settings;
    protected Vector2 m_TableSize;
    protected Coroutine m_ControlRoutine;

    public virtual void Setup(PaddleSettings settings, Vector2 tableSize) 
    {
        m_Settings = settings;
        m_Camera = Camera.main;
        m_TableSize = tableSize;
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Transform = transform;

        transform.localScale = new Vector3(m_Settings.Width, m_Settings.Height, 1);
    }

    public virtual void PrepareGame() { }

    public virtual void Activate()
    {
        if (m_ControlRoutine != null)
            return;
       
        m_ControlRoutine = StartCoroutine(Move());
    }
    public void Deactivate()
    {
        if (m_ControlRoutine == null)
            return;

        StopCoroutine(m_ControlRoutine);
        m_ControlRoutine = null;
    }
    private IEnumerator Move() 
    {
        while (true)
        {
#if UNITY_EDITOR
            var input = Input.GetAxis("Horizontal");

            if (input != 0)
            {
                var newPos = m_Transform.position + input * Vector3.right * m_Settings.Speed * Time.fixedDeltaTime;
                newPos.x = Mathf.Clamp(newPos.x, -m_TableSize.x * 0.5f + m_Settings.Width * 0.5f, m_TableSize.x * 0.5f - m_Settings.Width * 0.5f);

                m_Transform.position = newPos;
            }
#else 
            if (Input.touches.Length > 0) 
            {
                var newPos = m_Camera.ScreenToWorldPoint(Input.touches[0].position);

                newPos.z = m_Transform.position.z;
                newPos.y = m_Transform.position.y;
                newPos.x = Mathf.Clamp(newPos.x, -m_TableSize.x * 0.5f + m_Settings.Width * 0.5f, m_TableSize.x * 0.5f - m_Settings.Width * 0.5f);
                m_Transform.position = newPos;
            }
#endif

            yield return null;
        }
    }
}
