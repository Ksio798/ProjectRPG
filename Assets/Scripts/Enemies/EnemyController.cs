using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public float MovementSpeed = 2;
    float movementDelta = 0.5f;
     List<Transform> WalkingPoints = new List<Transform>();
    int currentWalkingPoint = 0;
    public Transform PointParent;
    public float FollowStopDistance = 2;
    protected Transform followTarget;
    NavMeshAgent agent;
    public float PlayerAttackDistance;
    //  В МЕТОДЕ СТАРТ ПОСТАВИМ ВРАГА В НАЧАЛЬНУЮ ТОЧКУ МАРШРУТА
    virtual protected void Start()
    {
        for (int i = 0; i < PointParent.childCount; i++)
        {
            WalkingPoints.Add(PointParent.GetChild(i));
        }
        EnemyViewZone evz = GetComponentInChildren<EnemyViewZone>();
        evz.OnObjEnterZone += OnObjEnterZone;
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        transform.position = WalkingPoints[currentWalkingPoint].position;
        currentWalkingPoint++;
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
    protected void FollowTarget()
    {

        if (Vector2.Distance(transform.position, followTarget.position) > PlayerAttackDistance)
            agent.SetDestination(followTarget.position);
        else
            agent.SetDestination(transform.position);
           // agent.isStopped = true;

        if (Vector3.Distance(transform.position, followTarget.position) > FollowStopDistance)
        {

            followTarget = null;

        }


    }


    private void OnObjEnterZone(Transform other)
    {
        // проверим по тегу, что тот с кем мы столкнулись other – это игрок
        if (other.tag == "Player")
        {
            followTarget = other.transform;
            Debug.Log("followTarget");
        }
        
    }

}


