using UnityEngine;

public class AudioManger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip background;

    private void Start()
    {
       audioSource.clip = background;
       audioSource.Play();
    }


}
