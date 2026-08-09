using UnityEngine;
[CreateAssetMenu(menuName = "Game Data/Map/City Map")]
public class CityMap : MapConfig
{
   public override void PlayMapLogic()
   {
       Debug.Log("Playing City Map Logic");
   }
}
