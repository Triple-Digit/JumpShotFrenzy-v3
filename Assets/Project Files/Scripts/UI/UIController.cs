using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    #region Singleton
    public static UIController instance;
    void Singleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    private void Awake()
    {
        Singleton();
    }
    #endregion

    [SerializeField] string m_mainMenu;
    [SerializeField] GameObject m_pause;
    public GameObject m_firstButtonInPause;
    
    void Update()
    {
        PauseButtonPress();
    }

    void PauseButtonPress()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame)
        {
            PauseUnpause();
        }        
    }

    public void PauseUnpause()
    {
        if(m_pause.activeInHierarchy)
        {
            m_pause.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            m_pause.SetActive(true);
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_firstButtonInPause);
        }
    }

    public void MainMenu()
    {
        ClearGame();
        SceneManager.LoadScene(m_mainMenu);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ClearGame()
    {
        foreach (AgentManager player in GameManager.instance.m_activePlayers)
        {
            Destroy(player.gameObject);
        }

        Destroy(GameManager.instance.gameObject);
        GameManager.instance = null;
    }
}
