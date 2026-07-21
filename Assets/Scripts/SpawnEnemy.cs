using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Cài đặt Chướng ngại vật")] 
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Cài đặt Thời gian")]
    [SerializeField] private float spawnInterval = 2f;     
    private float timer;
    void Start()
    {

        newSpawn();
        timer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        newSpawn();
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
        RandomSpawn();
        timer = spawnInterval;

        }
    }
    private void newSpawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].transform.position = new Vector3(
               spawnPoints[i].transform.position.x,
               spawnPoints[i].transform.position.y,
               RoadManager.instance.spawn()
            );
        }
    }


    private void RandomSpawn()
    {
        int rPoint  = Random.Range(0, spawnPoints.Length);
        int rEnemy = Random.Range(0, enemyPrefabs.Length);

        Instantiate(enemyPrefabs[rEnemy], spawnPoints[rPoint].position, Quaternion.Euler(0, 180, 0));

    }
}
