using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoPhysics : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    Collider2D a,b;

    // Start is called before the first frame update
    void Start()
    {
        body.drag = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
