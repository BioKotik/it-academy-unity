// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""b6808a35-9e68-4d76-806c-96aac94bea2c"",
            ""actions"": [
                {
                    ""name"": ""FireAction"",
                    ""type"": ""Button"",
                    ""id"": ""3b08183e-5c6e-4a9f-adf6-9777f47cbe04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SkillAction"",
                    ""type"": ""Button"",
                    ""id"": ""04fe8b79-4265-4be6-ad52-2881e06ecd5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7e56d5db-aa9c-45e0-93e0-f2711cc7280b"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""890e36b5-9d98-4e5c-884a-ef79544b5f69"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_FireAction = m_Player.FindAction("FireAction", throwIfNotFound: true);
        m_Player_SkillAction = m_Player.FindAction("SkillAction", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_FireAction;
    private readonly InputAction m_Player_SkillAction;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @FireAction => m_Wrapper.m_Player_FireAction;
        public InputAction @SkillAction => m_Wrapper.m_Player_SkillAction;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @FireAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireAction;
                @FireAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireAction;
                @FireAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireAction;
                @SkillAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkillAction;
                @SkillAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkillAction;
                @SkillAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkillAction;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @FireAction.started += instance.OnFireAction;
                @FireAction.performed += instance.OnFireAction;
                @FireAction.canceled += instance.OnFireAction;
                @SkillAction.started += instance.OnSkillAction;
                @SkillAction.performed += instance.OnSkillAction;
                @SkillAction.canceled += instance.OnSkillAction;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnFireAction(InputAction.CallbackContext context);
        void OnSkillAction(InputAction.CallbackContext context);
    }
}
