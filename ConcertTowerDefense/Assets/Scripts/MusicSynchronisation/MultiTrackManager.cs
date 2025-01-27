﻿using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class MultiTrackManager : MonoBehaviour
{

    [SerializeField]
    private BeatSynchroniser synchroniser;

    [SerializeField]
    private List<InstrumentArchetype> archetypes;

    private TowerProgression towerProgression;
    private TowerPlacer placer;

    private Dictionary<InstrumentType, TrackPlayer> players;

    private void Awake()
    {
        players = new Dictionary<InstrumentType, TrackPlayer>();
    }

    private void Start()
    {
        placer = TowerPlacer.Instance;
        placer.OnTowerPlaced += LoadInstrumentBeatmapperAtEndOfUpdate;
        towerProgression = TowerProgression.Instance;
        towerProgression.OnTowerLevelUp += LevelUpTower;
    }

    // We run this in a coroutine so that other listeners to the event settle before we load the track (band level, tower progression)
    private void LoadInstrumentBeatmapperAtEndOfUpdate(InstrumentType type, BeatMappedShooter shooter)
    {
        StartCoroutine(LoadInstrumentBeatmapper(type, shooter));
    }

    private IEnumerator LoadInstrumentBeatmapper(InstrumentType type, BeatMappedShooter shooter)
    {
        yield return new WaitForEndOfFrame();
        var archetype = archetypes.Where(a => a.type == type).First();
        if (!players.ContainsKey(type))
        {
            if (players.Count == 0)
            {
                synchroniser.StartSynchronisation();
            }

            var player = gameObject.AddComponent<TrackPlayer>();
            player.LoadTracks(archetype.trackLevels.ToArray(), towerProgression.GetLevelForInstrument(type), synchroniser.LoopProgress);
            players[type] = player;
        }
        shooter.RegisterOnBeatCallback(players[type].Mapper);
    }

    private void LevelUpTower(InstrumentType type, int level)
    {
        StartCoroutine(LevelUpTowerInNextFrame(type, level));
    }

    private IEnumerator LevelUpTowerInNextFrame(InstrumentType type, int level)
    {
        yield return new WaitForFixedUpdate();
        players[type].CrossFadeToTrackLevel(level);
    }

    private void OnDestroy()
    {
        placer.OnTowerPlaced -= LoadInstrumentBeatmapperAtEndOfUpdate;
        towerProgression.OnTowerLevelUp -= LevelUpTower;
    }
}
