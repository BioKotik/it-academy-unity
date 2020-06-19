using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum EnemyState
{ 
    Sleep,
    Wait,
    StartWalk,
    Walk,
    StartAttack,
    Attack,
    StartRun,
    Run,

}

public class Enemy : MonoBehaviour, IEnemy, IHitBox
{
    [SerializeField] private int health = 1;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform checkGroundPoint;
    [SerializeField] private Transform checkAttackPoint;
    [SerializeField] private Transform graphics;
    [SerializeField] private Transform helpers;

    private GameManager gameManager;
    private EnemyState currentEnemyState;
    private EnemyState nextState;
    private float wakeUpTimer;
    private float cowardiceTimer = 2f;
    private float waitTimer;
    private float attackTimer;
    private float currentDirection = 1;

    public int Health
    {
        get => health;
        private set
        {
            health = value;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 0.5f);
    }

    public void Hit(int damage)
    {
        Health -= damage;
        currentEnemyState = EnemyState.StartRun;
    }
    public void RegisterEnemy()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.Enemies.Add(this);
    }

    private void Awake()
    {
        RegisterEnemy();

        wakeUpTimer = Time.time + 1f;
    }

    private void Update()
    {
        switch (currentEnemyState)
        {
            case EnemyState.Sleep:
                Sleep();
                break;

            case EnemyState.Wait:
                Wait();
                break;

            case EnemyState.StartWalk:
                animator.SetInteger("Walking", 1);
                currentEnemyState = EnemyState.Walk;
                break;

            case EnemyState.Walk:
                Walk();
                break;

            case EnemyState.StartAttack:
                animator.SetTrigger("Attack");
                ((IHitBox)gameManager.Player).Hit(1);
                currentEnemyState = EnemyState.Attack;
                break;

            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.StartRun:
                currentEnemyState = EnemyState.Run;
                StartRun();
                break;

            case EnemyState.Run:
                Run();
                break;

            default:
                return;
        }
    }

    private void StartSleeping(float sleepTime = 1f)
    {
        wakeUpTimer = Time.time + sleepTime;
        currentEnemyState = EnemyState.Sleep;
    }

    private void Sleep()
    {
        if(Time.time >= wakeUpTimer)
        {
            WakeUp();
        }
    }

    private void WakeUp()
    {
        var playerPosition = ((MonoBehaviour)gameManager.Player).transform.position;
        if(Vector3.Distance(transform.position, playerPosition) > 6f)
        {
            wakeUpTimer = Time.time + 1;
            return;
        }

        currentEnemyState = EnemyState.Wait;
        nextState = EnemyState.StartWalk;
        waitTimer = Time.time + 0.1f;
    }

    private void Wait()
    {
        if(Time.time >= waitTimer)
        {
            currentEnemyState = nextState;
        }
    }

    private void Walk()
    {
        transform.Translate(transform.right * Time.deltaTime * currentDirection);
        
        //проверяем возможность идти дальше
        RaycastHit2D hit = Physics2D.Raycast(checkGroundPoint.position,Vector2.down, 0.3f);      

        if (hit.collider == null)
        {
            currentDirection *= -1;
            graphics.localScale = new Vector3(currentDirection, 1f, 1f);

            float xAngle = currentDirection > 0 ? 0f : 180f;
            helpers.localEulerAngles = new Vector3(0f, xAngle, 0f);

            currentEnemyState = EnemyState.Wait;
            nextState = EnemyState.StartWalk;

            waitTimer = Time.time + 0.2f;

            animator.SetInteger("Walking", 0);
            return;
        }

        //проверяем возможность атаковать
        hit = Physics2D.Raycast(checkAttackPoint.position, checkAttackPoint.right, 0.3f);
        if(hit.collider != null)
        {
            var player = hit.collider.GetComponent<Player>();
            if(player != null)
            {
                currentEnemyState = EnemyState.StartAttack;
            }    
        }
        
    }

    private void Attack()
    {
        if(Time.time < attackTimer)
        {
            return;
        }

        currentEnemyState = EnemyState.Wait;
        nextState = EnemyState.StartWalk;
        waitTimer = Time.time + 0.2f;
    }

    private void Run()
    {
        currentEnemyState = EnemyState.Wait;
        nextState = EnemyState.StartWalk;
        waitTimer = Time.time + 0.2f;
    }

    private void StartRun()
    {
        currentDirection *= -1;
        graphics.localScale = new Vector3(currentDirection, 1f, 1f);

        float xAngle = currentDirection > 0 ? 0f : 180f;
        helpers.localEulerAngles = new Vector3(0f, xAngle, 0f);

        currentEnemyState = EnemyState.Run;
        nextState = EnemyState.StartWalk;

        animator.SetInteger("Walking", 1);
    }
}
