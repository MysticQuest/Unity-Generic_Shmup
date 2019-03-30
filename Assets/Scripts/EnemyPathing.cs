using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;

    //[SerializeField] GameObject pathObj;
    List<Transform> path;
    public int pathIndex = 0;
    public Vector3 targetPos;

    void Start()
    {
        //ParsePathObject();

        path = waveConfig.GetWaypoints();
        transform.position = path[pathIndex].transform.position;
    }

    /*private void ParsePathObject()
    {
        Transform[] pathArray = pathObj.GetComponentsInChildren<Transform>();
        path.AddRange(pathArray);
        path.RemoveAt(0);
    }*/

    void Update()
    {
        Pathing();
    }

    //method that is used to change wave configs for the enemy on the fly
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Pathing()
    {
        if (pathIndex <= path.Count - 1)
        {
            var targetPos = path[pathIndex].transform.position;
            var genSpeed = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, genSpeed);
            if (transform.position == targetPos)
            {
                pathIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
