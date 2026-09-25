using UnityEngine;

public class EnemyChaseState : EnemyState
{

    private EnemyBehaviour enemyBehaviour;
    private bool reachedToThePosition = false;
    
    public override void EnterState(EnemyBehaviour enemyBehaviour)
    {
        this.enemyBehaviour = enemyBehaviour;
        enemyBehaviour.navMeshAgent.enabled = true;
        enemyBehaviour.navMeshAgent.SetDestination(enemyBehaviour.lastKnownPlayerPos);
        enemyBehaviour.e_EnemyState = E_EnemyState.Chase;
    }

    public override void ExitState(EnemyBehaviour enemyBehaviour)
    {
        enemyBehaviour.navMeshAgent.enabled = true;
    }

    public override void UpdateState(EnemyBehaviour enemyBehaviour)
    {
        if(!reachedToThePosition && enemyBehaviour.navMeshAgent.remainingDistance <= enemyBehaviour.reachDistance) // reached to the point
        {
            
            enemyBehaviour.navMeshAgent.enabled = false;
            reachedToThePosition = true;
        }

        if(reachedToThePosition)
        {
            if(FaceLastKnownPlayerLookRoation())
            {
                enemyBehaviour.SwtichState(enemyBehaviour.enemyIdleState);
                
            }
        }
    }

    bool FaceLastKnownPlayerLookRoation()
    {
        enemyBehaviour.transform.rotation = Quaternion.Slerp( enemyBehaviour.transform.rotation, enemyBehaviour.lastKnownPlayerLookRotation,
        enemyBehaviour.rotationSpeed * Time.deltaTime);

        float angle = Quaternion.Angle(enemyBehaviour.transform.rotation,enemyBehaviour.lastKnownPlayerLookRotation);

        return angle<1f;
    }

  
}
