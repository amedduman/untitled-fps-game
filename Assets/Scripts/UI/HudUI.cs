namespace TheRig.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using DG.Tweening;
    using TheRig.Gun;
    using ThirdParty.DependencyProvider;
    using TheRig.GameEvents;

    public class HudUI : MonoBehaviour
    {
        [SerializeField] Image _hitFeedbackImage;
        [SerializeField] Image _crosshairImage;
        [SerializeField] TextMeshProUGUI _ammoText;
        [SerializeField] float _hitFeedbackFadeOutDuration = 1;

        GameEvents _gameEvents
        {
            get
            {
                return DependencyProvider.Instance.Get<GameEvents>();
            }
        }

        private void Awake()
        {
            _hitFeedbackImage.DOFade(0,0);
        }

        private void OnEnable()
        {
            _gameEvents.OnGunChanged += HandleGunChanged;
            _gameEvents.OnGunFire += HandlePlayerShoot;
            _gameEvents.OnEnemyGetDamaged += HandleEnemyGetDamaged;
            _gameEvents.OnGunReloadComplete += HandleGunReloadComplete;
        }

        private void OnDisable()
        {
            _gameEvents.OnGunChanged -= HandleGunChanged;
            _gameEvents.OnGunFire -= HandlePlayerShoot;
            _gameEvents.OnEnemyGetDamaged -= HandleEnemyGetDamaged;
            _gameEvents.OnGunReloadComplete -= HandleGunReloadComplete;
        }

        void HandleGunChanged(Gun newGun)
        {
            _crosshairImage.sprite = newGun.Crosshair;
            SetAmmoText(newGun.MaxAmmo);
        }

        void HandlePlayerShoot(int remainingAmmo)
        {
            SetAmmoText(remainingAmmo);
        }

        void HandleEnemyGetDamaged()
        {
            _hitFeedbackImage.DOFade(1, 0);
            _hitFeedbackImage.DOFade(0, _hitFeedbackFadeOutDuration); 
        }

        void HandleGunReloadComplete(int maxAmmo)
        {
            SetAmmoText(maxAmmo);
        }

        void SetAmmoText(int remainingAmmo)
        {
            _ammoText.text = remainingAmmo.ToString();
        }
    }
}
