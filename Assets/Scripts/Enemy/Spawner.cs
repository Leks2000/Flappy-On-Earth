using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpriteRenderer leftWall;
    [SerializeField] private SpriteRenderer rightWall;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject beer;
    [SerializeField] private float SpawnTime = 0.4f;

    private int countSpawnedObjects = 0;
    private int countSpawnedLeft = 0;
    private int countSpawnedRight = 0;
    private float timer;
    Vector2 spawnPosition;

    private MovePlayer player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<MovePlayer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!player.GameOver())
        {
            if (timer >= SpawnTime)
            {
                int side = Random.Range(0, 2);
                if (side == 0 && (countSpawnedLeft < 3))
                {
                    SpawnEnemy(leftWall.bounds.max.x, false);
                    countSpawnedLeft++;
                    countSpawnedRight = 0;
                }
                if (side == 1 && (countSpawnedRight < 3))
                {
                    SpawnEnemy(rightWall.bounds.min.x, true);
                    countSpawnedRight++;
                    countSpawnedLeft = 0;
                }
                timer = 0f;
            }
            if (SpawnTime <= 0.4f)
            {
                CancelInvoke("IncreaseSpeed");
            }
        }
    }

    private void SpawnEnemy(float wall, bool flip)
    {
        spawnPosition = SetPosition(wall, flip);
        Instantiate(enemy, spawnPosition, enemy.transform.rotation);
        countSpawnedObjects++;
        if (countSpawnedObjects == 4)
        {
            SpawnBeer(wall, flip);
            countSpawnedObjects = 0;
        }
    }

    private void SpawnBeer(float lastEnemy, bool flip)
    {
        Vector2 beerPosition = new Vector2(lastEnemy, spawnPosition.y + 0.5f);
        beer.GetComponent<SpriteRenderer>().flipY = flip;
        Instantiate(beer, beerPosition, beer.transform.rotation);
    }

    private Vector2 SetPosition(float wall, bool flip)
    {
        float pos = Random.Range(-0.3f, 0.3f);
        Vector2 playerPosition = GameObject.Find("Player").transform.position;
        enemy.transform.position = new Vector2(wall, playerPosition.y + 4 + pos);
        enemy.GetComponent<SpriteRenderer>().flipY = flip;
        return enemy.transform.position;
    }
}
