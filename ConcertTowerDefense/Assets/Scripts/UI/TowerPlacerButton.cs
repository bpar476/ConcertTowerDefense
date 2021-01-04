using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class TowerPlacerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// The kind of tower that this button will spawn
    /// </summary>
    [SerializeField]
    private InstrumentArchetype archetype;
    /// <summary>
    /// Game object showing the tooltip for the tower this button places.
    /// </summary>
    [SerializeField]
    private GameObject tooltip;

    private Button button;
    private RawImage towerImage;
    private Text costText;

    private void Awake()
    {
        InitialiseComponents();
    }

    private void Start()
    {
        SetImageToTowerSprite();

        SetCostTextToTowerCost();

        SetButtonClickEvent();

        SetTooltipText();

        tooltip.SetActive(false);
    }

    private void InitialiseComponents()
    {
        button = GetComponent<Button>();
        towerImage = GetComponentInChildren<RawImage>();
        costText = GetComponentInChildren<Text>();
    }

    private void SetImageToTowerSprite()
    {
        towerImage.texture = archetype.towerPrefab.GetComponent<SpriteRenderer>().sprite.texture;
    }

    private void SetCostTextToTowerCost()
    {
        costText.text = archetype.cost.ToString();
    }

    private void SetButtonClickEvent()
    {
        button.onClick.AddListener(() => TowerPlacer.Instance.GrabTower(archetype));
    }

    private void SetTooltipText()
    {
        Text tooltipText = tooltip.GetComponentInChildren<Text>();
        tooltipText.text = archetype.description;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}
