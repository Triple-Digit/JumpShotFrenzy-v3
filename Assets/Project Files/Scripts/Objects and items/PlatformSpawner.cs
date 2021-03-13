using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] GameObject m_platforms;
    public float m_spawnTime = 3f;
    [SerializeField] bool m_gameStarted;



    private void Start()
    {
        m_gameStarted = true;
        StartCoroutine(TriggerSpawn());
    }

    void Spawn()
    {
        Instantiate(m_platforms, gameObject.transform);
    }

    IEnumerator TriggerSpawn()
    {
        while(m_gameStarted)
        {
            yield return new WaitForSeconds(m_spawnTime);
            Spawn();
        }
    }

}
