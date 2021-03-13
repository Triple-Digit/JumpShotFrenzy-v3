using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScene : MonoBehaviour
{
    [SerializeField] TMP_Text m_winText;
    public Image m_PlayerImage;
    public string m_mainMenuScene, m_colorSelectScene;

    
    void Start()
    {
        SetUP();
    }
       
    void SetUP()
    {
        m_winText.text = "Player " + (GameManager.instance.m_lastPlayerNumber + 1) + " Wins!";
        m_PlayerImage.material = GameManager.instance.m_activePlayers[GameManager.instance.m_lastPlayerNumber].GetComponent<AgentManager>().m_baseSprite.material;
    }

    public void Rematch()
    {
        GameManager.instance.StartFirstRound();
    }

    public void ChangeColor()
    {
        ClearGame();
        SceneManager.LoadScene(m_colorSelectScene);
    }

    public void MainMenu()
    {
        ClearGame();
        SceneManager.LoadScene(m_mainMenuScene);
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
