using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlowMoPhysics : MonoBehaviour
{
    
    [SerializeField] SpriteRenderer mat;
    public Vector3 punch;
    public float duration;
    public int vibrato;
    public float elasticity;




    // Start is called before the first frame update
    void Start()
    {

        //mat.DOFade(0, 1.5f);
        RTL();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void RTL()
    {
        transform.DORotate(new Vector3(0, 0, duration), 2, RotateMode.Fast).OnComplete(() => { RTR(); }); ;
    }

    void RTR()
    {
        transform.DORotate(new Vector3(0, 0, -duration), 2, RotateMode.Fast).OnComplete(() => { RTL(); }) ;
    }
}
