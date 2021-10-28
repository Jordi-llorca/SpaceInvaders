using UnityEngine;
using UnityEngine.UI;


public class BulletSpawner : MonoBehaviour
{
    internal int currentRow;
    internal int column;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float minTime;

    [SerializeField]
    private float maxTime;

    [SerializeField]
    private AudioClip shooting;

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
        if (transform.position.y <= -1f) LevelLoader.Instance.LoadNextLevel();

        timer += Time.deltaTime;
        if (timer < currentTime)
        {
            return;
        }

        Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        GameManager.Instance.PlaySfx(shooting);

        timer = 0f;
        currentTime = Random.Range(minTime, maxTime);

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Bala")
        {
            return;
        }

        GameManager.Instance.UpdateScore(InvaderSwarm.Instance.GetPoints(followTarget.gameObject.name));

        followTarget.GetComponentInChildren<SpriteRenderer>().enabled = false;
        currentRow = currentRow - 1;

        if (currentRow < 0)
        {
            InvaderSwarm.Instance.updateMax(column);
            InvaderSwarm.Instance.updateMin(column);
            gameObject.SetActive(false);
        }
        else
        {
            Setup();
        }

        InvaderSwarm.Instance.IncreaseDeathCount();
    }
}
