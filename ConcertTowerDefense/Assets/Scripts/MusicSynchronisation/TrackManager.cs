using System;
using UnityEngine;

public class TrackManager : MonoBehaviour
{

    [SerializeField]
    private MusicTrack track;

    private float[] beatMap;

    private BeatSynchroniser conductor;

    private int currentBeatIndex = 0;

    private bool waitingForNextLoop = false;

    public Action OnBeat;

    private void Start()
    {
        conductor = BeatSynchroniser.Instance;
        beatMap = track.BeatMap;
    }

    private void Update()
    {
        if (conductor.CompletedLoop)
        {
            waitingForNextLoop = false;
        }

        if (conductor.LoopBeatProgress >= beatMap[currentBeatIndex] && !waitingForNextLoop)
        {
            OnBeat?.Invoke();
            currentBeatIndex++;

            if (currentBeatIndex == beatMap.Length)
            {
                waitingForNextLoop = true;
                currentBeatIndex = 0;
            }
        }
    }

}
