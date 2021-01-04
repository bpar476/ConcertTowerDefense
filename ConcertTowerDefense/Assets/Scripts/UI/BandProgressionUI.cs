using UnityEngine;
using UnityEngine.UI;

public class BandProgressionUI : MonoBehaviour
{
    [SerializeField]
    private Text bandLevelText;

    private BandProgression bandProgression;

    void Start()
    {
        bandProgression = BandProgression.Instance;
        bandLevelText.text = "";
    }

    // FIXME: I shouldn't run update, there should be a band progression event
    private void Update()
    {
        var level = bandProgression.BandLevel;
        var levelText = "";
        if (level != 0)
        {
            levelText = string.Format("Band Level: {0}\n ({1}x Bonus Damage)", level, GhostHealth.DamageMultiplierForBandLevel(level));
        }
        bandLevelText.text = levelText;
    }

}