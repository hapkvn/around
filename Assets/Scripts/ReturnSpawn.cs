using UnityEngine;
// class sẽ kéo  vị trí  0 0 khi đạt đến tộ nhất định
public class ReturnSpawn : MonoBehaviour
{

    public static ReturnSpawn instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }



    public float RePos()
    {
        return Random.Range(1000f, 5000f);
     
    }
}
