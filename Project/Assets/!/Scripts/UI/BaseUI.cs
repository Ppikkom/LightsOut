using TMPro;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] protected UIButtons buttons;
    [SerializeField] protected UIGameObjects objs;
    [SerializeField] protected TextMeshProUGUI textMeshProUGUI;
    protected const string TableName = "Table";
    public virtual void SetActive(bool flag)
    {
        gameObject.SetActive(flag);
    }

    protected virtual void AddButtonEvent() { }

    protected virtual void RemoveButtonEvent() { }

    protected virtual void EditText(string s)
    {
        textMeshProUGUI.text = s;
    }

    public virtual void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public virtual void HideUI()
    {
        gameObject.SetActive(false);
    }
}