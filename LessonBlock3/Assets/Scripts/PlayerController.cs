using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterState
{
    Idle,
    Move,
    Attack,
    Skill,
    Hit,
    Dead,

}

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private CharacterState characterState;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] ball;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Button fireButton;
    [SerializeField] private Button skillButton;

    public void AttackEvent()
    {
        StartCoroutine(AttackCoroutine());
    }

    public void SkillEvent()
    {
        StartCoroutine(SkillCoroutine());       
    }

    public IEnumerator AttackCoroutine()
    {
        fireButton.interactable = false;

        var obj = Instantiate(ball[0], firePoint.position, Quaternion.identity);
        var rig = obj.GetComponent<Rigidbody>();
        if (rig != null)
        {
            rig.AddForce(Vector3.forward * 5f, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(5f);

        fireButton.interactable = true;

    }

    public IEnumerator SkillCoroutine()
    {
        skillButton.interactable = false;

        for (int i = 0; i < 5; i++)
        {
            var obj = Instantiate(ball[i], firePoint.position, Quaternion.identity);
            var rig = obj.GetComponent<Rigidbody>();

            if (rig != null)
            {
                rig.AddForce(Vector3.forward * 5f, ForceMode.Impulse);
            }

            yield return new WaitForSecondsRealtime(.1f);
        }

        yield return new WaitForSeconds(5f);

        skillButton.interactable = true;

    }

    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        characterState = CharacterState.Idle;
        InputController.OnInputAction += OnInputCommand;
    }

    private void OnDestroy()
    {
        InputController.OnInputAction -= OnInputCommand;
    }

    private void OnInputCommand(InputCommand command)
    {
        switch (command)
        {
            case InputCommand.Fire:
                Attack();
                break;

            case InputCommand.Skill:
                Skill();
                break;

            case InputCommand.Move:
                Move();
                break;

            default:
                break;
        }
    }

    private void Move()
    {
        animator.SetTrigger("Move");

        transform.Translate(Vector3.forward);
        characterState = CharacterState.Move;

        DelayRun.Execute(delegate
        {
            characterState = CharacterState.Move;
        },  .5f, gameObject);
    }

    private void Attack()
    {
        if (characterState == CharacterState.Attack || characterState == CharacterState.Skill)
        {
            return;
        }

        animator.SetTrigger("Attack");
        characterState = CharacterState.Attack;

        DelayRun.Execute(delegate 
        { 
            characterState = CharacterState.Idle; 
        },  .5f, gameObject);
    }

    private void Skill()
    {
        if (characterState == CharacterState.Attack || characterState == CharacterState.Skill)
        {
            return;
        }

        animator.SetTrigger("Skill");
        characterState = CharacterState.Skill;

        DelayRun.Execute(delegate
        {
            characterState = CharacterState.Idle;
        },  1f, gameObject);
    }

    private void Run()
    {
        if (characterState == CharacterState.Move)
        {
            return;
        }

        animator.SetTrigger("Skill");
        characterState = CharacterState.Skill;

        DelayRun.Execute(delegate
        {
            characterState = CharacterState.Idle;
        }, 1f, gameObject);
    }
}
