using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CycleThroughMusicTracks : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] tracks;

    private int currentTrackIndex = 0;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = tracks[currentTrackIndex];
        source.Play();
        source.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            currentTrackIndex++;
            if (currentTrackIndex >= tracks.Length)
            {
                currentTrackIndex = 0;
            }
            source.clip = tracks[currentTrackIndex];
            source.Play();
        }
    }
}
