using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderSwarm : MonoBehaviour
{
    [System.Serializable]
    private struct InvaderType
    {
        public string name;
        public Sprite[] sprites;
        public int points;
        public int rowCount;
    }

    internal static InvaderSwarm Instance;

    [Header("Spawning")]
    [SerializeField]
    private InvaderType[] invaderTypes;

    [SerializeField]
    private int columnCount = 11;
    [SerializeField]
    private int rowCount;

    [SerializeField]
    private float ySpacing;

    [SerializeField]
    private float xSpacing;

    [SerializeField]
    private Transform spawnStartPoint;

    private float minX;

    [Space]
    [Header("Movement")]
    [SerializeField]
    private float speedFactor = 10f;

    private Transform[,] invaders;
    private bool isMovingRight = true;
    private float maxX;
    private float currentX;
    private float xIncrement;

    public float finX;
    public float velY;

    [SerializeField]
    private BulletSpawner bulletSpawnerPrefab;

    private Dictionary<string, int> pointsMap;
    private int killCount;

    public float velperscore = 0.2f;


    private int firstCol = 0;
    private int lastCol;
    public int columnsDestroyed = 0;

    internal void IncreaseDeathCount()
    {
        killCount++;
        if (killCount >= invaders.Length)
        {
            Start();
            return;
        }
    }

    internal Transform GetInvader(int row, int column)
    {
        if (row < 0 || column < 0
            || row >= invaders.GetLength(0) || column >= invaders.GetLength(1))
        {
            return null;
        }

        return invaders[row, column];
    }

    internal int GetPoints(string alienName)
    {
        if (pointsMap.ContainsKey(alienName))
        {
            return pointsMap[alienName];
        }
        return 0;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        killCount = 0;
        firstCol = 0;
        lastCol = columnCount - 1;
        columnsDestroyed = 0;

        minX = spawnStartPoint.position.x;

        GameObject swarm = new GameObject { name = "Swarm" };
        Vector2 currentPos = spawnStartPoint.position;

        maxX = minX + finX * xSpacing * columnCount;
        currentX = minX;
        invaders = new Transform[rowCount, columnCount];

        

        pointsMap = new System.Collections.Generic.Dictionary<string, int>();
        int rowIndex = 0;
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                var invaderType = invaderTypes[Random.Range(0, invaderTypes.Length)];
                var invaderName = invaderType.name.Trim();
                pointsMap[invaderName] = invaderType.points;
                var invader = new GameObject() { name = invaderName };
                invader.AddComponent<SimpleAnimator>().sprites = invaderType.sprites;
                invader.transform.position = currentPos;
                invader.transform.SetParent(swarm.transform);

                invaders[rowIndex, j] = invader.transform;
                currentPos.x += xSpacing;
            }

            currentPos.x = minX;
            currentPos.y -= ySpacing;

            rowIndex++;
        }   
        

        for (int i = 0; i < columnCount; i++)
        {
            var bulletSpawner = Instantiate(bulletSpawnerPrefab);
            bulletSpawner.transform.SetParent(swarm.transform);
            bulletSpawner.column = i;
            bulletSpawner.currentRow = rowCount - 1;
            bulletSpawner.Setup();
        }
    }


    public void updateMax(int col)
    {
        if (col != lastCol) return;
        while (!invaders[0, lastCol].GetComponentInChildren<SpriteRenderer>().enabled && lastCol > firstCol)
        {
            lastCol--;
            columnsDestroyed++;
            maxX += xSpacing;
        }
    }
    public void updateMin(int col)
    {
        if (col != firstCol) return;
        while (!invaders[0, firstCol].GetComponentInChildren<SpriteRenderer>().enabled && firstCol < lastCol)
        {
            firstCol++;
            columnsDestroyed++;
            minX -= xSpacing;
        }
    }

    private void Update()
    {
        xIncrement = (speedFactor + (velperscore * GameManager.Instance.score) + 0.5f * columnsDestroyed) * Time.deltaTime;
        if (isMovingRight)
        {
            currentX += xIncrement;
            if (currentX < maxX)
            {
                MoveInvaders(xIncrement, 0);
            }
            else
            {
                ChangeDirection();
            }
        }
        else
        {
            currentX -= xIncrement;
            if (currentX > minX)
            {
                MoveInvaders(-xIncrement, 0);
            }
            else
            {
                ChangeDirection();
            }
        }
    }

    private void MoveInvaders(float x, float y)
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                invaders[i, j].Translate(x, y, 0);
            }
        }
    }

    private void ChangeDirection()
    {
        isMovingRight = !isMovingRight;
        MoveInvaders(0, -ySpacing * velY);
    }
}
