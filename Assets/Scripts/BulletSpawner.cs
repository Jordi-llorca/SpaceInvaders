using UnityEngine;
using UnityEngine.UI;


public class BulletSpawner : MonoBehaviour
{
    internal int currentRow;
    public GameManager gm;
    internal int column;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float minTime;

    [SerializeField]
    private float maxTime;

    private float timer;
    private float currentTime;
    private Transform followTarget;

    internal void Setup()
    {
        currentTime = Random.Range(minTime, maxTime);
        followTarget = InvaderSwarm.Instance.GetInvader(currentRow, column);
    }

    private void Update()
    {
        transform.position = followTarget.position;

        timer += Time.deltaTime;
        if (timer < currentTime)
        {
            return;
        }

        Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        timer = 0f;
        currentTime = Random.Range(minTime, maxTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Bala")
        {
            return;
        }

        followTarget.GetComponentInChildren<SpriteRenderer>().enabled = false;
        currentRow = currentRow - 1;
        gm.score += 10;
        gm.UpdateScore();
 
        if (currentRow < 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Setup();
        }
    }
}
