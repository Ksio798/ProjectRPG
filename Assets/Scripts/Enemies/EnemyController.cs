using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//using System.Linq;
//using UnityEditor.Rendering;
using System;

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
     ParticleSystem particleSystem;
    public event System.Action OnDie;
    bool isDied = false;
    public Dropping Drop;
    override protected void Start()
    {



        base.Start();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        EnemyViewZone evz = GetComponentInChildren<EnemyViewZone>();
        evz.OnObjEnterZone += OnObjEnterZone;
       // agent = GetComponent<NavMeshAgent>();
        //if (PointParent != null)
        //{
        //    SetWalkingPoints();
        //}
        transform.position = WalkingPoints[currentWalkingPoint].position;
        currentWalkingPoint++;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = stats.Speed;
    }
    public void SetWalkingPoints()
    {
        for (int i = 0; i < PointParent.childCount; i++)
        {
            WalkingPoints.Add(PointParent.GetChild(i));
          
        }
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
        if (!followTarget.gameObject.activeSelf)
        {
            Transform tr = FindObjectOfType<PlayerController>().transform;
            if (Vector3.Distance(transform.position, followTarget.position) <= FollowStopDistance)
            {

                followTarget = tr;
            }
           
        }

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
        else if(WalkingPoints != null&& WalkingPoints.Count != 0)
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
        particleSystem.Play();
        UpdateHp();
    }
    protected void UpdateHp()
    {
        image.fillAmount = stats.health / stats.MaxHealth;
    }
    public override void Die()
    {
        if (!isDied)
        {
        OnDie?.Invoke();
            if (UnityEngine.Random.Range(0, 3) ==0)
            {
            DropResurses();

            }
            isDied = true;
        }
        base.Die();
    }
void DropResurses()
    {
        Dropping d = Instantiate(Drop); 
        d.transform.position = transform.position;
       // d.Pos = gameObject.transform.position;
        float time = 30 * Time.fixedDeltaTime;
        Vector2 start = Vector2.up / time - 0.5f * Physics2D.gravity * time;
    
        d.GetComponent<Rigidbody2D>().velocity = start;
        d.GetComponent<Rigidbody2D>().AddTorque(UnityEngine.Random.Range(100, 500));
        d.StartCoroutine(d.WaitToStop());
    }
}


