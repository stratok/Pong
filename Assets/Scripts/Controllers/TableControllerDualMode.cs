using Photon.Pun;
using UnityEngine;

public class TableControllerDualMode : TableController
{
    protected override void SpawnBall(BallSettings settings)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        m_Ball = PhotonNetwork.Instantiate(m_BallPrefab.name, Vector2.zero, Quaternion.identity).GetComponent<BallDualMode>();
        m_Ball.Init(settings, m_TableSize);
        m_Ball.GetComponent<SpriteRenderer>().color = m_BallColor;
    }

    protected override void SpawnPaddle(PaddleSettings settings)
    {
        Vector2 position;

        if (PhotonNetwork.IsMasterClient)
        {
            position = Vector2.down * m_TableSize.y +
                       Vector2.up * settings.Indent +
                       Vector2.up * settings.Height * 0.5f;
        }
        else
        {
            position = Vector2.up * m_TableSize.y +
                       Vector2.down * settings.Indent +
                       Vector2.down * settings.Height * 0.5f;
        }

        m_Paddle = PhotonNetwork.Instantiate(m_PaddlePrefab.name, position, Quaternion.identity).GetComponent<PaddleDualMode>();
        m_Paddle.Setup(settings, m_TableSize);
    }
}
