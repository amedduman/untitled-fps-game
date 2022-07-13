using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] Image _crosshairImage;

    GameManager _gameManager;

    private void OnEnable()
    {
        _gameManager = DependencyProvider.Instance.Get<GameManager>();

        _gameManager.OnGunChanged += HandleGunChanged;
    }

    private void OnDisable()
    {
        _gameManager.OnGunChanged -= HandleGunChanged;
    }

    public void HandleGunChanged(Gun newGun)
    {
        _crosshairImage.sprite = newGun.Crosshair;
    }
}
