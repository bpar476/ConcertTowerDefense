using UnityEngine;

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

    protected override BeatSynchroniser Init()
    {
        secondsPerBeat = 60f / bpm;
        audioStartTime = (float)AudioSettings.dspTime;
        return this;
    }

    //FIXME: I really shouldn't need to be restarted. Instead, I shouldn't start until there are towers that care about the beat
    /// <summary>
    /// Restarts the beat synchronisation
    /// </summary>
    public void Restart()
    {
        audioStartTime = (float)AudioSettings.dspTime;
        completedLoopThisUpdate = false;
        numCompletedLoops = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ResetLoopCompletion();

        UpdateProgress();

        UpdateLoops();
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
