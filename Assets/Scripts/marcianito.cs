using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marcianito : MonoBehaviour
{
    private Transform enemyh;
    private float nextfe;
    private bool baja;
    public float espeed, frate, lado, suelo;
    public GameObject shot;
    public Transform shotspawn;

    void Start()
    {
        //InvokeRepeating("Move", 0.1f, 0.3f);
        enemyh= GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        Move();
        if (Time.time > nextfe)
        {
            nextfe = Time.time + frate;
            Instantiate(shot, shotspawn.position, shotspawn.rotation);
        }
    }

    void Move()
    {
        if (enemyh.position.x > -lado || enemyh.position.x < lado && baja == true)
        {
            baja = false;
        }
        if (enemyh.position.x <= -lado || enemyh.position.x >= lado)
        {
        if (baja == false)
        {
            espeed = -espeed;
            enemyh.position += Vector3.down * 0.5f;
            baja = true;
        }
                
        }
            
        if (enemyh.position.y <= suelo)
        {
            //-vida
        }
        enemyh.position += Vector3.right * espeed * Time.fixedDeltaTime;
    }

    
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //-vida
        }
    }*/
}
