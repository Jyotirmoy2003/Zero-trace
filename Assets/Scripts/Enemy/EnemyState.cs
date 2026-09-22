using UnityEngine;

public abstract class EnemyState 
{
    public abstract void EnterState(EnemyBehaviour enemyBehaviour);
    public abstract void UpdateState(EnemyBehaviour enemyBehaviour);
    public abstract void ExitState(EnemyBehaviour enemyBehaviour);
    
}
