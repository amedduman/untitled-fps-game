namespace ThirdParty.DependencyProvider
{
    using UnityEngine;

    [DefaultExecutionOrder(-99999)]
    public class DependencyBinder : MonoBehaviour
    {
        [SerializeField] Component _componentToBind;

        private void Awake()
        {
            DependencyProvider.Instance.Register(_componentToBind);
        }
    }
}