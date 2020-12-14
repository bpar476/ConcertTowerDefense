using UnityEngine;

public class GhostSpawner : MonoBehaviour
{

    /// <summary>
    /// Whether the spawner has spawned all of the ghosts it plans to spawn
    /// </summary>
    /// <value>True if there are no more ghosts to spawn. False otherwise</value>
    public bool Finished { get { return nextSpawnIndex >= spawnTimings.Length; } }

    [SerializeField]
    private GameObject ghostPrefab;

    [SerializeField]
    private float[] spawnTimings;

    /// <summary>
    /// If true, the spawner will start spawning ghosts on scene load. Otherwise it will wait until
    /// another object calls StartSpawner
    /// </summary>
    [SerializeField]
    private bool startImmediately;

    private int nextSpawnIndex = 0;

    private bool started = false;

    private float startTime;

    private void Start()
    {
        if (startImmediately)
        {
            StartSpawner();
        }
    }

    public void StartSpawner()
    {
        started = true;
        startTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        if (nextSpawnIndex < spawnTimings.Length && started)
        {
            if (Time.timeSinceLevelLoad - startTime > spawnTimings[nextSpawnIndex])
            {
                SpawnGhost();
                nextSpawnIndex++;
            }
        }
    }

    private void SpawnGhost()
    {
        Instantiate(ghostPrefab, transform.position, Quaternion.identity);
    }
}
