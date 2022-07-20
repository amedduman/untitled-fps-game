namespace TheRig.Other
{
    using UnityEngine;
    using MoreMountains.Feedbacks;
    using TheRig.Player;
    using TheRig.GameEvents;
    using ThirdParty.DependencyProvider;

    public class EnemyXpDrop : MonoBehaviour
    {
        GameEvents _gameEvents
        {
            get
            {
                return DependencyProvider.Instance.Get<GameEvents>();
            }
        }
        [SerializeField] DummyEntity _enemy;
        [SerializeField] MMF_Player _onGetCollect;

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerEntity _player))
            {
                GetComponent<Collider>().enabled = false;
                _player.GetXp(_enemy.XpToDrop);
                _onGetCollect.PlayFeedbacks();
            }
        }
    }
}
