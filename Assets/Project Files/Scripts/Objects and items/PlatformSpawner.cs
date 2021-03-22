using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] GameObject m_platforms;
    public float m_spawnTime = 3f;
    float m_timer;
    [SerializeField] bool m_gameStarted;


    private void Update()
    {
        m_gameStarted = GameManager.instance.m_canFight;
        m_timer -= Time.deltaTime;
        if(m_gameStarted && m_timer <= 0f)
        {
            m_timer = m_spawnTime;
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(m_platforms, gameObject.transform);
    }

    

}
