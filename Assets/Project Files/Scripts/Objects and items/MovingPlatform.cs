using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    
    [SerializeField] float m_moveSpeed = 3f, m_destroyTime = 5f;


    
    void Start()
    {
        
        Destroy(gameObject, m_destroyTime);
    }

   
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - m_moveSpeed * Time.deltaTime);
    }


}
