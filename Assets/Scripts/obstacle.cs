using UnityEngine;

public class obstacle : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        destroyObs();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Va cham");
        }
    }
    private void destroyObs()
    {
        if(Player.instance.transform.position.z - transform.position.z > 20f)
        {
            Destroy(gameObject);
        }
    }
}
