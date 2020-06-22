using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuController : MonoBehaviour
{
    [SerializeField] private SceneController m_SceneController = null;

    [SerializeField] private Button m_ButtonSolo = null;
    [SerializeField] private Button m_ButtonDuel = null;
    [SerializeField] private Button m_ButtonSettings = null;

    public void Awake()
    {
        m_ButtonSolo.onClick.AddListener(StartSolo);
        m_ButtonDuel.onClick.AddListener(StartDuel);
        m_ButtonSettings.onClick.AddListener(ShowSettings);
    }

    private void StartSolo() 
    {
        m_SceneController.LoadScene(Scenes.Single);
    }

    private void StartDuel() 
    {
        m_SceneController.LoadScene(Scenes.Dual);
    }

    private void ShowSettings()
    {
        m_SceneController.LoadScene(Scenes.Settings);
    }

    private void OnDestroy()
    {
        m_ButtonSolo.onClick.RemoveAllListeners();
        m_ButtonDuel.onClick.RemoveAllListeners();
        m_ButtonSettings.onClick.RemoveAllListeners();
    }
}
