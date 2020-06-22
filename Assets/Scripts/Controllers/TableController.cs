using UnityEngine;

public class TableController : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_BallPrefab = null;
    [SerializeField]
    protected GameObject m_PaddlePrefab = null;

    protected Camera m_Camera;
    protected Vector2 m_TableSize;

    protected Paddle m_Paddle;
    protected Ball m_Ball;
    protected Color m_BallColor;

    protected void Awake()
    {
        m_Camera = Camera.main;
    }

    public virtual void Init(GameSettings settings, Color ballColor)
    {
        m_BallColor = ballColor;

        m_TableSize.y = m_Camera.orthographicSize;
        m_TableSize.x = settings.TableSettings.TableWidth;

        SpawnPaddle(settings.PaddleSettings);
        SpawnBall(settings.BallSettings);
    }

    public void PrepareGame()
    {
        m_Paddle.PrepareGame();
        m_Ball?.PrepareGame();
    }

    public void StartGame() 
    {
        m_Paddle.Activate();
        m_Ball?.Activate();
    }

    public void StopGame() 
    {
        m_Paddle.Deactivate();
        m_Ball?.Deactivate();
    }

    protected virtual void SpawnBall(BallSettings settings) { }
    protected virtual void SpawnPaddle(PaddleSettings settings) { }
}
