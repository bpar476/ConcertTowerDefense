﻿using UnityEngine;

public class BeatSynchroniser : Singleton<BeatSynchroniser>
{

    /// <summary>
    /// The progress in beats through this loop. Resets after completing a loop
    /// </summary>
    public float LoopBeatProgress { get { return loopProgressBeats + 1; } }

    /// <summary>
    /// True on the frame that a loop has completed
    /// </summary>
    public bool CompletedLoop { get { return completedLoopThisUpdate; } }

    /// <summary>
    /// The progress through the loop in seconds
    /// </summary>
    public float LoopProgress
    {
        get
        {
            return loopProgressBeats * secondsPerBeat;
        }
    }

    /// <summary>
    /// Beats per minute
    /// </summary>
    [SerializeField]
    [Range(60, 150)]
    private int bpm;

    /// <summary>
    /// Number of beats in this loop.
    /// </summary>
    [SerializeField]
    private int beatsPerLoop;
    private float secondsPerBeat;
    private int numCompletedLoops;
    private float audioStartTime;
    private float loopProgressBeats;
    private float totalProgress;
    private float totalProgressBeats;
    private bool completedLoopThisUpdate = false;

    private bool started = false;

    protected override BeatSynchroniser Init()
    {
        secondsPerBeat = 60f / bpm;
        return this;
    }

    /// <summary>
    /// Starts the beat synchronisation
    /// </summary>
    public void StartSynchronisation()
    {
        started = true;
        audioStartTime = (float)AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            ResetLoopCompletion();

            UpdateProgress();

            UpdateLoops();
        }
    }

    private void ResetLoopCompletion()
    {
        if (completedLoopThisUpdate)
        {
            completedLoopThisUpdate = false;
        }
    }

    private void UpdateProgress()
    {
        totalProgress = (float)AudioSettings.dspTime - audioStartTime;
        totalProgressBeats = totalProgress / secondsPerBeat;
        loopProgressBeats = totalProgressBeats - numCompletedLoops * beatsPerLoop;
    }

    private void UpdateLoops()
    {
        if (totalProgressBeats > beatsPerLoop + beatsPerLoop * numCompletedLoops)
        {
            numCompletedLoops++;
            completedLoopThisUpdate = true;
        }
    }

    protected override bool ShouldNotDestroyOnLoad()
    {
        return false;
    }
}
