using UnityEngine;

public class GhostSpawner : MonoBehaviour
{

    /// <summary>
    /// The number of ghosts that this spawner will spawn this level
    /// </summary>
    public int GhostCount { get { return spawnTimings.Length; } }

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
