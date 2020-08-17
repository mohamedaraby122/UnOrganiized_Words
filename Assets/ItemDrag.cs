using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour,IDragHandler,IEndDragHandler
{
    public Transform lastParent;
    GameObject obj = null;
    public void OnDrag(PointerEventData eventData)
    {
        obj = new GameObject();
        obj=this.gameObject;
        obj.transform.parent = lastParent;
        obj.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        obj = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
