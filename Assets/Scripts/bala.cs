using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    public float bspeed;
    public float techo;

    internal void DestroySelf()
    {
        GameManager.Instance.CreateExplosion(transform.position);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * bspeed * Time.deltaTime);
        if (transform.position.y >= techo)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    { 
        if(other.tag == "Enemy" || other.tag == "BalaEnemigo")
        {
            DestroySelf();
        }   
    }
 }
