using UnityEngine;

public class EnemyAttackState : EnemyState
{

    private float nextShotTime;
    private EnemyBehaviour enemyBehaviour;
   
    public override void EnterState(EnemyBehaviour enemyBehaviour)
    {
        this.enemyBehaviour = enemyBehaviour;
        enemyBehaviour.navMeshAgent.enabled = false;
        enemyBehaviour.e_EnemyState = E_EnemyState.Attatck;
    }

    public override void ExitState(EnemyBehaviour enemyBehaviour)
    {
        enemyBehaviour.navMeshAgent.enabled = true;
    }

    public override void UpdateState(EnemyBehaviour enemyBehaviour)
    {
        if(Time.time >= nextShotTime)
        {
            enemyBehaviour.ShotAtPlayer();
            nextShotTime = Time.time + (1f/enemyBehaviour.gunRateOfFire);
        }

        FacePlayer();
    }

   
    void FacePlayer()
    {
        Vector3 direction = enemyBehaviour.playerTransform.position - enemyBehaviour.transform.position;
            direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyBehaviour.transform.rotation = Quaternion.Slerp(
                enemyBehaviour.transform.rotation,
                targetRotation,
                enemyBehaviour.rotationSpeed * Time.deltaTime
            );
        }
    }
}
