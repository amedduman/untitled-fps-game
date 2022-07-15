namespace TheRig.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TheRig.Gun;
    using ThirdParty.DependencyProvider;
    using TheRig.GameEvents;

    public class HudUI : MonoBehaviour
    {
        [SerializeField] Image _crosshairImage;

        GameEvents _gameEvents
        {
            get
            {
                return DependencyProvider.Instance.Get<GameEvents>();   
            }
        }

        private void OnEnable() 
        {
            _gameEvents.OnGunChanged += HandleGunChanged;
        }

        private void OnDisable()
        {
            _gameEvents.OnGunChanged -= HandleGunChanged;
        }

        public void HandleGunChanged(Gun newGun)
        {
            _crosshairImage.sprite = newGun.Crosshair;
        }
    }
}
