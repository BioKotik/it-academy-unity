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
    [SerializeField] private Transform skillPoint;
    [SerializeField] private Button fireButton;
    [SerializeField] private Button skillButton;
    [SerializeField] private Button deadShotButton;
    [SerializeField] private GameObject deadShotEffect;

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

        var obj = ObjectsPool.Instance.GetObject(ball[0]);
        obj.transform.position = firePoint.transform.position;
        obj.transform.rotation = firePoint.transform.rotation;
        var rig = obj.GetComponent<Rigidbody>();
        if (rig != null)
        {
            rig.velocity = Vector3.zero;
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

        ObjectsPool.Instance.PrepareObject(ball[0], 5);
        ObjectsPool.Instance.PrepareObject(ball[0], 10);
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

            case InputCommand.DeadShot:
                DeadShot();
                break;
                
            default:
                break;
        }
    }

    private void DeadShot()
    {
        if (characterState == CharacterState.Attack || characterState == CharacterState.Skill)
        {
            return;
        }

        animator.SetTrigger("Attack");
        DelayRun.Execute(delegate { characterState = CharacterState.Idle; }, 1.65f, gameObject);
        DelayRun.Execute(DeadShotExecute, 1.35f, gameObject);
    }

    private void DeadShotExecute()
    {
        var hits = new RaycastHit[] { };
        hits = Physics.RaycastAll(skillPoint.position, skillPoint.forward, 100f);

        if (hits == null)
        {
            return;
        }

        var obj = Instantiate(deadShotEffect, skillPoint.position, skillPoint.rotation);

        Destroy(obj, 1f);
        
        var healthObjects = new List<Health>();

        foreach (var hit in hits)
        {
            var health = hit.transform.GetComponent<Health>();
            if(health != null)
            {
                healthObjects.Add(health);
                if(healthObjects.Count == 3)
                {
                    break;
                }
            }
        }

        var timer = 0f;
        foreach (var item in healthObjects)
        {            
            DelayRun.Execute(delegate { item.SetDamage(int.MaxValue); }, timer, gameObject);
            timer += .2f;
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
