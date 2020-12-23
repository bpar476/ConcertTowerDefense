using System.Collections.Generic;
using UnityEngine;

public class BandProgression : Singleton<BandProgression>
{

    public int BandLevel
    {
        get
        {
            return bandComposition.Count;
        }
    }


    [SerializeField]
    private NotificationSystem notificationSystem;

    private HashSet<InstrumentType> bandComposition;
    private TowerPlacer placer;

    protected override BandProgression Init()
    {
        bandComposition = new HashSet<InstrumentType>();
        return this;
    }

    protected override bool ShouldNotDestroyOnLoad()
    {
        return false;
    }

    private void Start()
    {
        placer = TowerPlacer.Instance;
        placer.OnTowerPlaced += UpdateBandLevel;
    }

    private void OnDestroy()
    {
        placer.OnTowerPlaced -= UpdateBandLevel;
    }

    private void UpdateBandLevel(InstrumentType type, BeatMappedShooter ignored)
    {
        if (!bandComposition.Contains(type))
        {
            bandComposition.Add(type);
            if (BandLevel > 1)
            {
                notificationSystem.PublishNotification("Band Up!");
            }
        }
    }
}
