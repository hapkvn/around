using UnityEngine;
[CreateAssetMenu(menuName = "Game Data/Map/Forest")]

public class forest : MapConfig
{
    public override void PlayMapLogic()
    {
        Debug.Log("Playing City Map Logic");
    }
}
