using UnityEngine;
using UnityEngine.EventSystems;

public class InfoBoxHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoBox; // Assign in Inspector
    private bool isHoveringButton = false;
    private bool isHoveringBox = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHoveringButton = true;
        infoBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHoveringButton = false;
        CheckHideInfoBox();
    }

    // Attach this to the InfoBox as well
    public void OnInfoBoxEnter()
    {
        isHoveringBox = true;
    }

    public void OnInfoBoxExit()
    {
        isHoveringBox = false;
        CheckHideInfoBox();
    }

    private void CheckHideInfoBox()
    {
        if (!isHoveringButton && !isHoveringBox)
        {
            infoBox.SetActive(false);
        }
    }
}
