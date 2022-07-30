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
        [SerializeField] TextMeshProUGUI _remainingMinutesText;
        [SerializeField] TextMeshProUGUI _remainingSecondsText;


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
            _gameEvents.OnGameplaySessionTimeInSecondsChange += HandleGameplayTimerChanged;
        }

        private void OnDisable()
        {
            _gameEvents.OnGunChanged -= HandleGunChanged;
            _gameEvents.OnGunFire -= HandlePlayerShoot;
            _gameEvents.OnGunReloadComplete -= HandleGunReloadComplete;
            _gameEvents.OnPlayerHealthChanged -= HandlePlayerGetDamage;
            _gameEvents.OnPlayerXpChanged -= HandleXpChanged;
            _gameEvents.OnGameplaySessionTimeInSecondsChange -= HandleGameplayTimerChanged;
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

        void HandleGameplayTimerChanged(int remainingTimeInSeconds)
        {
            Vector2 MinutesAndSeconds = ConvertSecondsToMinutesAndSeconds(remainingTimeInSeconds);
            _remainingMinutesText.text = MinutesAndSeconds.x.ToString();
            _remainingSecondsText.text = MinutesAndSeconds.y.ToString();
        }


        void SetAmmoText(int remainingAmmo)
        {
            _ammoText.text = remainingAmmo.ToString();
        }

        Vector2 ConvertSecondsToMinutesAndSeconds(int seconds)
        {
            int minutes = 0;
            while(seconds - 60 >= 0)
            {
                seconds -= 60;
                minutes++;
            }
            return new Vector2(minutes, seconds);
        }
    }
}
