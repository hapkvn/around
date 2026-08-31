using UnityEngine;

public class CameraIntro : MonoBehaviour
{
    [SerializeField] private GameObject CameraIn;

    public void PlayGame()
    {
        if (CameraIn != null && StartGame.intance.isS())
        {            
            CameraIn.SetActive(false);
        }
    }
}
