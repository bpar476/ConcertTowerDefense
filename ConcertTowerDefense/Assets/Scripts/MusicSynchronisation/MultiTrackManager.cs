using System.Collections.Generic;
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

    private Dictionary<InstrumentType, TrackPlayer> players;

    private void Awake()
    {
        players = new Dictionary<InstrumentType, TrackPlayer>();
    }

    private void Start()
    {
        placer.OnTowerPlaced += LoadInstrumentBeatmapper;
    }

    private void LoadInstrumentBeatmapper(InstrumentType type, BeatMappedShooter shooter)
    {
        var archetype = archetypes.Where(a => a.type == type).First();
        if (!players.ContainsKey(type))
        {
            if (players.Count == 0)
            {
                synchroniser.Restart();
            }

            var player = gameObject.AddComponent<TrackPlayer>();
            player.LoadTracks(archetype.trackLevels.ToArray());
            players[type] = player;
        }
        shooter.RegisterOnBeatCallback(players[type].Mapper);
    }

    private void OnDestroy()
    {
        placer.OnTowerPlaced -= LoadInstrumentBeatmapper;
    }
}
