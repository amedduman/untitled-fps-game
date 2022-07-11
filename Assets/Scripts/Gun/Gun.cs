using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    protected GunHandler _gunHandler;

    private void Start()
    {
        _gunHandler = DependencyProvider.Instance.Get<GunHandler>();
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
