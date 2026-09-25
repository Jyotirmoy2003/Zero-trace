using UnityEngine;

public class EnemyChaseState : EnemyState
{

    private EnemyBehaviour enemyBehaviour;
    public override void EnterState(EnemyBehaviour enemyBehaviour)
    {
        this.enemyBehaviour = enemyBehaviour;
        enemyBehaviour.navMeshAgent.SetDestination(enemyBehaviour.lastKnownPlayerPos);
        enemyBehaviour.e_EnemyState = E_EnemyState.Chase;
    }

    public override void ExitState(EnemyBehaviour enemyBehaviour)
    {
        
    }

    public override void UpdateState(EnemyBehaviour enemyBehaviour)
    {
        if(enemyBehaviour.navMeshAgent.remainingDistance <= enemyBehaviour.reachDistance) // reached to the point
        {
            enemyBehaviour.SwtichState(enemyBehaviour.enemyIdleState);
        }
    }

  
}
