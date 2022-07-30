namespace TheRig.Other
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "GameValues", menuName = "FPS/GameValues")]
    public class GameValues : ScriptableObject
    {
        [field: SerializeField] public float SpawnInterval {get; private set;} = 1;
        [field: SerializeField] public int GameplaySessionTimeInMinutes {get; private set;} = 20;
    }
}