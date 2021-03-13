using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Platform data")]
    [SerializeField] Transform[] m_spawnPoints;
    [SerializeField] GameObject[] m_platforms;

    [SerializeField] float m_moveSpeed = 3f, m_destroyTime = 5f;


    // Start is called before the first frame update
    void Start()
    {
        SetUp();
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - m_moveSpeed * Time.deltaTime);
    }

    void SetUp()
    {
        foreach (Transform position in m_spawnPoints)
        {
            int rand = Random.Range(0, m_spawnPoints.Length-1);
            Instantiate(m_platforms[rand], position.position, transform.rotation, position);
        }
    }

    private void Awake()
    {
        StartCoroutine("DestroyByTime");
    }

    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(m_destroyTime);
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
