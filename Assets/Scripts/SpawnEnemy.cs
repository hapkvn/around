using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;

    private RoadManager road;
    void Start()
    {

        for(int i=0; i< spawnPoints.Length; i++)
        {
            spawnPoints[i].transform.position = new Vector3(
               spawnPoints[i].transform.position.x,
               spawnPoints[i].transform.position.y,
               road.spawn()
            );
        }
    }

    // Update is called once per frame
    void Update()
    {
        RandomSpawn();
    }


    private void RandomSpawn()
    {
        int rPoint  = Random.Range(0, spawnPoints.Length);
        int rEnemy = Random.Range(0, enemyPrefabs.Length);

        Instantiate(enemyPrefabs[rEnemy], spawnPoints[rPoint].position, Quaternion.identity);

    }
}
