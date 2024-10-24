﻿using UnityEngine;
using UnityEngine.InputSystem;

using Framework;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class InputParser : MonoBehaviour
    {
        [SerializeField] private SceneSwitcher sceneSwitcher;
        [SerializeField] private Interacter interacter;

        [SerializeField, Range(0, 25)] private float interactRange = 1;

        private Movement _movement;

        private Camera _mainCam;
        private PlayerInput _playerInput;
        private InputActionAsset _inputActionAsset;
        
        private void Awake()
        {
            GetReferences();
            Init();
        }
        
        private void FixedUpdate()
        {
            Vector2 moveInput = _inputActionAsset["Move"].ReadValue<Vector2>();
            _movement.Move(moveInput);
        }

        private void OnEnable() => AddListeners();

        private void OnDisable() => RemoveListeners();
        
        public Vector2 GetMousePos()
        {
            Vector2 mousePos = _inputActionAsset["MousePosition"].ReadValue<Vector2>();
            return _mainCam.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        }
        
        private void GetReferences()
        {
            _mainCam = Camera.main;
            
            _playerInput = GetComponent<PlayerInput>();
            _movement = GetComponent<Movement>();
        }

        private void Init()
        {
            _inputActionAsset = _playerInput.actions;
        }

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

        private void Interact(InputAction.CallbackContext context) => interacter.CheckInteraction(interactRange);
        
        private void ResetLevel(InputAction.CallbackContext context) => sceneSwitcher.LoadScene();

        #endregion
    }
}