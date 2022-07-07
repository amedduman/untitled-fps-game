using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "FPS/Gun")]
public class GunProperties : ScriptableObject
{
    [field: SerializeField]
    public int Ammo {get; private set;}
    
    public GameObject AmmoPrefab;

    private void OnValidate()
    {
        if(AmmoPrefab.TryGetComponent<IAmmo>(out IAmmo ammo))
        {
            // ignore
        }
        else
        {
            AmmoPrefab = null;
        }
    }
}