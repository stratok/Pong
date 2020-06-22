using DG.Tweening;
using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerSingleMode : MonoBehaviour
{
    [SerializeField] private Button m_ButtonPlay = null;
    [SerializeField] private Button m_ButtonExit = null;
    [SerializeField] private CanvasGroup m_ResultPanel = null;

    [SerializeField] private TextMeshProUGUI m_ScoreText = null;
    [SerializeField] private TextMeshProUGUI m_MaxScoreText = null;

    private readonly string m_ScoreTextDefault = "Score: ";
    private readonly string m_MaxScoreTextDefault = "Record: ";

    private UISettings m_Settings;

    public void Init(UISettings settings)
    {
        m_Settings = settings;

        EventController.OnScoreChanged += OnScoreChanged;

        m_ResultPanel.alpha = 0;

        m_ButtonPlay.onClick.AddListener(OnButtonRestartClick);
        m_ButtonExit.onClick.AddListener(OnButtonExitClick);
    }

    private void OnDestroy()
    {
        m_ButtonPlay.onClick.RemoveAllListeners();
        m_ButtonExit.onClick.RemoveAllListeners();

        EventController.OnScoreChanged -= OnScoreChanged;
    }

    public void StopGame() 
    {
        ShowMainPanel();
    }

    private void OnScoreChanged(int score, int maxScore)
    {
        m_ScoreText.text = СombineText(m_ScoreTextDefault, score);
        m_MaxScoreText.text = СombineText(m_MaxScoreTextDefault, maxScore);
    }

    private void OnButtonRestartClick()
    {
        m_ResultPanel.interactable = false;
        EventController.HandlePrepareGame();
        HideMainPanel(() => EventController.HandleStartGame());
    }

    private void OnButtonExitClick() 
    {
        EventController.HandleExitGame();
    }

    private void ShowMainPanel() {
        m_ResultPanel.DOFade(1, m_Settings.FadeTime)
                   .OnComplete(()=> { m_ResultPanel.interactable = true; });
    }

    private void HideMainPanel(Action callback = null)
    {
        m_ResultPanel.DOFade(0, m_Settings.FadeTime)
                   .OnComplete(() => { callback.SafeInvoke(); });
    }

    private string СombineText(string text, int value)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(text);
        builder.Append(value);

        return builder.ToString();
    }
}
