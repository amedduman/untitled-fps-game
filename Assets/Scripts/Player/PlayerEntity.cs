namespace TheRig.Player
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using TheRig.Gun;
    using TheRig.GameEvents;
    using ThirdParty.DependencyProvider;

    [RequireComponent(typeof(CharacterController))]
    public class PlayerEntity : MonoBehaviour
    {
        GameEvents _gameEvents;

        public int CurrentHealth { get; private set; }

        [SerializeField] int _maxHealth = 100;
        [SerializeField] Gun _gun;
        [SerializeField] float _speed = .1f;
        [Foldout("non-designer", false)][SerializeField] InputActionReference _movement, _pointerPos, _shoot, _reload;
        [Foldout("non-designer", false)][SerializeField] LayerMask _ground;

        CharacterController _controller;
        bool _isDead;
        int _currentXp;

        void Awake()
        {
            _gameEvents = DependencyProvider.Instance.Get<GameEvents>();
            //Cursor.lockState = CursorLockMode.Confined;
            CurrentHealth = _maxHealth;
            _controller = GetComponent<CharacterController>();
        }

        void Start()
        {
            _gameEvents.InvokePlayerHealthChanged(CurrentHealth);
            _gameEvents.InvokePlayerXpChanged(_currentXp);
        }

        void Update()
        {
            if (_isDead) return;

            Reload();

            Rotation();

            Movement();

            Shoot();
        }

        void Reload()
        {
            if (_reload.action.triggered)
            {
                _gun.ForceReload();
            }
        }

        void Rotation()
        {
            Vector3 screenPos = _pointerPos.action.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(screenPos);

            RaycastHit hit;
            if (Physics.Raycast(ray.origin,
                            ray.direction,
                            out hit, Mathf.Infinity, _ground))
            {
                Vector3 direction = (hit.point - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
            }

        }

        private void Movement()
        {
            Vector2 movementInput = _movement.action.ReadValue<Vector2>();
            Vector3 moveDir = new Vector3(movementInput.x, 0, movementInput.y) * _speed;

            _controller.Move(moveDir * Time.deltaTime);
        }

        private void Shoot()
        {
            if (_shoot.action.inProgress)
            {
                _gun.GetComponent<Gun>().Shoot(_controller.velocity * Time.deltaTime);
            }
        }

        public void GetDamage(int damage)
        {
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);

            _gameEvents.InvokePlayerHealthChanged(CurrentHealth);


            if (CurrentHealth <= 0)
            {
                HandleDeath();
            }
        }

        void HandleDeath()
        {
            _isDead = true;
            _gameEvents.InvokePlayerDeath();
        }

        public void GetXp(int xp)
        {
            _currentXp += xp;
            _gameEvents.InvokePlayerXpChanged(_currentXp);
        }
    }
}