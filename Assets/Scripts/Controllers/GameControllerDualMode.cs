using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameControllerDualMode : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameSettings m_Settings = null;
    [SerializeField] private TableControllerDualMode m_TableController = null;
    [SerializeField] private UIControllerDualMode m_UIController = null;
    [SerializeField] private SaveController m_SaveController = null;
    [SerializeField] private SceneController m_SceneController = null;

    private int m_Player1Score;
    private int m_Player2Score;

    private PhotonView m_PhotonView;

    private void Awake()
    {
        m_PhotonView = PhotonView.Get(this);
        
        EventController.PrepareGame += PrepareGame;
        EventController.StartGame += StartGame;
        EventController.StopGame += StopGame;
        EventController.ExitGame += ExitGame;

        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();

        m_UIController.Init(m_Settings.UISettings);
    }

    private void OnDestroy()
    {
        EventController.PrepareGame -= PrepareGame;
        EventController.StartGame -= StartGame;
        EventController.StopGame -= StopGame;
        EventController.ExitGame -= ExitGame;
    }

    private void Init()
    {
        m_Player1Score = 0;
        m_Player2Score = 0;

        m_TableController.Init(m_Settings, m_SaveController.BallColor);
    }

    private void PrepareGame() 
    {
        if (PhotonNetwork.IsMasterClient)
            m_PhotonView.RPC("PrepareGameRPC", RpcTarget.All);
    }
    private void StartGame() 
    {
        if (PhotonNetwork.IsMasterClient)
            m_PhotonView.RPC("StartGameRPC", RpcTarget.All);
    }
    private void StopGame(PlayerNumber winner) 
    {
        if (PhotonNetwork.IsMasterClient)
            m_PhotonView.RPC("StopGameRPC", RpcTarget.All, winner);
    }

    [PunRPC]
    private void PrepareGameRPC()
    {
        m_TableController.PrepareGame();
        m_UIController.PrepareGame();
    }

    [PunRPC]
    private void StartGameRPC()
    {
        m_TableController.StartGame();
    }

    [PunRPC]
    private void StopGameRPC(PlayerNumber winner)
    {
        m_TableController.StopGame();

        switch (winner)
        {
            case PlayerNumber.Player1:
                m_Player1Score++;
                break;
            case PlayerNumber.Player2:
                m_Player2Score++;
                break;
        }

        m_UIController.UpdateScore(m_Player1Score, m_Player2Score);

        if (m_Player1Score >= m_Settings.ScoreForWin)
            m_UIController.ShowWinner(PlayerNumber.Player1);
        else if (m_Player2Score >= m_Settings.ScoreForWin)
            m_UIController.ShowWinner(PlayerNumber.Player2);
        else
            PrepareGame();
    }

    private void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        m_SceneController.LoadScene(Scenes.Menu);
    }

    public override void OnConnectedToMaster()
    {
        m_UIController.ShowWaitingText();
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogWarning("OnJoinRandomFailed");
        PhotonNetwork.CreateRoom("TestRoom" + Random.Range(0, 10000), new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        Debug.LogWarning("OnJoinedRoom");

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            m_UIController.HideStatusText();
            Init();
            PrepareGame();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2) 
        {
            m_UIController.HideStatusText();
            Init();
            PrepareGame();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ExitGame();
    }
}
