using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : BaseCharecter
{
 
    float movementDelta = 0.5f;
     List<Transform> WalkingPoints = new List<Transform>();
    int currentWalkingPoint = 0;
    public Transform PointParent;
    public float FollowStopDistance = 2;
    protected Transform followTarget;
    [HideInInspector]
   public NavMeshAgent agent;
    public float PlayerAttackDistance;
    public float TimeToAttack;
    public Image image;
  protected float timer;
    //  В МЕТОДЕ СТАРТ ПОСТАВИМ ВРАГА В НАЧАЛЬНУЮ ТОЧКУ МАРШРУТА
    override protected void Start()
    {
        base.Start();
        for (int i = 0; i < PointParent.childCount; i++)
        {
            WalkingPoints.Add(PointParent.GetChild(i));
        }
        EnemyViewZone evz = GetComponentInChildren<EnemyViewZone>();
        evz.OnObjEnterZone += OnObjEnterZone;
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = stats.Speed;
        transform.position = WalkingPoints[currentWalkingPoint].position;
        currentWalkingPoint++;

    }
    virtual protected void Update()
    {
        if (followTarget != null && Vector2.Distance(transform.position, followTarget.position) <= PlayerAttackDistance)
            Attack();
        Move();
        TimerMath();
    }

    // Защищенный метод движения по маршруту
    protected virtual void MoveByRoute()
    {

        if (Vector2.Distance(transform.position, WalkingPoints[currentWalkingPoint].position) > movementDelta)
        {
            
            agent.SetDestination(WalkingPoints[currentWalkingPoint].position);
        }
        else
        {
            currentWalkingPoint++;
            if (currentWalkingPoint == WalkingPoints.Count)
                currentWalkingPoint = 0;
        }


    }
    protected virtual void FollowTarget()
    {

        if (Vector2.Distance(transform.position, followTarget.position) > PlayerAttackDistance)
            agent.SetDestination(followTarget.position);
        else
            agent.SetDestination(transform.position);
          

        if (Vector3.Distance(transform.position, followTarget.position) > FollowStopDistance)
        {

            followTarget = null;
            Debug.Log("followTarget = null");
        }


    }


    private void OnObjEnterZone(Transform other)
    {
        // проверим по тегу, что тот с кем мы столкнулись other – это игрок
        if (other.tag == "Player")
        {
            followTarget = other.transform;
            Debug.Log("followTarget ");
        }
        
    }
    protected virtual void Attack()
    {
    }
    protected virtual void Move()
    {
        if (followTarget != null)
        {
            FollowTarget();
           
        }
        else
            MoveByRoute();
    }
    protected virtual void TimerMath()
    {
        if (timer < TimeToAttack)
        {
            timer += Time.deltaTime;
        }
    }
    public override void TakeDamage(float Dmg)
    {
        base.TakeDamage(Dmg);
        UpdateHp();
    }
   protected void UpdateHp()
    {
        image.fillAmount = stats.health / stats.MaxHealth;
    }
}


