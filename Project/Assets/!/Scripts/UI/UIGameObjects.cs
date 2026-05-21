using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;

public class UIGameObjects : MonoBehaviour
{
    [SerializeField] private GameObject basicObject;
    [SerializeField] private GameObject resultObject;

    [SerializeField] private Transform basicTransform;
    [SerializeField] private Transform resultTransform;
    [SerializeField] private Transform unusedTransform;

    [SerializeField] private GridLayoutGroup basicGrid;
    [SerializeField] private GridLayoutGroup resultGrid;
    private readonly Vector2 CellSize2 = new Vector2(375, 0);
    private readonly Vector2 CellSize3 = new Vector2(250, 0);

    [SerializeField] private SerializedDictionary<UIButtonType, Transform> buttons;

    void Start()
    {
        HideAllButtons();
    }

    public void SetGridCellSize(int size, bool isBasic = true)
    {
        if(isBasic == true)
        {
            basicGrid.cellSize = size == 2 ? CellSize2 : CellSize3;
        }
        else
        {
            resultGrid.cellSize = size == 2 ? CellSize2 : CellSize3;
        }
    }

    public void ShowUI(bool isBasic = true)
    {
        if(isBasic == true) basicObject.SetActive(true);
        else resultObject.SetActive(true);
    }

    public void HideUI(bool isBasic = true)
    {
        if(isBasic == true) basicObject.SetActive(false);
        else resultObject.SetActive(false);
    }

    public void ShowButton(UIButtonType type, bool isBasic = true)
    {
        if(isBasic == true)
        {
            buttons[type].SetParent(basicTransform);
        }
        else
        {
            buttons[type].SetParent(resultTransform);
        }
    }

    public void HideButton(UIButtonType type)
    {
        buttons[type].SetParent(unusedTransform);
    }

    private void HideAllButtons()
    {
        foreach(var v in buttons.Values)
        {
            v.SetParent(unusedTransform);
        }
    }
}
