using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public Queue<GameObject> tilePool = new Queue<GameObject>();
    public Queue<GameObject> coinPool = new Queue<GameObject>();
    public Queue<GameObject> bulletPool = new Queue<GameObject>();
    public Queue<GameObject> enemyPool = new Queue<GameObject>();
    public Queue<GameObject> itemPool = new Queue<GameObject>();

    public GameObject tilePrefab;
    public GameObject coinPrefab;
    public GameObject bulletPrefab;
    public GameObject itemPrefab;
    public GameObject enemyPrefab;

    public GameState gamestate = GameState.Pause;

    //public Transform parent;


    public GameObject floor;

    public GameSetting Advance;
    //other obj's parent
    public Transform parent;
    public Transform bulletSpawnPos;

    public InGame UIInGame;
    // Coin
    int coin;
    int score;

    int initialSize = 40;
    float totalsum;
    float currentY = 3f;

    float lastTime;

    public int Coin { get => coin; set => coin = value;}
    //public int Score { get => score; set => score = value;}

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            UIInGame.UpdateScore(value);
        }
    }



    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GetAllWeight();
        
        GenerateTilePool();
        GenerateBulletPool();
        GenerateItemPool();
        GenerateCoinPool();
        GenerateEnemyPool();
        //Debug.Log(itemPool.Count + " ", tilePool.Count + " ", coinPool.Count + " ", enemyPool.Count + " ", bulletPool.Count);
        
        Debug.Log(totalsum);
        
        //create things
        for (int i = 0; i < initialSize; i++)
        {
            GenerateTile();
            GenerateItem();
            GenerateEnemy();
        }
    }
    void GenerateTilePool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject go = Instantiate(tilePrefab, transform);
            go.SetActive(false);
            go.name = i.ToString();
            tilePool.Enqueue(go);
        }
    }
    void GenerateItemPool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject go = Instantiate(itemPrefab, parent);
            go.SetActive(false);
            go.name = i.ToString();
            itemPool.Enqueue(go);
        }
    }
    void GenerateCoinPool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject go = Instantiate(coinPrefab, parent);
            go.SetActive(false);
            go.name = i.ToString();
            coinPool.Enqueue(go);
        }
    }
    void GenerateEnemyPool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject go = Instantiate(enemyPrefab, parent);
            go.SetActive(false);
            go.name = i.ToString();
            enemyPool.Enqueue(go);
        }
    }
    void GenerateBulletPool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject go = Instantiate(bulletPrefab, parent);
            go.SetActive(false);
            go.name = i.ToString();
            bulletPool.Enqueue(go);
        }
    }

    //random tile
    void GenerateTile()
    {
        GameObject go = GetInactiveObject(ObjectType.Tile);
        float rand = Random.Range(0, totalsum);
        int randNumber = SetTileByRandomNumber(rand);
        Vector2 pos = new Vector2(Random.Range(-4.5f, 4.5f), currentY);
        switch (randNumber)
        {
            case 0:
                go.transform.position = pos;
                currentY += Random.Range(Advance.normalTile.minHeight, Advance.normalTile.maxHeight);
                go.name = "0";
                go.SetActive(true);
                break;
            case 1:
                go.transform.position = pos;
                currentY += Random.Range(Advance.brokenTile.minHeight, Advance.brokenTile.maxHeight);
                go.name = "1";
                go.SetActive(true);
                break;
            case 2:
                go.transform.position = pos;
                currentY += Random.Range(Advance.oneTimeOnlyTile.minHeight, Advance.oneTimeOnlyTile.maxHeight);
                go.name = "2";
                go.SetActive(true);
                break;
            case 3:
                go.transform.position = pos;
                currentY += Random.Range(Advance.springTile.minHeight, Advance.springTile.maxHeight);
                go.name = "3";
                go.SetActive(true);
                break;
            case 4:
                go.transform.position = pos;
                currentY += Random.Range(Advance.movingHorizontalTile.minHeight, Advance.movingHorizontalTile.maxHeight);
                go.name = "4";
                go.SetActive(true);
                break;
            case 5:
                go.transform.position = pos;
                currentY += Random.Range(Advance.movingVerticalTile.minHeight, Advance.movingVerticalTile.maxHeight);
                go.name = "5";
                go.SetActive(true);
                break;
        }
    }

    void GenerateItem()
    {
        float rand = Random.Range(0f, 1f);
        if(rand < Advance.itemProbability)
        {
            GameObject randGo = null;
            while (true)
            {
                randGo = transform.GetChild(Random.Range(0, initialSize)).gameObject;
                if(randGo.GetComponent<Tile>().tileType < 4 && randGo.transform.position.y > 5)
                {
                    break;
                }
            }
            
            GameObject go = GetInactiveObject(ObjectType.Item);
            go.name = Random.Range(0, 2).ToString();
            go.SetActive(true);
            go.transform.position = randGo.transform.position + new Vector3(0, 0.5f, 0);
        }
    }

    void GenerateEnemy()
    {
        float rand = Random.Range(0f, 1f);
        if (rand < Advance.enemyProbability)
        {
            Vector2 pos = new Vector2(Random.Range(-4.5f, 4.5f), currentY);
            GameObject go = GetInactiveObject(ObjectType.Enemy);
            go.name = Random.Range(0, 3).ToString();
            go.transform.position = pos;
            go.SetActive(true);

        }
    }

    int SetTileByRandomNumber(float number)
    {
        if (number <= Advance.normalTile.weight)
            return 0;
        else if (number <= Advance.normalTile.weight + Advance.brokenTile.weight)
            return 1;
        else if (number <= Advance.normalTile.weight + Advance.brokenTile.weight + Advance.oneTimeOnlyTile.weight)
            return 2;
        else if (number <= Advance.normalTile.weight + Advance.brokenTile.weight + Advance.oneTimeOnlyTile.weight + Advance.springTile.weight)
            return 3;
        else if (number <= Advance.normalTile.weight + Advance.brokenTile.weight + Advance.oneTimeOnlyTile.weight + Advance.springTile.weight + Advance.movingHorizontalTile.weight)
            return 4;
        else if (number <= Advance.normalTile.weight + Advance.brokenTile.weight + Advance.oneTimeOnlyTile.weight + Advance.springTile.weight + Advance.movingHorizontalTile.weight + Advance.movingVerticalTile.weight)
            return 5;

        return -1;
    }

    //When the tile destroy create a new tile
    //The destroy tile will control the whole game creating, other things will be creating by destroying tiles
    //The Difficulty will be changed by this part
    //The score will be added by this part
    void CreateTile()
    {
        if(gamestate != GameState.GameOver)
        {
            GenerateTile();
            GenerateItem();
            GenerateEnemy();
            IncreaseDifficulty(5);
            Score += 5;
        }
    }

    void IncreaseDifficulty(float time)
    {
        if(Time.time - lastTime > time)
        {
            lastTime = Time.time;
            Advance.enemyProbability += 0.01f;
            Debug.Log("IncreaseDifficulty");
        }
    }




    //Get things from pool
    public GameObject GetInactiveObject(ObjectType type)
    {
        switch (type)
        {
            case ObjectType.Tile:
                return tilePool.Dequeue();
            case ObjectType.Item:
                return itemPool.Dequeue();
            case ObjectType.Coin:
                return coinPool.Dequeue();
            case ObjectType.Enemy:
                return enemyPool.Dequeue();
            case ObjectType.Bullet:
                GameObject go = bulletPool.Dequeue();
                go.transform.position = bulletSpawnPos.position;
                go.SetActive(true);
                return go;
            default:
                return null;
        }
    }


    //recycle things
    public void AddInActiveObjectToPool(GameObject go, ObjectType type)
    {
        go.SetActive(false);
        switch (type)
        {
            case ObjectType.Tile:
                tilePool.Enqueue(go);
    //Create tiles
                CreateTile();
                break;
            case ObjectType.Item:
                itemPool.Enqueue(go);
                break;
            case ObjectType.Coin:
                coinPool.Enqueue(go);
                break;
            case ObjectType.Enemy:
                enemyPool.Enqueue(go);
                break;
            case ObjectType.Bullet:
                bulletPool.Enqueue(go);
                break;
            default:
                break;
        }
    
    }

    //get the sum of weight
    void GetAllWeight()
    {
        float sum = 0;
        sum += Advance.normalTile.weight;
        sum += Advance.brokenTile.weight;
        sum += Advance.oneTimeOnlyTile.weight;
        sum += Advance.springTile.weight;
        sum += Advance.movingVerticalTile.weight;
        sum += Advance.movingHorizontalTile.weight;
        totalsum = sum;
    }

    public void EndGame()
    {
        if(gamestate != GameState.GameOver)
        {
            gamestate = GameState.GameOver;
            UIManager.Instance.OpenPanel(2, true);
            MusicManager.Instance.isGameOver = true;
        }
    }

}

public enum ObjectType
{
    Tile,
    Item,
    Coin,
    Enemy,
    Bullet
}

public enum GameState
{
    Pause,
    Running,
    GameOver
}