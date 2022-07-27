namespace TheRig.UI
{
    using UnityEngine;
    using TMPro;
    using TheRig.Gun;
    using ThirdParty.DependencyProvider;
    using TheRig.GameEvents;

    public class HudUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _ammoText;
        [SerializeField] TextMeshProUGUI _healthText;
        [SerializeField] TextMeshProUGUI _xpText;

        GameEvents _gameEvents;

        private void Awake()
        {
            _gameEvents = DependencyProvider.Instance.Get<GameEvents>();
        }

        private void OnEnable()
        {
            _gameEvents.OnGunChanged += HandleGunChanged;
            _gameEvents.OnGunFire += HandlePlayerShoot;
            _gameEvents.OnGunReloadComplete += HandleGunReloadComplete;
            _gameEvents.OnPlayerHealthChanged += HandlePlayerGetDamage;
            _gameEvents.OnPlayerXpChanged += HandleXpChanged;
        }

        private void OnDisable()
        {
            _gameEvents.OnGunChanged -= HandleGunChanged;
            _gameEvents.OnGunFire -= HandlePlayerShoot;
            _gameEvents.OnGunReloadComplete -= HandleGunReloadComplete;
            _gameEvents.OnPlayerHealthChanged -= HandlePlayerGetDamage;
            _gameEvents.OnPlayerXpChanged -= HandleXpChanged;
        }

        void HandleGunChanged(Gun newGun)
        {
            SetAmmoText(newGun.MaxAmmo);
        }

        void HandlePlayerShoot(int remainingAmmo)
        {
            SetAmmoText(remainingAmmo);
        }

        void HandleGunReloadComplete(int maxAmmo)
        {
            SetAmmoText(maxAmmo);
        }

        void HandlePlayerGetDamage(int currentHealth)
        {
            _healthText.text = currentHealth.ToString();
        }

        void HandleXpChanged(int currentXp)
        {
            _xpText.text = currentXp.ToString();
        }

        void SetAmmoText(int remainingAmmo)
        {
            _ammoText.text = remainingAmmo.ToString();
        }
    }
}
