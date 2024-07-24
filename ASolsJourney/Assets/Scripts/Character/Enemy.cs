using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Enemy : Character
{
    public enum State
    {
        Idle,
        Chase,
        Attack
    }
    protected State curState;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float chaseDistance;
    // [SerializeField] private float attackDistance;

    protected GameObject target;
    protected float lastAttackTime;
    protected float targetDistance;

    [Header("Components")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    protected virtual void Start ()
    {
        target = FindObjectOfType<Player>().gameObject;
    }
    
    protected virtual void Update ()
    {   
        if(target == null) return;
        // Calculate the distance from us to the target.
        targetDistance = Vector2.Distance(transform.position, target.transform.position);

        // Flip the sprite to face the target.
        spriteRenderer.flipX = GetTargetDirection().x > 0;

        switch(curState)
        {
            case State.Idle: IdleUpdate(); break;
            case State.Chase: ChaseUpdate(); break;
            case State.Attack: AttackUpdate(); break;
        }
    }

    // Changes our current state.
    void ChangeState (State newState)
    {
        curState = newState;
    }
    // Called every frame while in the "Idle" state.
    void IdleUpdate ()
    {
        if(targetDistance <= chaseDistance)
            ChangeState(State.Chase);
    }
    // Called every frame while in the "Chase" state.
    void ChaseUpdate ()
    {
        if(InAttackRange())
            ChangeState(State.Attack);
        else if(targetDistance > chaseDistance)
            ChangeState(State.Idle);

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }


    // Called every frame while in the "Attack" state.
    void AttackUpdate ()
    {
        if(targetDistance > chaseDistance)
            ChangeState(State.Idle);
        else if(!InAttackRange())
            ChangeState(State.Chase);

        if(CanAttack()) 
        {
            lastAttackTime = Time.time;
            AttackTarget();
        }
    }
    protected virtual bool InAttackRange()
    {
        return false;
    }

    protected virtual bool CanAttack ()
    {
        return false;
    }

    protected virtual void AttackTarget ()
    {
        
    }

    public override void Die ()
    {
        DropItems();
        Destroy(gameObject);
    }
    void DropItems ()
    {

    }

    Vector2 GetTargetDirection ()
    {
        return (target.transform.position - transform.position).normalized;
    }


}