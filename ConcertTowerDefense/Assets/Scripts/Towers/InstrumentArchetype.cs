using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Instrument Tower", order = 51)]
public class InstrumentArchetype : ScriptableObject
{
    public InstrumentType type;

    public BeatMappedShooter towerPrefab;

    /// <summary>
    /// The tracks for each level of the tower. There should be
    /// exactly 3.
    /// </summary>
    public List<MusicTrack> trackLevels;

    /// <summary>
    /// The price to place this tower
    /// </summary>
    public int cost;

    /// <summary>
    /// A description of the tower archetype which will be rendered on the tooltip
    /// </summary>
    [TextArea]
    public string description;
}
