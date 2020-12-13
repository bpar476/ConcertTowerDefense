﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerProgression : MonoBehaviour
{
    /// <summary>
    /// Callback function invoked when a tower type levels up. Arguments
    /// are the tower type, and the level.
    /// </summary>
    public Action<InstrumentType, int> OnTowerLevelUp;

    [SerializeField]
    private TowerPlacer placer;

    private Dictionary<InstrumentType, int> towerCounts;

    private Dictionary<InstrumentType, int> towerLevels;

    public int GetLevelForInstrument(InstrumentType type)
    {
        if (towerLevels.ContainsKey(type))
        {
            return towerLevels[type];
        }
        return 0;
    }

    private void Awake()
    {
        placer.OnTowerPlaced += HandleTowerPlaced;
        towerCounts = new Dictionary<InstrumentType, int>();
        towerLevels = new Dictionary<InstrumentType, int>();
    }

    private void HandleTowerPlaced(InstrumentType type, BeatMappedShooter ignored)
    {
        IncrementTowerTypeCount(type);

        UpdateTowerLevel(type);
    }

    private void IncrementTowerTypeCount(InstrumentType type)
    {
        if (towerCounts.ContainsKey(type))
        {
            towerCounts[type] += 1;
        }
        else
        {
            towerCounts[type] = 1;
        }
    }

    private void UpdateTowerLevel(InstrumentType type)
    {
        int newTowerLevel = towerCountToLevel(towerCounts[type]);
        if (!towerLevels.ContainsKey(type) || towerLevels[type] < newTowerLevel)
        {
            towerLevels[type] = newTowerLevel;
            OnTowerLevelUp?.Invoke(type, newTowerLevel);
        }
    }

    private static int towerCountToLevel(int towerCount)
    {
        if (towerCount >= 5)
        {
            return 3;
        }
        if (towerCount >= 3)
        {
            return 2;
        }
        if (towerCount >= 1)
        {
            return 1;
        }
        return 0;
    }

    private void OnDestroy()
    {
        placer.OnTowerPlaced -= HandleTowerPlaced;
    }
}