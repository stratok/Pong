using UnityEngine;
using UnityEngine.UI;

public class UISettingsController : MonoBehaviour
{
    [SerializeField] private Button[] m_ColorButtons = null;
    [SerializeField] private Button m_ButtonExit = null;
    [SerializeField] private SaveController m_SaveController = null;
    [SerializeField] private SceneController m_SceneController = null;
    [SerializeField] private Image m_BallImage = null;

    private Color m_CurrentColor = Color.white;

    private void Start()
    {
        foreach (var button in m_ColorButtons)
            button.onClick.AddListener(() => OnButtonChangeColorClick(button));

        m_ButtonExit.onClick.AddListener(OnButtonExitClick);
        m_CurrentColor = m_SaveController.BallColor;
        m_BallImage.color = m_CurrentColor;
    }

    private void OnDestroy()
    {
        foreach (var button in m_ColorButtons)
            button.onClick.RemoveAllListeners();

        m_ButtonExit.onClick.RemoveAllListeners();
    }

    private void OnButtonChangeColorClick(Button button) 
    {
        var color = button.GetComponent<Image>().color;
        m_CurrentColor = color;
        m_BallImage.color = color;
    }

    private void OnButtonExitClick()
    {
        m_SaveController.BallColor = m_CurrentColor;
        m_SceneController.LoadScene(Scenes.Menu);
    }
}
