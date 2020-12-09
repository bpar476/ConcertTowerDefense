﻿using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    /// <summary>
    /// Color to tint the tower's sprite when it can be placed in its current position
    /// </summary>
    [SerializeField]
    private Color legalPlacementTint;
    /// <summary>
    /// Color to tint the tower's sprite when it cannot be placed in its current position
    /// </summary>
    [SerializeField]
    private Color illegalPlacementTint;

    /// <summary>
    /// The stage that the towers can be placed on
    /// </summary>
    [SerializeField]
    private StageLayout stage;

    private Camera mainCam;

    private InstrumentArchetype currentTowerArchetype;
    private GameObject currentTower;
    private SpriteRenderer currentTowerRenderer;
    private Color originalColor;

    private Mode state;
    private bool hoveringValidCell = false;
    private StageCell currentCell;

    private void Awake()
    {
        mainCam = Camera.main;
        state = Mode.EMPTY;
    }

    private void Update()
    {
        if (state == Mode.HOLDING)
        {
            var currentPos = MousePositionToGameWorldPosition();
            currentTower.transform.position = currentPos;

            foreach (var cell in stage.Layout)
            {
                if (currentPos.x >= cell.BottomLeft.x && currentPos.x <= cell.TopRight.x && currentPos.y >= cell.BottomLeft.y && currentPos.y <= cell.TopRight.y)
                {
                    if (currentCell != null)
                    {
                        currentCell.UnHighlight();
                    }

                    currentTowerRenderer.color = originalColor * legalPlacementTint;
                    currentCell = cell;
                    hoveringValidCell = true;
                    break;
                }

                hoveringValidCell = false;
            }

            if (hoveringValidCell)
            {
                currentCell.Highlight(Color.yellow);

                // TODO look up cost of tower depending on tower type Magic number 5 should be replaced with value from tower type
                if (Input.GetMouseButtonDown(0) && TowerCurrency.Instance.Currency >= 5)
                {
                    currentTower.transform.position = currentCell.Center;
                    currentTowerRenderer.color = originalColor;
                    currentCell.UnHighlight();
                    TowerCurrency.Instance.ConsumeCurrency(currentTowerArchetype.cost);

                    // TODO fire event for listeners that tower of type has been created
                    state = Mode.EMPTY;
                    currentCell = null;
                    currentTower = null;
                    currentTowerRenderer = null;
                    currentTowerArchetype = null;
                }
                else
                {
                    currentTowerRenderer.color = originalColor * legalPlacementTint;
                }
            }
            else
            {
                currentTowerRenderer.color = originalColor * illegalPlacementTint;
            }
        }
    }

    public void GrabTower(InstrumentArchetype tower)
    {
        if (state == Mode.EMPTY && TowerCurrency.Instance.Currency >= tower.cost)
        {
            currentTowerArchetype = tower;
            currentTower = Instantiate(currentTowerArchetype.towerPrefab.gameObject, MousePositionToGameWorldPosition(), Quaternion.identity);
            currentTowerRenderer = currentTower.GetComponent<SpriteRenderer>();
            originalColor = currentTowerRenderer.color;
            currentTowerRenderer.color = originalColor * illegalPlacementTint;
            state = Mode.HOLDING;
        }
    }

    private Vector3 MousePositionToGameWorldPosition()
    {
        var point = mainCam.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(point.x, point.y, 0);
    }

    private enum Mode
    {
        EMPTY, HOLDING
    }
}
