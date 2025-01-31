using UnityEngine;
using UnityEngine.EventSystems;

public class StationInfoBoxHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public StationInfoBoxManager stationInfoBoxManager; // Assign in Inspector
    // private bool _isHoveringButton = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // _isHoveringButton = true;
        stationInfoBoxManager.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // _isHoveringButton = false;
        stationInfoBoxManager.gameObject.SetActive(false);
        //CheckHideInfoBox();
    }

    // // Attach this to the InfoBox as well
    // public void OnInfoBoxEnter()
    // {
    //     _isHoveringBox = true;
    // }
    //
    // public void OnInfoBoxExit()
    // {
    //     _isHoveringBox = false;
    //     CheckHideInfoBox();
    // }

    // private void CheckHideInfoBox()
    // {
    //     if (!_isHoveringButton && !_isHoveringBox)
    //     {
    //         stationInfoBoxManager.gameObject.SetActive(false);
    //     }
    // }
}
