using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] protected UIButtons buttons;
    [SerializeField] protected UIGameObjects objs;
    public virtual void SetActive(bool flag)
    {
        gameObject.SetActive(flag);
    }

    protected virtual void AddButtonEvnet()
    {
        
    }

    protected virtual void RemoveButtonEvent()
    {
        
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