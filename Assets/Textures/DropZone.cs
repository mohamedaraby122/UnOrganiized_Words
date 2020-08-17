using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropZone : MonoBehaviour,IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        print("Drop");
        if (eventData.pointerDrag == null)
        {
            return;
        }
        DragDrop drag = eventData.pointerDrag.GetComponent<DragDrop>();
        if (drag!=null){
            if (drag.parentToReturnTo.gameObject.tag!="MainBoard")
            {
                 GameObject t = Instantiate(drag.gameObject, drag.parentToReturnTo.transform);

            }
            
                int levl = drag.gameObject.GetComponent<WordNumber>().number;
                GamManager.Instance.SetCurrentLevel(levl);
            
            print(drag.gameObject.GetComponent<WordNumber>().number);

            drag.parentToReturnTo = this.transform;
            drag.gameObject.SetActive(false);
        }


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Pointer Enter");
        if (eventData.pointerDrag == null)
        {
            return;
        }
        DragDrop drag = eventData.pointerDrag.GetComponent<DragDrop>();
        if (drag != null)
        {
            drag.placeHolderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("Pointer Exit");
        if (eventData.pointerDrag == null)
        {
            return;
        }
        DragDrop drag = eventData.pointerDrag.GetComponent<DragDrop>();
        if (drag != null)
        {
            drag.placeHolderParent = drag.parentToReturnTo;
        }

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
