using UnityEngine;

public class EnemyIdleState : EnemyState
{

    private float timmer = 0;
    private float waitTime = 3f;


    public override void EnterState(EnemyBehaviour enemyBehaviour)
    {
        enemyBehaviour.e_EnemyState = E_EnemyState.Idle;
    }

    public override void ExitState(EnemyBehaviour enemyBehaviour)
    {
        
    }

    public override void UpdateState(EnemyBehaviour enemyBehaviour)
    {
        if(timmer >= waitTime)
        {
            timmer = 0;
            enemyBehaviour.SwtichState(enemyBehaviour.enemyPetrolState);
        }
        else
        {
            timmer += Time.deltaTime;
        }
    }
}
