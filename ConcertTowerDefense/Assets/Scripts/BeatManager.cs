using System;
using UnityEngine;

public class BeatManager : Singleton<BeatManager>
{

    /// <summary>
    /// Beats per minute
    /// </summary>
    [SerializeField]
    [Range(60, 150)]
    private int bpm;

    /// <summary>
    /// Action which will be invoked every eigth note in the beat.
    /// </summary>
    public Action OnEigth;

    private float period;

    // FIXME: I will be wrong after a scene change. Either reload me every scene or reset the last beat when the scene loads.
    private float lastBeat = 0;

    protected override BeatManager Init()
    {
        // TODO Should period be recalculated every frame so BPM can be changed on the fly?
        period = 1 / (bpm / 60f * 2f);

        Debug.Log("period is");
        Debug.Log(period);
        return this;
    }

    private void Update()
    {
        Debug.Log("Update at " + Time.time);
        if (HasEigthNotePassed())
        {
            Beat();

            UpdateLastBeat();
        }
    }

    private bool HasEigthNotePassed()
    {
        return Time.time - lastBeat > period;
    }

    private void Beat()
    {
        Debug.Log("Beating eigth note at " + Time.time);

        OnEigth?.Invoke();
    }

    private void UpdateLastBeat()
    {
        lastBeat = Time.time;
    }
}
