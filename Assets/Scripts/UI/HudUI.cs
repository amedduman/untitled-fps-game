namespace TheRig.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using TheRig.Gun;
    using ThirdParty.DependencyProvider;
    using TheRig.GameEvents;

    public class HudUI : MonoBehaviour
    {
        [SerializeField] Image _crosshairImage;
        [SerializeField] TextMeshProUGUI _ammoText;

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
            _gameEvents.OnGunFire += HandlePlayerShoot;
            _gameEvents.OnGunReloading += HandleGunReloading;
        }

        private void OnDisable()
        {
            _gameEvents.OnGunChanged -= HandleGunChanged;
            _gameEvents.OnGunFire -= HandlePlayerShoot;
            _gameEvents.OnGunReloading -= HandleGunReloading;
        }

        void HandleGunChanged(Gun newGun)
        {
            _crosshairImage.sprite = newGun.Crosshair;
            SetAmmoText(newGun.AmmoMax);
        }

        void HandlePlayerShoot(int remainingAmmo)
        {
            SetAmmoText(remainingAmmo);
        }

        void HandleGunReloading(int newAmmo)
        {
            SetAmmoText(newAmmo);
        }

        void SetAmmoText(int remainingAmmo)
        {
            _ammoText.text = remainingAmmo.ToString();
        }
    }
}
