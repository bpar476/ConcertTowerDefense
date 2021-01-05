using System.Linq;
using UnityEngine;

public class VictoryObserver : Singleton<VictoryObserver>
{

    [SerializeField]
    private GhostSpawner[] spawners;

    [SerializeField]
    private GameObject victoryUI;

    private int totalGhostsInLevel;

    private int numGhostsKilled = 0;

    protected override VictoryObserver Init()
    {
        totalGhostsInLevel = spawners.Select(spawner => spawner.GhostCount).Sum();
        return this;
    }

    protected override bool ShouldNotDestroyOnLoad()
    {
        return false;
    }

    public void ReportGhostKilled()
    {
        numGhostsKilled++;

        if (numGhostsKilled == totalGhostsInLevel)
        {
            DoVictory();
        }
    }

    private void DoVictory()
    {
        victoryUI.SetActive(true);
    }
}
