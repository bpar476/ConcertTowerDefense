using UnityEngine;

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
    private GameObject currentTower;
    private Color originalColor;

    private Mode state;
    private bool hoveringValidCell = false;
    private StageLayout.Cell currentCell;

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
                if (currentPos.x >= cell.bottomLeft.x && currentPos.x <= cell.topRight.x && currentPos.y >= cell.bottomLeft.y && currentPos.y <= cell.topRight.y)
                {
                    currentTower.GetComponent<SpriteRenderer>().color = originalColor * legalPlacementTint;
                    currentCell = cell;
                    hoveringValidCell = true;
                    break;
                }

                hoveringValidCell = false;
            }

            if (hoveringValidCell)
            {
                // TODO cache renderer
                currentTower.GetComponent<SpriteRenderer>().color = originalColor * legalPlacementTint;
            }
            else
            {
                currentTower.GetComponent<SpriteRenderer>().color = originalColor * illegalPlacementTint;

            }
        }
    }

    public void GrabTower(GameObject tower)
    {
        if (state == Mode.EMPTY)
        {
            currentTower = Instantiate(tower, MousePositionToGameWorldPosition(), Quaternion.identity);
            var renderer = currentTower.GetComponent<SpriteRenderer>();
            originalColor = renderer.color;
            renderer.color = originalColor * illegalPlacementTint;
            state = Mode.HOLDING;
        }
    }

    public void DropTower()
    {
        if (state == Mode.HOLDING)
        {

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
