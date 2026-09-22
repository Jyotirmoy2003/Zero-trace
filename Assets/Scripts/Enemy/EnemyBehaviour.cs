
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    #region States
    public EnemyState currentState;
    public EnemyIdleState enemyIdleState = new EnemyIdleState();
    public EnemyPetrolState enemyPetrolState = new EnemyPetrolState();
    public EnemyChaseState enemyChaseState = new EnemyChaseState();
    public EnemyDeathState enemyDeathState = new EnemyDeathState();

    #endregion


    public List<Transform> pertrolPoints = new List<Transform>();
    
    public NavMeshAgent navMeshAgent;
    public bool isPetrolReverse = false;

   

    void Start()
    {
        currentState = enemyIdleState;
        currentState.EnterState(this);
    }

   


    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwtichState(EnemyState newState)
    {
        currentState.ExitState(this);
        newState.EnterState(this);
        currentState = newState;
    }



}


public class enemystateattacctk : EnemyState
{
    public override void EnterState(EnemyBehaviour enemyBehaviour)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(EnemyBehaviour enemyBehaviour)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyBehaviour enemyBehaviour)
    {
        throw new System.NotImplementedException();
    }
}