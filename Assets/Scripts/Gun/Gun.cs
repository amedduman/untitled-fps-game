using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour
{
    [field: SerializeField] public Sprite Crosshair {get; private set;}
    protected GunHandler _gunHandler;

    protected virtual void Start()
    {
        _gunHandler = DependencyProvider.Instance.Get<GunHandler>();
        _gunHandler.GunChanged(this);
    }

    public virtual void Reload()
    {

    }

    public virtual void Shoot(Vector3 vel)
    {

    }

    public virtual void ForceReload()
    {
        
    }
}
