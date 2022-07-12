using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100000)]
public class DependencyProvider : MonoBehaviour
{
    public static DependencyProvider Instance {get; private set;}

    List<object> RegisteredItems = new List<object>();
    static bool _hasInitialized = false;

    private void Awake()
    {
        if(!_hasInitialized)
        {
            Instance = this;
            _hasInitialized = true;
        }
        else
        {
            Debug.LogError($"There are two {nameof(DependencyProvider)} in opened scene(s)");
            Debug.Break();
        }
    }

    private void OnDisable()
    {
        Instance = null;
        _hasInitialized = false;
    }

#region Register
    public bool Register(object itemToRegister)
    {
        if(itemToRegister == null)
        {
            Debug.LogError($"registration has failed.");
            Debug.Break();
            return false;
        }
        RegisteredItems.Add(itemToRegister);
        return true;
    }

    public bool Deregister(object itemToDeregister)
    {
        if(itemToDeregister == null)
        {
            Debug.LogError("de-registration has failed.");
            Debug.Break();
            return false;
        }
        if(RegisteredItems.Contains(itemToDeregister))
        {
            RegisteredItems.Remove(itemToDeregister);
        }
        else
        {
            Debug.LogError("item for de-registration is has not been registered.");
            Debug.Break();
            return false;
        }
        Debug.LogError($"something unexpected happened while trying to do de-registration.");
        Debug.Break();
        return false;
    }
#endregion 

    public T Get<T>() where T : class
    {
        foreach (var item in RegisteredItems)
        {
            if(item is T)
            {
                return (T)item;
            }
        }
        Debug.Log($"null returned");
        return null;
    }
}
