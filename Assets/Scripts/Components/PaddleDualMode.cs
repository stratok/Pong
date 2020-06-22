using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PaddleDualMode : Paddle
{
    private PhotonView m_PhotonView;

    private void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    public override void PrepareGame() 
    {
        if (PhotonNetwork.IsMasterClient)
        {
            transform.position = Vector2.down * m_TableSize.y +
                                 Vector2.up * m_Settings.Indent +
                                 Vector2.up * m_Settings.Height * 0.5f;
        }
        else 
        {
            transform.position = Vector2.up * m_TableSize.y +
                                 Vector2.down * m_Settings.Indent +
                                 Vector2.down * m_Settings.Height * 0.5f;
        }
    }

    public override void Activate()
    {
        if (!m_PhotonView.IsMine)
            return;

        base.Activate();
    }
}
