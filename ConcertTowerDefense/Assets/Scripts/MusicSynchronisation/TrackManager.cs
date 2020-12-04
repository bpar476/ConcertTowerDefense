using System;
using UnityEngine;

public class TrackManager : MonoBehaviour
{

    private float[] beatMap;

    private BeatSynchroniser conductor;

    private int currentBeatIndex = 0;

    private bool waitingForNextLoop = false;

    public Action OnBeat;

    private void Start()
    {
        conductor = BeatSynchroniser.Instance;
    }

    /// <summary>
    /// Smoothly transitions to a new beatmap
    /// </summary>
    /// <param name="newBeatMap">The new beatmap</param>
    public void ChangeBeatMap(float[] newBeatMap)
    {
        beatMap = newBeatMap;
        if (conductor == null)
        {
            // This case only happens when called from Start() of another component that is Started before this component
            currentBeatIndex = 0;
        }
        else
        {
            currentBeatIndex = Array.FindIndex(beatMap, beat => beat >= conductor.LoopBeatProgress);

            currentBeatIndex = Mathf.Max(Array.FindIndex(beatMap, beat => beat >= conductor.LoopBeatProgress), 0);
        }
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
