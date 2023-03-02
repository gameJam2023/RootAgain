using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggableitem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public Image item;

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("Begin Drag");
        item.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

    }

    public void OnDrag(PointerEventData eventData)
    {
        print("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("End drag");
        transform.SetParent(parentAfterDrag);
        item.raycastTarget = true;
    }

}

