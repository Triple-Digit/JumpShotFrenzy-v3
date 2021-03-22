using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArenaManager : MonoBehaviour
{
    public List<Transform> m_spawnpoints = new List<Transform>();
    [SerializeField] bool m_roundOver;
    public bool m_start, m_suddenDeath, m_stopTimer;

    [SerializeField] TMP_Text m_playerWinText, m_countDownTimer, m_timer;
    [SerializeField] float m_totalMatchTime = 180f;
    float m_matchtime;

    [SerializeField] GameObject m_bullets;
    [SerializeField] float m_spawnTime;
    Vector2 m_screenBounds;

    private void Start()
    {
        SetUp();
    }


    void Update()
    {
        TrackMatch();
        CountDown();
    }

    void SetUp()
    {        
        GameManager.instance.ActivatePlayers();
        foreach (AgentManager player in GameManager.instance.m_activePlayers)
        {
            int randomPoint = Random.Range(0, m_spawnpoints.Count);
            player.transform.position = m_spawnpoints[randomPoint].position;
            if (GameManager.instance.m_activePlayers.Count <= m_spawnpoints.Count)
            {
                m_spawnpoints.RemoveAt(randomPoint);
            }
        }
        m_matchtime = 3f;
    }

    void TrackMatch()
    {
        if (GameManager.instance.CheckActivePlayers() == 1 && !m_roundOver)
        {
            m_suddenDeath = false;
            GameManager.instance.m_canFight = false;
            m_roundOver = true;
            StartCoroutine(EndRoundCo());
        }
    }


    IEnumerator EndRoundCo()
    {
        m_playerWinText.gameObject.SetActive(true);
        m_playerWinText.text = "Player " + (1 + GameManager.instance.m_lastPlayerNumber) + " wins!";             
        GameManager.instance.AddRoundWin();        
        yield return new WaitForSeconds(3f);
        GameManager.instance.GoToNextArena();
    }

    void CountDown()
    {
        if (m_stopTimer) return;
        m_matchtime -= Time.deltaTime;
        if (!m_start)
        {
            m_countDownTimer.gameObject.SetActive(true);
            m_timer.gameObject.SetActive(false);
            
            if(m_matchtime <= 0)
            { 

                m_start = true;
                GameManager.instance.m_canFight = true;
                foreach (AgentManager player in GameManager.instance.m_activePlayers)
                {
                    player.TriggerControl();                                        
                }
                m_matchtime = m_totalMatchTime;
            }
            m_countDownTimer.text = Mathf.CeilToInt(m_matchtime).ToString();
        }
        else
        {
            m_countDownTimer.gameObject.SetActive(false);
            m_timer.gameObject.SetActive(true);
            
            if(m_matchtime <= 0)
            {
                m_matchtime = 0; 
                m_playerWinText.text = "Sudden Death";                
                m_suddenDeath = true;
                m_stopTimer = true;
                StartCoroutine(ActivateSuddenDeath());
            }
            m_timer.text = Mathf.CeilToInt(m_matchtime).ToString();
        }        
    }

    IEnumerator ActivateSuddenDeath()
    {        
        while(m_suddenDeath)
        {
            yield return new WaitForSeconds(m_spawnTime);
            SuddenDeath();
        }        
    }

    void SuddenDeath()
    {        
        m_screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        GameObject a = Instantiate(m_bullets) as GameObject;
        a.transform.position = new Vector2(Random.Range(-m_screenBounds.x, m_screenBounds.x), m_screenBounds.y * 2);
    }


}
