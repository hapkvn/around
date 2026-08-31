using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
   

    [Header("Cài đặt Thời gian")]
    [SerializeField] private float spawnInterval = 2f;     
    private float timer;
    void Start()
    {

        timer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {

        if(!StartGame.intance.isS())
        {
            return;
        }
        else
        {
            if (!Player.instance.IsEndGame())
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    RandomSpawn();
                    timer = spawnInterval;
                }

            }

        }
    }  


    private void RandomSpawn()
    {
        GameObject obstacle = Map_Manager.instance.returnObs();
        Transform rPoint  =  Map_Manager.instance.returnObsPos();

        Vector3 finalSpawnPosition = new Vector3(
            rPoint.position.x,
            rPoint.position.y,
            RoadManager.instance.spawn()
        );

        Quaternion rotation = Quaternion.identity;
        if (rPoint.position.x < 0)
        {
            rotation = Quaternion.Euler(0, 180, 0);
        }

        Instantiate(obstacle, finalSpawnPosition, rotation);

    }
}
