using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    public void OnRestart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
