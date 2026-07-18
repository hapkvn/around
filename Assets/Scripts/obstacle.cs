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
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player"))
        {
            Debug.Log("Va cham");
            // Thực hiện hành động khi va chạm với Player
        }
    }
}
