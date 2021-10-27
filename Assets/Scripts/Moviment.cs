using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moviment : MonoBehaviour
{
    [SerializeField]
    private AudioClip shooting;

    [SerializeField]
    private AudioClip hit;

    private Transform player;
    private float tspeed;
    private float nextf;
    private Vector3 touchp;
    private Vector3 direction;
    public float speedd;

    public Transform shotSpawn;
    //public Joystick js;
    public Rigidbody2D rb;
    public float maxBound, minBound, speed, fire;
    public GameObject shot;


    private void Start()
    {
        player = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player.position.x < minBound && tspeed < 0)
            rb.velocity = new Vector2(0.0f, 0.0f);
        else if (player.position.x > maxBound && tspeed > 0)
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
            GetComponent<Animator>().SetTrigger("Shoot");
            GameManager.Instance.PlaySfx(shooting);
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchp = Camera.main.ScreenToWorldPoint(touch.position);
            touchp.y=0;
            touchp.z = 0;
            direction = (touchp - transform.position);
            rb.velocity = new Vector2(direction.x, 0) * speedd;
            if (touch.phase == TouchPhase.Ended)
                rb.velocity = Vector2.zero;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BalaEnemigo")
        {
            GameManager.Instance.PlaySfx(hit);
            GameManager.Instance.UpdateLives();
        }
            
    }
}
