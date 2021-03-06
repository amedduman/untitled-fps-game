namespace ThirdParty.DependencyProvider
{
    using System.Collections.Generic;
    using UnityEngine;

    [DefaultExecutionOrder(-100000)]
    public class DependencyProvider : MonoBehaviour
    {
        public static DependencyProvider Instance { get; private set; }

        List<object> RegisteredItems = new List<object>();

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()  
        {
            Instance = null;
        }

        #region Register
        public bool Register(object itemToRegister)
        {
            if (itemToRegister == null)
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
            if (itemToDeregister == null)
            {
                Debug.LogError($"de-registration has failed. item for de-reregistration is null");
                Debug.Break();
                return false;
            }
            if (RegisteredItems.Contains(itemToDeregister))
            {
                RegisteredItems.Remove(itemToDeregister);
                return true;
            }
            else
            {
                Debug.LogError($"{itemToDeregister} has not been registered but you are trying to de-register it.");
                Debug.Break();
                return false;
            }
        }
        #endregion

        public T Get<T>() where T : class
        {
            foreach (var item in RegisteredItems)
            {
                if (item is T)
                {
                    if (IsNullOrDestroyed(item))
                    {
                        Deregister(item);
                        break;
                    }
                    return (T)item;
                }
            }
            Debug.Log($"null returned instead of {typeof(T)}");
            Debug.Break();
            return null;
        }

        bool IsNullOrDestroyed(System.Object obj)
        {

            if (object.ReferenceEquals(obj, null)) return true;

            if (obj is UnityEngine.Object) return (obj as UnityEngine.Object) == null;

            return false;
        }
    }
}