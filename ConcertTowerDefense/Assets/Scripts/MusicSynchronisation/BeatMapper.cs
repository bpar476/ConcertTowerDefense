using System;
using UnityEngine;

public class BeatMapper : MonoBehaviour
{

    private float[] beatMap;

    private BeatSynchroniser conductor;

    private int currentBeatIndex = 0;

    private bool outOfNotes = false;

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

            currentBeatIndex = Mathf.Max(Array.FindIndex(beatMap, beat => beat >= conductor.LoopBeatProgress), 0);
        }
        outOfNotes = false;
    }

    private void Update()
    {
        if (conductor.LoopBeatProgress > beatMap[beatMap.Length - 1] && currentBeatIndex == 0)
        {
            outOfNotes = true;
        }

        if (conductor.CompletedLoop)
        {
            outOfNotes = false;
        }

        if (conductor.LoopBeatProgress >= beatMap[currentBeatIndex] && !outOfNotes)
        {
            OnBeat?.Invoke();
            currentBeatIndex++;

            if (currentBeatIndex == beatMap.Length)
            {
                outOfNotes = true;
                currentBeatIndex = 0;
            }
        }
    }

}
