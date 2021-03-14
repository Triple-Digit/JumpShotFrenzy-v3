using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton Method
    public static GameManager instance;


    void Singleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    #endregion

    #region Lists
    public List<AgentManager> m_activePlayers = new List<AgentManager>();
    List<string> m_levelOrder = new List<string>();
    List<int> m_roundWins = new List<int>();
    #endregion

    public string[] m_allLevels;
    public string m_winScene;
    

    [Header("Game State atrributes")]
    public int m_maxPlayers = 5;
    public bool m_canFight;
    public int m_pointsToWin;
    bool m_gameWon;

    [HideInInspector] public int m_lastPlayerNumber;
    [HideInInspector] public int m_playerNumber = 0;


    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        if(instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ActivatePlayers()
    {
        foreach(AgentManager player in m_activePlayers)
        {
            player.gameObject.SetActive(true);
            
        }
    }

    public void AddActivePlayer(AgentManager player)
    {
        instance.m_activePlayers.Add(player);
    }

    public void RemoveActivePlayer(AgentManager player)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>().m_shaking = false;
        instance.m_activePlayers.Remove(player);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>().m_shaking = true;
        
    }

    public int CheckActivePlayers()
    {
        int playerAliveCount = 0;

        for(int i = 0; i < m_activePlayers.Count; i++)
        {
            if(m_activePlayers[i].gameObject.activeInHierarchy)
            {
                playerAliveCount++;
                m_lastPlayerNumber = i;
            }
        }

        return playerAliveCount;
    }

    public void GoToNextArena()
    {
        if(!m_gameWon)
        {
            if (m_levelOrder.Count == 0)
            {
                List<string> allLevelList = new List<string>();
                allLevelList.AddRange(m_allLevels);

                for (int i = 0; i < m_allLevels.Length; i++)
                {
                    int selected = Random.Range(0, allLevelList.Count);

                    m_levelOrder.Add(allLevelList[selected]);
                    allLevelList.RemoveAt(selected);
                }
            }

            string levelToload = m_levelOrder[0];
            m_levelOrder.RemoveAt(0);

            foreach (AgentManager player in m_activePlayers)
            {
                player.gameObject.SetActive(true);
                player.TriggerControl();
                player.Respawn();
            }

            SceneManager.LoadScene(levelToload);
        }
        else
        {
            foreach (AgentManager player in m_activePlayers)
            {
                player.gameObject.SetActive(false);
            }
            SceneManager.LoadScene(m_winScene);
        }       

    }

    public void StartFirstRound()
    {
        m_roundWins.Clear();
        foreach(AgentManager player in m_activePlayers)
        {
            m_roundWins.Add(0);
        }

        m_gameWon = false;
        GoToNextArena();
    }

    public void AddRoundWin()
    {
        if(CheckActivePlayers() == 1)
        {
            m_roundWins[m_lastPlayerNumber]++;
            m_activePlayers[m_lastPlayerNumber].UpdateCrown();
            if(m_roundWins[m_lastPlayerNumber] >= m_pointsToWin)
            {
                m_gameWon = true;
            }

        }
    }


}
