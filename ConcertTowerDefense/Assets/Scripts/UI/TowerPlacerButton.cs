using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TowerPlacerButton : MonoBehaviour
{
    /// <summary>
    /// The kind of tower that this button will spawn
    /// </summary>
    [SerializeField]
    private InstrumentArchetype archetype;

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

}
