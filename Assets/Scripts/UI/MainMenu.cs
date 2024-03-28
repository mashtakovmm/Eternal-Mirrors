using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject creditsTab;
    [SerializeField] GameObject mainMenuTab;
    public void OnStartClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCreditsClick()
    {
        mainMenuTab.SetActive(false);
        creditsTab.SetActive(true);
    }

    public void OnBackClick()
    {
        mainMenuTab.SetActive(true);
        creditsTab.SetActive(false);
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
