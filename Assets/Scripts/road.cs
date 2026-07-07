using UnityEngine;

public class road : MonoBehaviour
{
    [SerializeField] private GameObject road1; 
    [SerializeField] private GameObject road2; 
    [SerializeField] private GameObject road3; 
    [SerializeField] private GameObject player;

    [SerializeField] private float roadLength = 40f;

    void Start()
    {

    }

    void Update()
    {
        updateRoad();
    }

    void updateRoad()
    {
       
        if (player.transform.position.z > road2.transform.position.z + 15)
        {
            Vector3 newPosition = road3.transform.position;
            newPosition.z += roadLength;

            road1.transform.position = newPosition;

         
            GameObject temp = road1;
            road1 = road2;
            road2 = road3;
            road3 = temp;
        }
    }
}