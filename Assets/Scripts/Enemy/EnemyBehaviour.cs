
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
    public EnemyAttackState enemyAttackState= new EnemyAttackState();

    public E_EnemyState e_EnemyState;

    #endregion


    public List<Transform> pertrolPoints = new List<Transform>();
    
    public NavMeshAgent navMeshAgent;
    public bool isPetrolReverse = false;


    public Transform enemyEyeTranform;
    public Transform playerTransform;
    public float detectionRange =4;
    public float fieldOfView = 60f;
    public Vector3 lastKnownPlayerPos;
    public float reachDistance = 0.2f;

    public float gunDamage = 10f;
    public float gunRateOfFire = 2f;
    public LayerMask bulletLayer;
    public Bullet bulletPrefab;
    public Transform gunPointTranform;

   

    void Start()
    {
        currentState = enemyIdleState;
        currentState.EnterState(this);
    }

   


    void Update()
    {
        currentState.UpdateState(this);
        if(DetectPlayer())
        {
            lastKnownPlayerPos = playerTransform.position;
            SwtichState(enemyAttackState);
        }
        else
        {
            if(currentState == enemyAttackState) // we just lost the player
            {
                SwtichState(enemyChaseState);
            }
        }
    }

    public void SwtichState(EnemyState newState)
    {
        currentState.ExitState(this);
        newState.EnterState(this);
        currentState = newState;
    }



    bool DetectPlayer()
    {
        Vector3 directionToPlayer = playerTransform.position - enemyEyeTranform.position;

        if(directionToPlayer.magnitude > detectionRange)
            return false;

        float angle = Vector3.Angle(enemyEyeTranform.forward,directionToPlayer);
        if(angle > fieldOfView / 2f)
            return false;

        //Raycast 
        if(Physics.Raycast(transform.position,directionToPlayer.normalized,out RaycastHit hit, detectionRange,~bulletLayer))
        {
            Debug.Log(hit.transform == playerTransform);
            if(hit.transform == playerTransform)
            {
                return true;
            }
        }

        return false;
    }

    public void ShotAtPlayer()
    {
        Instantiate(bulletPrefab,gunPointTranform.position,gunPointTranform.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        // Detection range
        Gizmos.DrawWireSphere(enemyEyeTranform.position, detectionRange);

        // FOV lines
        Vector3 leftDirection = Quaternion.Euler(0, -fieldOfView / 2f, 0) 
                                * enemyEyeTranform.forward;

        Vector3 rightDirection = Quaternion.Euler(0, fieldOfView / 2f, 0) 
                                * enemyEyeTranform.forward;

        Gizmos.DrawLine(
            enemyEyeTranform.position,
            enemyEyeTranform.position + leftDirection * detectionRange
        );

        Gizmos.DrawLine(
            enemyEyeTranform.position,
            enemyEyeTranform.position + rightDirection * detectionRange
        );
    }

}


public enum E_EnemyState
{
    None,
    Idle,
    Petrol,
    Chase,
    Attatck,
    Death,
}