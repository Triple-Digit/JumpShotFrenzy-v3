using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColorSelect : MonoBehaviour
{
    [Tooltip("0 = Blue, " +
        "1 = Green, " +
        "2 = Orange, " +
        "3 = Purple, " +
        "4 = Red , " +
        "5 = White")]
    [SerializeField] int m_color;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            AgentManager player = other.GetComponent<AgentManager>();
            player.SetColor(m_color);
        }
    }
}
