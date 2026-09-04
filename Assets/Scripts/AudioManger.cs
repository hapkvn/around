using UnityEngine;

public class AudioManger : MonoBehaviour
{
    public static AudioManger instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceC;


    [SerializeField] private AudioClip background;
    [SerializeField] private AudioClip carCrash;


    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }


    private void Start()
    {
       audioSource.clip = background;
       audioSource.Play();
    }

    public void playCrash()
    {
        audioSourceC.PlayOneShot(carCrash);

    }


}
