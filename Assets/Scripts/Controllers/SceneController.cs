using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(Scenes scene) 
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
