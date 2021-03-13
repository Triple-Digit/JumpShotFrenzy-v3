using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> m_playerConfigs;

    [SerializeField] int m_maxPlayers = 4;

    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            m_playerConfigs = new List<PlayerConfiguration>();
        }
    }

    public void SetPlayerColor(int index, Material color)
    {
        m_playerConfigs[index].m_playerMaterial = color;
    }

    public void ReadyUp(int index)
    {
        m_playerConfigs[index].m_isReady = true;
        if(m_playerConfigs.Count == m_maxPlayers&& m_playerConfigs.All(p => p.m_isReady == true))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        
        if(!m_playerConfigs.Any(p=> p.m_playerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            m_playerConfigs.Add(new PlayerConfiguration(pi));
        }


    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        m_playerIndex = pi.playerIndex;
        m_playerInput = pi;
    }
    public PlayerInput m_playerInput { get; set; }
    public int m_playerIndex { get; set; }
    public bool m_isReady { get; set; }
    public Material m_playerMaterial { get; set; }
}
