using UnityEngine;
using UnityEngine.InputSystem;

using Framework;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class InputParser : MonoBehaviour
    {
        [SerializeField] private SceneSwitcher sceneSwitcher;
        
        private PlayerInput _playerInput;
        private InputActionAsset _inputActionAsset;
        
        private void Awake()
        {
            GetReferences();
            Init();
        }

        private void OnEnable() => AddListeners();

        private void OnDisable() => RemoveListeners();
        
        private void GetReferences() => _playerInput = GetComponent<PlayerInput>();

        private void Init() => _inputActionAsset = _playerInput.actions;

        private void AddListeners()
        {
            _inputActionAsset["Interact"].performed += Interact;
            _inputActionAsset["ResetLevel"].performed += ResetLevel;
        }

        private void RemoveListeners()
        {
            _inputActionAsset["Interact"].performed -= Interact;
            _inputActionAsset["ResetLevel"].performed -= ResetLevel;
        }

        #region Context

        private void Interact(InputAction.CallbackContext context) => Debug.Log("Interact");
        
        private void ResetLevel(InputAction.CallbackContext context) => sceneSwitcher.LoadScene();

        #endregion
    }
}