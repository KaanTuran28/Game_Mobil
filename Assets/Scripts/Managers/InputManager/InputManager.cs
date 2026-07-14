using GameMobil.Core;
using GameMobil.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameMobil.Managers
{
    /// <summary>
    /// Projede mevcut olan <c>InputSystem_Actions.inputactions</c> varlığını sarmalayan manager.
    /// Bu varlık için C# wrapper class üretimi kapalı olduğu için (generateWrapperCode: 0),
    /// üretilen bir sınıfa değil doğrudan <see cref="InputActionAsset"/> referansına dayanır.
    /// </summary>
    /// <remarks>
    /// Sürekli eksen değerleri (Move/Look) her frame poll edilen property'ler olarak sunulur;
    /// bunları her frame EventBus üzerinden yayınlamak gereksiz GC/performans maliyeti yaratırdı.
    /// Ayrık (discrete) aksiyonlar (Jump/Interact/Attack) ise düşük frekanslı oldukları için
    /// EventBus üzerinden yayınlanır. Oyuncu hareketi/davranışı bu sınıfın kapsamı dışındadır.
    /// </remarks>
    public class InputManager : MonoBehaviour, IInputManager
    {
        private const string PlayerActionMapName = "Player";

        [SerializeField] private InputActionAsset _inputActions;

        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _jumpAction;
        private InputAction _interactAction;
        private InputAction _attackAction;

        public Vector2 MoveInput => _moveAction != null ? _moveAction.ReadValue<Vector2>() : Vector2.zero;
        public Vector2 LookInput => _lookAction != null ? _lookAction.ReadValue<Vector2>() : Vector2.zero;

        public void Initialize()
        {
            if (_inputActions == null)
            {
                Debug.LogWarning("InputManager: InputActionAsset atanmamış.");
                return;
            }

            var playerMap = _inputActions.FindActionMap(PlayerActionMapName, throwIfNotFound: false);
            if (playerMap == null)
            {
                Debug.LogWarning($"InputManager: '{PlayerActionMapName}' action map bulunamadı.");
                return;
            }

            _moveAction = playerMap.FindAction("Move");
            _lookAction = playerMap.FindAction("Look");
            _jumpAction = playerMap.FindAction("Jump");
            _interactAction = playerMap.FindAction("Interact");
            _attackAction = playerMap.FindAction("Attack");

            if (_jumpAction != null) _jumpAction.performed += OnJumpPerformed;
            if (_interactAction != null) _interactAction.performed += OnInteractPerformed;
            if (_attackAction != null) _attackAction.performed += OnAttackPerformed;

            playerMap.Enable();
        }

        public void Shutdown()
        {
            if (_jumpAction != null) _jumpAction.performed -= OnJumpPerformed;
            if (_interactAction != null) _interactAction.performed -= OnInteractPerformed;
            if (_attackAction != null) _attackAction.performed -= OnAttackPerformed;

            _inputActions?.FindActionMap(PlayerActionMapName, throwIfNotFound: false)?.Disable();
        }

        private void OnJumpPerformed(InputAction.CallbackContext context) => EventBus.Publish(new InputJumpEvent());
        private void OnInteractPerformed(InputAction.CallbackContext context) => EventBus.Publish(new InputInteractEvent());
        private void OnAttackPerformed(InputAction.CallbackContext context) => EventBus.Publish(new InputAttackEvent());
    }
}
