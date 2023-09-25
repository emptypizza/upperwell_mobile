using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public enum GROUP_TYPE { NORMAL, CHASER, SLOW, HIGH, ADV }

    public GROUP_TYPE group_type;
    public GROUP_TYPE group_type_next;

    public GameObject mobgroup;
    public Enemy[] enemyPrefabs;       // The enemy prefab to instantiate.
    public float spawnRate = 1.2f;         // Rate at which enemies will spawn.
    [SerializeField]
    private float nextSpawnTime = 0f;    // To control the timing of the spawns.

    public float gameWorldWidth = 20f;   // Width of the game world.
    public float gameWorldHeight = 10f;  // Height of the game world.
    [SerializeField] ballrumble player;



    public float fLeveling = 0;
    public void Start()
    {

        //enemyPrefabs = new Enemy[6];
        mobwaveStart();
        if (null == player)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<ballrumble>();
        }
        fLeveling = 0;
    }

    public void mobwaveStart()
    {
        this.group_type = GROUP_TYPE.NORMAL;
        this.group_type_next = GROUP_TYPE.NORMAL;
        /*
		this.can_dispatch = false;
        this.mob_generate_line = 0.0f;
        this.next_line = 10.0f;*/
    }

    // public void FixedUpdate() {   this.mobAppearControl(); }        
    public float fGametime = 0f;
    public float fMobSpawnDelay = 3;

    public float Spawn_HorizontalLeft = -8;
    public float Spawn_HorizontalRight = 8;
    public float Spawn_VerticalBottom = 0; //Spawn_15VerticalBottom
    public float Spawn_VerticalTop = 16.5f; //Spawn_15VerticalTop


    public void Update()
    {
        fGametime = Time.time;

        if (fGametime >= nextSpawnTime)
        {

            Vector2[] spawnPoints0 = {



            new Vector2(Spawn_HorizontalLeft, Random.Range(Spawn_VerticalBottom, Spawn_VerticalTop) + this.transform.position.y),
            new Vector2(Spawn_HorizontalRight, Random.Range(Spawn_VerticalBottom, Spawn_VerticalTop) + this.transform.position.y),
            new Vector2(Random.Range(Spawn_HorizontalLeft, Spawn_HorizontalRight)  + this.transform.position.x,

            Spawn_VerticalTop),
            new Vector2(Random.Range(Spawn_HorizontalLeft, Spawn_HorizontalRight)+ this.transform.position.x, Spawn_VerticalBottom)


        };
            Vector2[] spawnPoints1 =
                {


                };

            fLeveling = fGametime / 5;//시간이 지날 수록 난이도를 상승시킨.
            Vector2 lv1spawnPosition = spawnPoints0[Random.Range(0, spawnPoints0.Length)]; //footman 중구난방하게 나오는 풋맨 스폰 위칟
            Vector2 lv2spawnPosition;// = spawnPoints1[Random.Range(0, spawnPoints1.Length)]; //wingman 은 좌우하 끝쪽에서 스폰시, pc의 position.y+1~3를 기억하여 spawn


            if (fLeveling <= 1) // 10으로 나눠서 1이하 인 경우, 풋맨 
            
                Instantiate(enemyPrefabs[0], lv1spawnPosition, Quaternion.identity);

            

            nextSpawnTime = fGametime + 1f / spawnRate; //SpawnEnemy();


        }

    }

    /*
    void SpawnEnemy()
    {
        Vector2[] spawnPoints = {
            new Vector2(-20, Random.Range(-25, 25)),
            new Vector2(20, Random.Range(-25, 25)),
            new Vector2(Random.Range(-15, 15), 25),
            new Vector2(Random.Range(-15, 15), -25)
        };

        Vector2 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefabs[0], spawnPosition, Quaternion.identity);
    }
    */
}
