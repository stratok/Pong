using System;
using UnityEngine;

public class GameControllerSingleMode : MonoBehaviour
{
    [SerializeField] private GameSettings m_Settings = null;
    [SerializeField] private TableControllerSingleMode m_TableController = null;
    [SerializeField] private UIControllerSingleMode m_UISingleGameController = null;
    [SerializeField] private ScoreController m_ScoreController = null;
    [SerializeField] private SaveController m_SaveController = null;
    [SerializeField] private SceneController m_SceneController = null;

    private void Start()
    {
        EventController.PrepareGame += PrepareGame;
        EventController.StartGame += StartGame;
        EventController.StopGame += StopGame;
        EventController.ExitGame += ExitGame;
        EventController.OnBallСaught += AddScore;

        m_TableController.Init(m_Settings, m_SaveController.BallColor);
        m_ScoreController.Init(m_Settings.ScoreSettings, m_SaveController);
        m_UISingleGameController.Init(m_Settings.UISettings);

        PrepareGame();
        StartGame();
    }

    private void OnDestroy()
    {
        EventController.PrepareGame -= PrepareGame;
        EventController.StartGame -= StartGame;
        EventController.StopGame -= StopGame;
        EventController.ExitGame -= ExitGame;
        EventController.OnBallСaught += AddScore;
    }

    private void PrepareGame()
    {
        m_TableController.PrepareGame();
        m_ScoreController.PrepareGame();
    }

    private void StartGame() 
    {
        m_TableController.StartGame();
    }

    private void StopGame(PlayerNumber winner)
    {
        m_UISingleGameController.StopGame();
        m_TableController.StopGame();
        m_ScoreController.StopGame();
    }

    private void ExitGame() 
    {
        m_SceneController.LoadScene(Scenes.Menu);
    }

    private void AddScore()
    {
        m_ScoreController.AddSingleModeScore();
    }
}