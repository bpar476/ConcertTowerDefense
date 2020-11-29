using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Beat : MonoBehaviour
{
    private AudioSource audioSource;

    private bool onBeat = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        BeatManager.Instance.OnEigth += BeatOnQuarter;
    }

    private void OnDestroy()
    {
        BeatManager.Instance.OnEigth -= BeatOnQuarter;
    }

    private void BeatOnQuarter()
    {
        if (onBeat)
        {
            audioSource.Play();
        }
        onBeat = !onBeat;
    }
}
