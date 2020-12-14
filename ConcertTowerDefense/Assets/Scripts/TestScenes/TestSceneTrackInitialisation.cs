using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneTrackInitialisation : MonoBehaviour
{
    [SerializeField]
    private TrackPlayer player;

    [SerializeField]
    private MusicTrack[] tracks;

    [SerializeField]
    private BeatMappedShooter tower;

    private void Start()
    {
        player.LoadTracks(tracks, 1, 0);
        tower.RegisterOnBeatCallback(player.Mapper);
    }
}
