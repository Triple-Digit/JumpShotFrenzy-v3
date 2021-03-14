using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AgentManager : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] PlayerController m_playerController;
    [SerializeField] GameObject m_character;
    [SerializeField] Material[] m_colors;
    public SpriteRenderer m_baseSprite;
    [SerializeField] Rigidbody2D m_body;

    [Header("Player Feedback")]
    [HideInInspector] public int m_currentColor;
    [SerializeField] GameObject m_explosion, m_playerNameHolder;
    [SerializeField] TMP_Text m_playerNameText;
    [SerializeField] GameObject m_crownBase, m_crownToppers, m_diamond;



    float m_invincibleCounter;
    int m_points;
    int m_playerID;


    private void Start()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.AddActivePlayer(this);
            GameManager.instance.m_playerNumber++;
            m_playerID = GameManager.instance.m_playerNumber;
            m_playerNameText.text = "P" + m_playerID;
            if (m_points == 0)
            {
                m_crownBase.SetActive(false);
                m_crownToppers.SetActive(false);
                m_diamond.SetActive(false);
            }
        }
        
    }

    private void Update()
    {
        InvincibilityTimer();
        DisplayName();
    }


    public void TriggerControl()
    {
        m_playerController.m_canControll = !m_playerController.m_canControll;
    }
    public void Respawn()
    {
        //get spawn points and move to that location and 
        m_character.SetActive(true);
    }

    public void Die()
    {
        if (m_invincibleCounter <= 0f)
        {
            m_character.SetActive(false);
            Camera.main.GetComponent<CameraControl>().m_shaking = false;
            Camera.main.GetComponent<CameraControl>().m_shaking = true;
            Instantiate(m_explosion, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            this.gameObject.SetActive(false);
        }
            
    }

    void DisplayName()
    {
        if (GameManager.instance.m_canFight)
        {
            m_playerNameHolder.SetActive(false);
        }
        else
        {
            m_playerNameHolder.SetActive(true);
        }
    }

    public void SetColor(int color)
    {
        m_baseSprite.material = m_colors[color];
        m_currentColor = color;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hazard") && GameManager.instance.m_canFight) Die();        
    }

    public void Invincible(float duration)
    {
        m_invincibleCounter += duration;
    }

    void InvincibilityTimer()
    {
        if(m_invincibleCounter >= 0f)
        {
            m_invincibleCounter -= Time.deltaTime;
        }
    }

    public void UpdateCrown()
    {
        m_points++;
        if(m_points == 1)
        {
            m_crownBase.SetActive(true);
        }
        if (m_points == 2)
        {
            m_crownBase.SetActive(true);
            m_crownToppers.SetActive(true);
        }
        if (m_points == 3)
        {
            m_crownBase.SetActive(true);
            m_crownToppers.SetActive(true);
            m_diamond.SetActive(true);
        }        
        else return;
    }



}
