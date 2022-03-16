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
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""Attack3"",
                    ""type"": ""Button"",
                    ""id"": ""91e359e8-8cf6-496f-a819-e0bfaedef46b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.75,pressPoint=0.75)""
                },
                {
                    ""name"": ""Attack2"",
                    ""type"": ""Button"",
                    ""id"": ""22acb434-2147-4fc6-b8e0-61a601bd9888"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
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
                    ""name"": ""MovementLockOn"",
                    ""type"": ""Value"",
                    ""id"": ""c84850a3-92e1-4397-9d48-56a9b435098f"",
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
                },
                {
                    ""name"": ""Super1"",
                    ""type"": ""Button"",
                    ""id"": ""4ad80288-f276-49cc-8685-083bb69c76ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Super2"",
                    ""type"": ""Button"",
                    ""id"": ""cdfbf204-f3e1-4f6e-ac7e-5056e8d6e218"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Super3"",
                    ""type"": ""Button"",
                    ""id"": ""2f5b616c-be22-4646-bd53-5f9ea878998e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Super4"",
                    ""type"": ""Button"",
                    ""id"": ""4e932055-f81c-4f15-a6ce-799aa44d414d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact/Throw"",
                    ""type"": ""Button"",
                    ""id"": ""7887ad73-8d65-45ea-926b-f9f8cf6541dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""609ad033-4dee-49d1-9adf-d68e08ec0134"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Kick"",
                    ""type"": ""Button"",
                    ""id"": ""ce809615-7009-4bc6-a082-432b3ba158cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LockOn"",
                    ""type"": ""Button"",
                    ""id"": ""f0da6003-38a4-4df8-9f4a-139cbb504a8d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6974f7ba-81c4-496a-80e1-61b54584a270"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ebafb25-191e-4c99-a925-dcd779e2e5a1"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""id"": ""62f0c6e0-35f6-4db0-a3cb-af1073870b4c"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Super1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79e58ad3-8093-4451-b037-27724b951db9"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Super1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5019612-24fb-466b-b9c6-1fd1080b52c4"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Super2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""094cf6ff-55e7-4612-809e-518d440cfd2a"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Super2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39c11047-64f2-4c52-8cab-bc7a2c79527d"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Super3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7791c5e7-7bb4-4577-9c4f-c3a3aa632330"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Super3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0944bc9e-68b3-48b7-b0d0-1586d9718241"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Super4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9b45439-ec98-411e-8f37-cd8d80c18f31"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Super4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8837c95b-b326-416b-8fc9-b4ac171286c7"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Interact/Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5701e96b-b475-49c6-88f5-44e2089230b7"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact/Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f6363ca-96c8-4a3a-b4b7-b2d5973c8530"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7166f1a5-24d7-41e9-8d18-bf63a73f553b"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8c0f5e3-0276-42d7-abaa-3bc79d430e4f"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Kick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d905be97-e333-4fbd-b9cf-914cf0a21742"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Kick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef389adc-56ac-4690-8a53-c4aa59e47f0d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Kick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7153b43-bfda-4726-b92c-7a47f5fdaedf"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0935ca5d-6ea8-4bca-b0e9-5617dd594649"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""628a56b9-cf17-4143-8b19-8e0120e1ca62"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""SubHorizontal"",
                    ""id"": ""f7f14e0d-8f99-45a0-b7c7-f8220badbc27"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementLockOn"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b9352f2e-9cac-4347-ae5f-ca147b5532cc"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementLockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""387f2d07-8536-4a99-bbe3-b5b227f27dc7"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementLockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f3e2e859-777d-4721-bb7c-669060b4fc80"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db653d50-8572-49c7-acef-3eb283ce8e06"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ea4527f-3098-40df-9a2d-93e09cc55b8d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Attack3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f558594-7cfa-4b20-acc7-e9d81fc745ed"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack3"",
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
        m_PlayerCon_Attack3 = m_PlayerCon.FindAction("Attack3", throwIfNotFound: true);
        m_PlayerCon_Attack2 = m_PlayerCon.FindAction("Attack2", throwIfNotFound: true);
        m_PlayerCon_Movement = m_PlayerCon.FindAction("Movement", throwIfNotFound: true);
        m_PlayerCon_MovementLockOn = m_PlayerCon.FindAction("MovementLockOn", throwIfNotFound: true);
        m_PlayerCon_MouseLook = m_PlayerCon.FindAction("MouseLook", throwIfNotFound: true);
        m_PlayerCon_Super1 = m_PlayerCon.FindAction("Super1", throwIfNotFound: true);
        m_PlayerCon_Super2 = m_PlayerCon.FindAction("Super2", throwIfNotFound: true);
        m_PlayerCon_Super3 = m_PlayerCon.FindAction("Super3", throwIfNotFound: true);
        m_PlayerCon_Super4 = m_PlayerCon.FindAction("Super4", throwIfNotFound: true);
        m_PlayerCon_InteractThrow = m_PlayerCon.FindAction("Interact/Throw", throwIfNotFound: true);
        m_PlayerCon_Dodge = m_PlayerCon.FindAction("Dodge", throwIfNotFound: true);
        m_PlayerCon_Kick = m_PlayerCon.FindAction("Kick", throwIfNotFound: true);
        m_PlayerCon_LockOn = m_PlayerCon.FindAction("LockOn", throwIfNotFound: true);
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
    private readonly InputAction m_PlayerCon_Attack3;
    private readonly InputAction m_PlayerCon_Attack2;
    private readonly InputAction m_PlayerCon_Movement;
    private readonly InputAction m_PlayerCon_MovementLockOn;
    private readonly InputAction m_PlayerCon_MouseLook;
    private readonly InputAction m_PlayerCon_Super1;
    private readonly InputAction m_PlayerCon_Super2;
    private readonly InputAction m_PlayerCon_Super3;
    private readonly InputAction m_PlayerCon_Super4;
    private readonly InputAction m_PlayerCon_InteractThrow;
    private readonly InputAction m_PlayerCon_Dodge;
    private readonly InputAction m_PlayerCon_Kick;
    private readonly InputAction m_PlayerCon_LockOn;
    public struct PlayerConActions
    {
        private @PlayerActions m_Wrapper;
        public PlayerConActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack1 => m_Wrapper.m_PlayerCon_Attack1;
        public InputAction @Attack3 => m_Wrapper.m_PlayerCon_Attack3;
        public InputAction @Attack2 => m_Wrapper.m_PlayerCon_Attack2;
        public InputAction @Movement => m_Wrapper.m_PlayerCon_Movement;
        public InputAction @MovementLockOn => m_Wrapper.m_PlayerCon_MovementLockOn;
        public InputAction @MouseLook => m_Wrapper.m_PlayerCon_MouseLook;
        public InputAction @Super1 => m_Wrapper.m_PlayerCon_Super1;
        public InputAction @Super2 => m_Wrapper.m_PlayerCon_Super2;
        public InputAction @Super3 => m_Wrapper.m_PlayerCon_Super3;
        public InputAction @Super4 => m_Wrapper.m_PlayerCon_Super4;
        public InputAction @InteractThrow => m_Wrapper.m_PlayerCon_InteractThrow;
        public InputAction @Dodge => m_Wrapper.m_PlayerCon_Dodge;
        public InputAction @Kick => m_Wrapper.m_PlayerCon_Kick;
        public InputAction @LockOn => m_Wrapper.m_PlayerCon_LockOn;
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
                @Attack3.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAttack3;
                @Attack3.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAttack3;
                @Attack3.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAttack3;
                @Attack2.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAttack2;
                @Attack2.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAttack2;
                @Attack2.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAttack2;
                @Movement.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMovement;
                @MovementLockOn.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMovementLockOn;
                @MovementLockOn.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMovementLockOn;
                @MovementLockOn.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMovementLockOn;
                @MouseLook.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMouseLook;
                @Super1.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSuper1;
                @Super1.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSuper1;
                @Super1.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSuper1;
                @Super2.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSuper2;
                @Super2.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSuper2;
                @Super2.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSuper2;
                @Super3.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSuper3;
                @Super3.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSuper3;
                @Super3.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSuper3;
                @Super4.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSuper4;
                @Super4.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSuper4;
                @Super4.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSuper4;
                @InteractThrow.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnInteractThrow;
                @InteractThrow.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnInteractThrow;
                @InteractThrow.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnInteractThrow;
                @Dodge.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnDodge;
                @Kick.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnKick;
                @Kick.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnKick;
                @Kick.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnKick;
                @LockOn.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnLockOn;
                @LockOn.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnLockOn;
                @LockOn.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnLockOn;
            }
            m_Wrapper.m_PlayerConActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack1.started += instance.OnAttack1;
                @Attack1.performed += instance.OnAttack1;
                @Attack1.canceled += instance.OnAttack1;
                @Attack3.started += instance.OnAttack3;
                @Attack3.performed += instance.OnAttack3;
                @Attack3.canceled += instance.OnAttack3;
                @Attack2.started += instance.OnAttack2;
                @Attack2.performed += instance.OnAttack2;
                @Attack2.canceled += instance.OnAttack2;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @MovementLockOn.started += instance.OnMovementLockOn;
                @MovementLockOn.performed += instance.OnMovementLockOn;
                @MovementLockOn.canceled += instance.OnMovementLockOn;
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
                @Super1.started += instance.OnSuper1;
                @Super1.performed += instance.OnSuper1;
                @Super1.canceled += instance.OnSuper1;
                @Super2.started += instance.OnSuper2;
                @Super2.performed += instance.OnSuper2;
                @Super2.canceled += instance.OnSuper2;
                @Super3.started += instance.OnSuper3;
                @Super3.performed += instance.OnSuper3;
                @Super3.canceled += instance.OnSuper3;
                @Super4.started += instance.OnSuper4;
                @Super4.performed += instance.OnSuper4;
                @Super4.canceled += instance.OnSuper4;
                @InteractThrow.started += instance.OnInteractThrow;
                @InteractThrow.performed += instance.OnInteractThrow;
                @InteractThrow.canceled += instance.OnInteractThrow;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @Kick.started += instance.OnKick;
                @Kick.performed += instance.OnKick;
                @Kick.canceled += instance.OnKick;
                @LockOn.started += instance.OnLockOn;
                @LockOn.performed += instance.OnLockOn;
                @LockOn.canceled += instance.OnLockOn;
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
        void OnAttack3(InputAction.CallbackContext context);
        void OnAttack2(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnMovementLockOn(InputAction.CallbackContext context);
        void OnMouseLook(InputAction.CallbackContext context);
        void OnSuper1(InputAction.CallbackContext context);
        void OnSuper2(InputAction.CallbackContext context);
        void OnSuper3(InputAction.CallbackContext context);
        void OnSuper4(InputAction.CallbackContext context);
        void OnInteractThrow(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnKick(InputAction.CallbackContext context);
        void OnLockOn(InputAction.CallbackContext context);
    }
}
