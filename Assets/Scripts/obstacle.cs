using UnityEngine;

public class obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Tốc độ di chuyển của vật cản
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
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
