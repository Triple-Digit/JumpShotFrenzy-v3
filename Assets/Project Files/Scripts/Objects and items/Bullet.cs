using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool m_moveDirection = false; //false = move right, true = move left
    public float m_moveSpeed;
    public float m_destroyTime;
    

    private void Awake()
    {
        StartCoroutine("DestroyByTime");
    }

    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(m_destroyTime);
        DestroyObject();
    }

    
    public void ChangeDirection()
    {
        m_moveDirection = true;
    }

    
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (!m_moveDirection)
        {
            transform.Translate(Vector3.right * m_moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * m_moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //add feedback
        DestroyObject();
    }

}
