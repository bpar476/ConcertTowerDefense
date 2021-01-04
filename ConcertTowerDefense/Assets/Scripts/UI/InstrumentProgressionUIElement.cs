using UnityEngine;
using UnityEngine.UI;

public class InstrumentProgressionUIElement : MonoBehaviour
{

    private static readonly string LEVEL_TEXT_PREFIX = "LVL:";

    [SerializeField]
    private RawImage preview;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private InstrumentArchetype archetype;

    private TowerPlacer towerPlacer;
    private TowerProgression towerProgression;


    private void Start()
    {
        towerPlacer = TowerPlacer.Instance;
        towerPlacer.OnTowerPlaced += UnlockInstrument;


        towerProgression = TowerProgression.Instance;
        towerProgression.OnTowerLevelUp += LevelUpTowerUi;
    }

    private void UnlockInstrument(InstrumentType type, BeatMappedShooter ignored)
    {
        if (type == archetype.type)
        {
            Color originalColor = preview.color;
            preview.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
        }
    }

    private void LevelUpTowerUi(InstrumentType type, int level)
    {
        if (type == archetype.type)
        {
            levelText.text = string.Format("{0} {1}{2}", archetype.friendlyName, LEVEL_TEXT_PREFIX, level);
        }
    }
}
