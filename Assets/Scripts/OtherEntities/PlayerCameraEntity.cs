namespace TheRig.Other
{
    using UnityEngine;

    public class PlayerCameraEntity : MonoBehaviour
    {
        private void Awake()
        {
            DependencyProvider.Instance.Register(this);
        }
    }
}
