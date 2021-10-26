using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bspeed;
    private Transform bullet;
    public float techo;

    private void Start()
    {
        bullet = GetComponent<Transform>();
    }
    void FixedUpdate()

    {
        rb.AddForce(transform.up * bspeed, ForceMode2D.Impulse);
        if (bullet.position.y >= techo)
        {
            Destroy(gameObject);
        }


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy1")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Score.score += 10;
        }
    }
    
        
 }
