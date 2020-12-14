using UnityEngine;
using System.Collections;

public class TrackPlayer : MonoBehaviour
{
    public BeatMapper Mapper { get { return mapper; } }

    private int currentLevel;

    private MusicTrack[] tracks;

    private BeatMapper mapper;

    private AudioSource[] sources;
    private int currentTrackIndex = 0;

    public void LoadTracks(MusicTrack[] tracks, int currentLevel, float progress)
    {
        this.currentLevel = currentLevel;
        this.tracks = tracks;
        this.mapper = gameObject.AddComponent<BeatMapper>();
        mapper.ChangeBeatMap(tracks[currentLevel - 1].BeatMap);

        InitialiseTracks(progress);
    }

    public void CrossFadeToTrackLevel(int level)
    {
        if (level > tracks.Length)
        {
            Debug.Log(string.Format("No track level for %s", level));
            return;
        }

        var newTrackIndex = level - 1;
        StartCoroutine(CrossFadeTracks(currentTrackIndex, newTrackIndex));
        currentTrackIndex = newTrackIndex;
        mapper.ChangeBeatMap(tracks[currentTrackIndex].BeatMap);
    }

    private void InitialiseTracks(float progress)
    {
        sources = new AudioSource[tracks.Length];

        for (var i = 0; i < tracks.Length; i++)
        {
            var track = tracks[i];
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = track.Track;
            source.loop = true;
            sources[i] = source;
            source.volume = 0;
            source.time = progress;
            if (i == currentLevel - 1)
            {
                source.volume = 1;
            }
            source.Play();
        }
    }

    private IEnumerator CrossFadeTracks(int outIndex, int inIndex)
    {
        var outSource = sources[outIndex];
        var inSource = sources[inIndex];
        var originVolume = outSource.volume;
        for (var i = 0f; i < 1; i += (Time.deltaTime / 0.1f))
        {
            outSource.volume = Mathf.Lerp(originVolume, 0, i);
            inSource.volume = Mathf.Lerp(0, originVolume, i);

            yield return new WaitForEndOfFrame();
        }
        outSource.volume = 0;
        inSource.volume = originVolume;
    }
}
