namespace TheRig.Handler
{
    using System.Collections;
    using UnityEngine;
    using TheRig.Other; 
    using TheRig.GameEvents;
    using ThirdParty.DependencyProvider;

    public class GameplaySessionTimeHandler : MonoBehaviour
    {
        GameEvents _gameEvents;

        [SerializeField] GameValues _gv;
        int _currentTimeInSeconds;
        void Start()
        {
            _gameEvents = DependencyProvider.Instance.Get<GameEvents>();
            _currentTimeInSeconds = _gv.GameplaySessionTimeInMinutes * 60;
            StartCoroutine(CountDown());
        }

        IEnumerator CountDown()
        {
            while(_currentTimeInSeconds > 0)
            {
                _gameEvents.InvokeGameplaySessionTimeInSecondsChange(_currentTimeInSeconds);
                yield return new WaitForSecondsRealtime(1);
                _currentTimeInSeconds--;
            }
        }
    }
}
