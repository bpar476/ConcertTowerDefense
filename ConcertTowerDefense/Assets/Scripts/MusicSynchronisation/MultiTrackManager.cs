using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class MultiTrackManager : MonoBehaviour
{
    [SerializeField]
    private TowerPlacer placer;

    [SerializeField]
    private BeatSynchroniser synchroniser;

    [SerializeField]
    private List<InstrumentArchetype> archetypes;

    [SerializeField]
    private TowerProgression towerProgression;

    private Dictionary<InstrumentType, TrackPlayer> players;

    private void Awake()
    {
        players = new Dictionary<InstrumentType, TrackPlayer>();
        placer.OnTowerPlaced += LoadInstrumentBeatmapperAtEndOfUpdate;
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
                synchroniser.Start();
            }

            var player = gameObject.AddComponent<TrackPlayer>();
            player.LoadTracks(archetype.trackLevels.ToArray(), towerProgression.GetLevelForInstrument(type));
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
