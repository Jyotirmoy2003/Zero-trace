
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hivemind : MonoSingleton<Hivemind>
{
    [SerializeField] private List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();


    [SerializeField] private float communicationDistance = 10f;
    [SerializeField] private float searchRadius = 5f;
    [SerializeField] private float minimumPointDistance = 2f;
    [SerializeField] private float minimumPathDistanceBetwenPoints = 5f;



    void Start()
    {
        enemies.AddRange(
            FindObjectsByType<EnemyBehaviour>(FindObjectsSortMode.None)
        );
    }


    public void PlayerSeen(Vector3 lastKnownPosition,Quaternion lastKnownRoation,EnemyBehaviour detectingEnemy)
    {
        //Find enemys which are in communication range
        List<EnemyBehaviour> closeByEnemys = new List<EnemyBehaviour>();

        foreach(EnemyBehaviour enemyItem in enemies )
        {
            if(enemyItem == null) continue;

            float pathDistance = GetPathDistance(enemyItem.transform.position,detectingEnemy.transform.position);


            if(pathDistance <= communicationDistance) 
            {
                closeByEnemys.Add(enemyItem);
            }
        }

        //get valid sear points
        List<Vector3> SearchPoints = GenerateSearchPosition(lastKnownPosition,closeByEnemys.Count);


        //Assigen search points to the closest enemys
        for(int i=0; i< SearchPoints.Count ;i++)
        {
            closeByEnemys[i].CheckPlayer(SearchPoints[i],lastKnownRoation);
        }
    }

    private float GetPathDistance(Vector3 startPosition,Vector3 targetPosition)
    {
        if(startPosition == null) return Mathf.Infinity;

        NavMeshPath path = new NavMeshPath();

        bool pathFound = NavMesh.CalculatePath(startPosition,targetPosition,NavMesh.AllAreas,path);

        if(!pathFound || path.status != NavMeshPathStatus.PathComplete)
        {
            return Mathf.Infinity;
        }

        float distance = 0f;

        for(int i = 1 ; i < path.corners.Length ; i++ )
        {
            distance +=Vector3.Distance(path.corners[i-1],path.corners[i]);
        }

        return distance;
    }

    private List<Vector3> GenerateSearchPosition(Vector3 lastKnownPosition,int numberOfPoints)
    {
        List<Vector3> positions = new List<Vector3>();

        if(numberOfPoints <= 0) return positions;

        int attempts = 0;
        int maxAttempts = numberOfPoints * 20;

        while(positions.Count < numberOfPoints && attempts < maxAttempts)
        {
            attempts++;

            //Random direction around last knwn position
            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            Vector3 candidate = lastKnownPosition + new Vector3(randomDirection.x,0f,randomDirection.y) * Random.Range(2f,searchRadius);

            //check if this point is on navmesh
            if(!NavMesh.SamplePosition(candidate,out NavMeshHit hit,2f,NavMesh.AllAreas))
            {
                continue;
            }

            Vector3 validPosition = hit.position;

            //make sure those points are not too close to each other
            bool tooClose = false;

            foreach(Vector3 exisitingPosition in positions)
            {
                if(Vector3.Distance(validPosition,exisitingPosition) < minimumPointDistance )
                {
                    tooClose = true;
                    break;
                }
            }

            if(tooClose) continue;

            if(GetPathDistance(lastKnownPosition,validPosition) > minimumPathDistanceBetwenPoints)
            {
                continue;
            }

            positions.Add(validPosition);

            
        }

        return positions;

    }




}