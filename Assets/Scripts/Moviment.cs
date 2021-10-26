using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moviment : MonoBehaviour
{
    private Transform player;
    private float tspeed;
    private float nextf;

    public Transform shotSpawn;
    public Joystick js;
    public Rigidbody2D rb;
    public float maxBound,minBound,speed,fire;
    public GameObject shot;
    
    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate(){
        
        if (js.Horizontal!=0f) {
            tspeed=js.Horizontal * speed;
            rb.AddForce(transform.right*tspeed, ForceMode2D.Impulse);
        }
        else
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }
        if (player.position.x < minBound && tspeed < 0)
            rb.velocity = new Vector2(0.0f, 0.0f);
        else if(player.position.x > maxBound && tspeed > 0)
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }      

    }
    private void Update()
    {
        if (Time.time > nextf)
        {
            nextf = Time.time + fire;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }
}
