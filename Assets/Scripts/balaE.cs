using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaE : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bspeed;
    private Transform bullet;
    public float suelo;

    private void Start()
    {
        bullet = GetComponent<Transform>();
    }
    void FixedUpdate()

    {
        rb.AddForce(transform.up * bspeed, ForceMode2D.Impulse);
        if (bullet.position.y >= suelo)
        {
            Destroy(gameObject);
        }


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            Destroy(gameObject);
            Score.score -= 10;
        }
    }


}