using UnityEngine;
using System.Collections;

public class TrackPlayer : MonoBehaviour
{
    [SerializeField]
    private MusicTrack[] bassTracks;

    private AudioSource[] bassSources;
    private int currentTrackIndex = 0;

    private void Awake()
    {
        bassSources = new AudioSource[bassTracks.Length];

        for (var i = 0; i < bassTracks.Length; i++)
        {
            var track = bassTracks[i];
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = track.Track;
            source.loop = true;
            bassSources[i] = source;
            if (i == 0)
            {
                source.volume = 1;
            }
            else
            {
                source.volume = 0;
            }
            source.Play();
        }
    }

    public void CrossFadeToTrackLevel(int level)
    {
        if (level > bassTracks.Length)
        {
            Debug.Log(string.Format("No track level for %s", level));
            return;
        }

        var newTrackIndex = level - 1;
        StartCoroutine(CrossFadeTracks(currentTrackIndex, newTrackIndex));
        currentTrackIndex = newTrackIndex;
    }

    private IEnumerator CrossFadeTracks(int outIndex, int inIndex)
    {
        var outSource = bassSources[outIndex];
        var inSource = bassSources[inIndex];
        var originVolume = outSource.volume;
        for (var i = 0f; i < 1; i += (Time.deltaTime / 0.1f))
        {
            outSource.volume = Mathf.Lerp(originVolume, 0, i);
            inSource.volume = Mathf.Lerp(0, originVolume, i);

            yield return new WaitForEndOfFrame();
        }
    }
}
