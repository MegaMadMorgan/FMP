// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/PlayerActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""PlayerCon"",
            ""id"": ""11368c2b-5dc6-4687-b34b-08c5f432d869"",
            ""actions"": [
                {
                    ""name"": ""Attack1"",
                    ""type"": ""Button"",
                    ""id"": ""a3a15c64-6707-4728-82f9-d1876c15287e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Attack2"",
                    ""type"": ""Button"",
                    ""id"": ""22acb434-2147-4fc6-b8e0-61a601bd9888"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""6e79c885-8e43-4430-abad-237dab9c4715"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""PassThrough"",
                    ""id"": ""698f67ce-616b-4d3d-ae03-6d87c81a755c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6974f7ba-81c4-496a-80e1-61b54584a270"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""axis"",
                    ""id"": ""da4b4b4d-157d-4006-af54-f24f88611abc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""881bfb9d-79f1-4c9a-8e97-c9777275f446"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""17e251cd-9f42-44e2-9432-abad519bef9a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0c391da6-b099-464d-8180-805c65f299dd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cef826b1-a043-49b5-a8cf-a82171260756"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e8441cbe-3ddf-4626-b9d6-8ee33e421278"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3e2e859-777d-4721-bb7c-669060b4fc80"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerCon
        m_PlayerCon = asset.FindActionMap("PlayerCon", throwIfNotFound: true);
        m_PlayerCon_Attack1 = m_PlayerCon.FindAction("Attack1", throwIfNotFound: true);
        m_PlayerCon_Attack2 = m_PlayerCon.FindAction("Attack2", throwIfNotFound: true);
        m_PlayerCon_Movement = m_PlayerCon.FindAction("Movement", throwIfNotFound: true);
        m_PlayerCon_MouseLook = m_PlayerCon.FindAction("MouseLook", throwIfNotFound: true);
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

    // PlayerCon
    private readonly InputActionMap m_PlayerCon;
    private IPlayerConActions m_PlayerConActionsCallbackInterface;
    private readonly InputAction m_PlayerCon_Attack1;
    private readonly InputAction m_PlayerCon_Attack2;
    private readonly InputAction m_PlayerCon_Movement;
    private readonly InputAction m_PlayerCon_MouseLook;
    public struct PlayerConActions
    {
        private @PlayerActions m_Wrapper;
        public PlayerConActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack1 => m_Wrapper.m_PlayerCon_Attack1;
        public InputAction @Attack2 => m_Wrapper.m_PlayerCon_Attack2;
        public InputAction @Movement => m_Wrapper.m_PlayerCon_Movement;
        public InputAction @MouseLook => m_Wrapper.m_PlayerCon_MouseLook;
        public InputActionMap Get() { return m_Wrapper.m_PlayerCon; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerConActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerConActions instance)
        {
            if (m_Wrapper.m_PlayerConActionsCallbackInterface != null)
            {
                @Attack1.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAttack1;
                @Attack1.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAttack1;
                @Attack1.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAttack1;
                @Attack2.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAttack2;
                @Attack2.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAttack2;
                @Attack2.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAttack2;
                @Movement.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMovement;
                @MouseLook.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMouseLook;
            }
            m_Wrapper.m_PlayerConActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack1.started += instance.OnAttack1;
                @Attack1.performed += instance.OnAttack1;
                @Attack1.canceled += instance.OnAttack1;
                @Attack2.started += instance.OnAttack2;
                @Attack2.performed += instance.OnAttack2;
                @Attack2.canceled += instance.OnAttack2;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
            }
        }
    }
    public PlayerConActions @PlayerCon => new PlayerConActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerConActions
    {
        void OnAttack1(InputAction.CallbackContext context);
        void OnAttack2(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnMouseLook(InputAction.CallbackContext context);
    }
}
