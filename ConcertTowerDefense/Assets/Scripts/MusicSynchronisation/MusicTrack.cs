using UnityEngine;

// TODO: Make it so that the track is played from this as well.
[CreateAssetMenu(fileName = "Track", menuName = "Track and Beatmap", order = 51)]
public class MusicTrack : ScriptableObject
{
    /// <summary>
    /// A looping track
    /// </summary>
    [SerializeField]
    private AudioClip track;

    /// <summary>
    /// Beatmap for the track. Should be filled with values
    /// denoting which beat a notes is played in the track.
    /// For example, a beatmap with a note on the first beat of
    /// every bar in a 4 bar sequence would be:
    /// 1 5 9 13
    /// </summary>
    [SerializeField]
    private float[] beatMap;

    /// <summary>
    /// Get an enumerator for the beatmap which iterates over the
    /// note positions for this track
    /// </summary>
    public float[] BeatMap { get { return (float[])beatMap.Clone(); } }

    public AudioClip Track { get { return track; } }
}
