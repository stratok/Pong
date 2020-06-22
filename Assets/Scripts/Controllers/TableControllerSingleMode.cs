using UnityEngine;

public class TableControllerSingleMode : TableController
{
    protected override void SpawnBall(BallSettings settings)
    {
        m_Ball = Instantiate(m_BallPrefab, transform).GetComponent<BallSingleMode>();
        m_Ball.Init(settings, m_TableSize);
        m_Ball.GetComponent<SpriteRenderer>().color = m_BallColor;
    }

    protected override void SpawnPaddle(PaddleSettings settings)
    {
        m_Paddle = Instantiate(m_PaddlePrefab, transform).GetComponent<PaddleSingleMode>();
        m_Paddle.Setup(settings, m_TableSize);
    }
}
