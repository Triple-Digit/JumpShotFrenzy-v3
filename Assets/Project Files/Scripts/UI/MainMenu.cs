using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] string m_firstScene;

    public void StartGame()
    {
        SceneManager.LoadScene(m_firstScene);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
