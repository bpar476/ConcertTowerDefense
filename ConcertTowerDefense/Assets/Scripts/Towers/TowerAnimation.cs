using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TowerAnimation : MonoBehaviour
{
    private static readonly string ANIM_TRIGGER_START_BEATING = "startBeating";
    private static readonly string ANIM_TRIGGER_SHOOT = "shoot";

    [SerializeField]
    private TowerPlacer towerPlacer;

    private Animator animator;
    private BeatSynchroniser synchroniser;

    public void ShootAnimation()
    {
        animator.SetTrigger(ANIM_TRIGGER_SHOOT);
    }

    private void OnEnable()
    {
        synchroniser = BeatSynchroniser.Instance;
        animator = GetComponent<Animator>();
        towerPlacer = TowerPlacer.Instance;
        towerPlacer.OnTowerPlaced += StartRockingIfIAmPlaced;
    }

    private void StartRockingIfIAmPlaced(InstrumentType type, BeatMappedShooter shooter)
    {
        if (shooter.gameObject.Equals(gameObject))
        {
            StartCoroutine(StartRockingOnNextBeat());
            towerPlacer.OnTowerPlaced -= StartRockingIfIAmPlaced;
        }
    }

    private IEnumerator StartRockingOnNextBeat()
    {
        var currentBeat = synchroniser.LoopBeatProgress;
        var nextBeat = Mathf.Ceil(currentBeat);
        while (synchroniser.LoopBeatProgress < nextBeat)
        {
            yield return new WaitForFixedUpdate();
        }

        animator.SetTrigger(ANIM_TRIGGER_START_BEATING);
    }
}
