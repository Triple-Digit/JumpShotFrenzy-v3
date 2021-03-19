using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] m_platforms;

    private void Awake()
    {
        int rand = Random.Range(0, m_platforms.Length);
        Instantiate(m_platforms[rand], transform.position, transform.rotation, transform);
    }    
}
