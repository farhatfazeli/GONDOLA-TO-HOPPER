using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoBoxHoverManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoBox;
    
    private IInfoBoxHoverHandler _infoBoxHoverHandler;

    private void Awake()
    {
        _infoBoxHoverHandler = infoBox.GetComponent<IInfoBoxHoverHandler>();
        if (_infoBoxHoverHandler == null)
        {
            throw new Exception("InfoBoxHoverManager: infoBox does not have a component that implements IInfoBoxHover");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _infoBoxHoverHandler.Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _infoBoxHoverHandler.Hide();
    }
}
