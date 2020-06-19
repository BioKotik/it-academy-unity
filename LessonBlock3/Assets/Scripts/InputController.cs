using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum InputCommand
{
    Fire,
    Skill,
    Move,

}

public class InputController : MonoBehaviour
{
    public static Action<InputCommand> OnInputAction;

    [SerializeField] private Button fireButton;
    [SerializeField] private Button skillButton;
    [SerializeField] private Button moveButton;

    private void Awake()
    {
        fireButton.onClick.AddListener(OnFireButton);
        skillButton.onClick.AddListener(OnSkillButton);
        moveButton.onClick.AddListener(OnMoveButton);
    }

    private void OnFireButton()
    {
        OnInputAction?.Invoke(InputCommand.Fire);
        Debug.Log("Fire");
    }

    private void OnSkillButton()
    {
        OnInputAction?.Invoke(InputCommand.Skill);
        Debug.Log("Skill");
    }

    private void OnMoveButton()
    {
        OnInputAction?.Invoke(InputCommand.Move);
        Debug.Log("Move");
    }

    // Start is called before the first frame update
    void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += OnInputTriggered;
    }

    private void OnInputTriggered(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        switch (context.action.name)
        {
            case "FireAction":
                OnFireButton();
                break;

            case "SkillAction":
                OnSkillButton();
                break;

            default:
                break;
        }
    }
}
