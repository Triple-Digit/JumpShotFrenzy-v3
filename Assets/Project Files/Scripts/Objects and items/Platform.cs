using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Attributes")]
    public bool m_bounce;
    bool m_break;
    /*
    [SerializeField] float m_bounceForce = 5f; 
    [SerializeField] float m_DestroyTime = 0.5f;
    [SerializeField] float m_RespawnTime = 3f;
    */

    [Header("Components")]
    [SerializeField] SpriteRenderer m_sprite;
    [SerializeField] BoxCollider2D m_collider;
    [SerializeField] 


    private void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {

        }
    }


    void React()
    {
        if(GameManager.instance.m_canFight == true)
        {
            if(m_bounce)
            {

            }

            else
            {
                m_break = true;
            }
        }
    }


    void Break()
    {
        if (!m_break) return;

        //m_sprite.color = new Vector3(m_sprite.color.r)


    }
}
