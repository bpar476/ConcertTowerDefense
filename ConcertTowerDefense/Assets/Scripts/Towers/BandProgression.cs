﻿using System.Collections.Generic;
using UnityEngine;

public class BandProgression : MonoBehaviour
{

    public int BandLevel
    {
        get
        {
            return bandComposition.Count;
        }
    }

    [SerializeField]
    private TowerPlacer placer;

    private HashSet<InstrumentType> bandComposition;

    private void Awake()
    {
        bandComposition = new HashSet<InstrumentType>();
    }

    private void Start()
    {
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
        }
    }
}