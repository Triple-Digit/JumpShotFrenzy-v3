using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] GameObject m_joinText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckNumberOfPlayersJoined();
    }

    void CheckNumberOfPlayersJoined()
    {
        if(GameManager.instance.m_activePlayers.Count >= GameManager.instance.m_maxPlayers)
        {
            m_joinText.SetActive(false);
        }
        else
        {
            m_joinText.SetActive(true);
        }
    }
}
