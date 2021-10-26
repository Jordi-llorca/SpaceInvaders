using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaE : MonoBehaviour
{
    public float bspeed;
    public float suelo;

    void FixedUpdate()
    {
        transform.Translate(Vector3.down * bspeed * Time.deltaTime);
        if (transform.position.y <= suelo)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Bala")
        {
            Destroy(gameObject);
        }
    }
}