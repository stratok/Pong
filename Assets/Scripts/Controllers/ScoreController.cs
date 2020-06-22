using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private SaveController m_SaveController;
    private int m_CurrentScore;
    private int m_RecordScore;
    private ScoreSettings m_Settings;

    public void Init(ScoreSettings settings, SaveController saveController) 
    {
        m_Settings = settings;
        m_SaveController = saveController;

        m_RecordScore = m_SaveController.MaxSingleScore;
    }

    public void AddSingleModeScore()
    {
        m_CurrentScore += m_Settings.ScoreForСaught;

        if (m_CurrentScore > m_RecordScore)
            m_RecordScore = m_CurrentScore;

        EventController.HandleScoreChanged(m_CurrentScore, m_RecordScore);
    }

    public void PrepareGame() 
    {
        m_CurrentScore = 0;
       

        EventController.HandleScoreChanged(m_CurrentScore, m_RecordScore);
    }

    public void StopGame() 
    {
        if (m_RecordScore > m_SaveController.MaxSingleScore)
            m_SaveController.MaxSingleScore = m_RecordScore;
    }

    private void OnApplicationQuit()
    {
        if (m_RecordScore > m_SaveController.MaxSingleScore)
            m_SaveController.MaxSingleScore = m_RecordScore;
    }
}
