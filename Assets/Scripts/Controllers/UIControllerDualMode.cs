using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIControllerDualMode : MonoBehaviour
{
    [SerializeField] private RectTransform m_Start;
    [SerializeField] private TextMeshProUGUI m_Status;
    [SerializeField] private TextMeshProUGUI m_Player1Score;
    [SerializeField] private TextMeshProUGUI m_Player2Score;
    [SerializeField] private SceneController m_SceneController;

    private UISettings m_Settings;

    private readonly string m_ConnectingText = "Connecting...";
    private readonly string m_WaitingText = "Waiting for other player...";
    private readonly string m_StartText = "Start!";
    private readonly string m_Player1WinText = "Player 1 win!!!";
    private readonly string m_Player2WinText = "Player 2 win!!!";

    private void Awake()
    {
        m_Status.DOFade(1, 1);
        m_Status.text = m_ConnectingText;
    }

    public void Init(UISettings settings)
    {
        m_Settings = settings;
        UpdateScore(0, 0);
    }

    public void PrepareGame() 
    {
        m_Status.text = m_StartText;

        DOTween.Sequence()
            .Append(m_Start.DOScale(Vector2.one, m_Settings.AnimTime))
            .AppendInterval(m_Settings.Delay)
            .Append(m_Start.DOScale(Vector2.zero, m_Settings.AnimTime))
            .OnComplete(() => {
                EventController.HandleStartGame(); 
            });
    }

    public void ShowWinner(PlayerNumber player) 
    {
        switch (player)
        {
            case PlayerNumber.Player1:
                m_Status.text = m_Player1WinText;
                break;
            case PlayerNumber.Player2:
                m_Status.text = m_Player2WinText;
                break;
        }

        DOTween.Sequence()
            .Append(m_Status.DOFade(1, m_Settings.FadeTime))
            .AppendInterval(m_Settings.Delay)
            .OnComplete(() => EventController.HandleExitGame());
    }

    public void ShowWaitingText()
    {
        m_Status.text = m_WaitingText;
        m_Status.DOFade(1, m_Settings.FadeTime);
    }

    public void HideStatusText()
    {
        m_Status.DOFade(0, m_Settings.FadeTime);
    }

    public void UpdateScore(int player1Score, int Player2Score) 
    {
        m_Player1Score.text = player1Score.ToString();
        m_Player2Score.text = Player2Score.ToString();
    }
}
