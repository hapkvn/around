using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceenManager : MonoBehaviour
{
   public void StartGame()
   {
        SceneManager.LoadScene("Game");
   }
}
