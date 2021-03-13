using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool m_invicibility, m_doubleJump;
    public float m_powerUpLength = 5f;



    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            EnablePower(other.gameObject);
        }
        DestroyObject();
    }

    void EnablePower(GameObject player)
    {
        if (m_invicibility)
        {
            player.GetComponent<AgentManager>().Invincible(m_powerUpLength);
        }
        if (m_doubleJump)
        {
            player.GetComponent<PlayerController>().ExtraJump(m_powerUpLength);
        }
    }
}
