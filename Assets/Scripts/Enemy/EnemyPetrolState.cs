using UnityEngine;

public class EnemyPetrolState : EnemyState
{

    EnemyBehaviour enemyBehaviour;
    private int index = -1;
    float reachDistance = 0.2f;
    private bool goingBack;

    void AssignNewPetrolPoint()
    {
        if(enemyBehaviour.isPetrolReverse)
        {
            if(index >= enemyBehaviour.pertrolPoints.Count-1)
            {
                goingBack = true;
            }else if(index <= 0)
            {
                goingBack = false;
            }


            if(goingBack ==  true)
            {
                index--;
            }
            else
            {
                index++;
            }

            enemyBehaviour.navMeshAgent.SetDestination(enemyBehaviour.pertrolPoints[index].position);
        }
        else
        {
            index = (index +1) % enemyBehaviour.pertrolPoints.Count;
            enemyBehaviour.navMeshAgent.SetDestination(enemyBehaviour.pertrolPoints[index].position);
        }
        
    }




    public override void EnterState(EnemyBehaviour enemyBehaviour)
    {
        this.enemyBehaviour = enemyBehaviour;
        AssignNewPetrolPoint();
    }

    public override void ExitState(EnemyBehaviour enemyBehaviour)
    {
        
    }

    public override void UpdateState(EnemyBehaviour enemyBehaviour)
    {
        if(enemyBehaviour.navMeshAgent.remainingDistance <= reachDistance) // reached to the point
        {
            enemyBehaviour.SwtichState(enemyBehaviour.enemyIdleState);
        }
    }
}
