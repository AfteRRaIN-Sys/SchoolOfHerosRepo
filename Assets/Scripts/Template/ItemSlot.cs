using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            GameObject dropped = eventData.pointerDrag;
            DragDrop item = dropped.GetComponent<DragDrop>();

            //Debug.Log(item.GetType().ToString() + " Dropped.");
            item.parentAfterDrag = transform;
        }
    }
}
