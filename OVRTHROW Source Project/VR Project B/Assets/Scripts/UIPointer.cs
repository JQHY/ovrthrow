using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIPointer : MonoBehaviour
{

    public bool Pointing;
    public Transform pointT;
    LineRenderer lrend;
    public Canvas cnv;
    public Button activeBtn;
    public Transform UIRoot;

    public Transform interactPoint;

    Button button;

    Text text;

    // var et = FindObjectOfType<EventTrigger>();
    // key down input has been moved inside raycast loop
    // because you should only be able to click when you are selecting something


    // Start is called before the first frame update
    void Start()
    {
        lrend = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Pointing)
        {
            Ray r = new Ray(pointT.position, pointT.forward);
            RaycastHit hitinfo = new RaycastHit();
            lrend.SetPosition(0, pointT.position);
            if (Physics.Raycast(r, out hitinfo, 50))
            {
                lrend.SetPosition(1, hitinfo.point);
                interactPoint.position = hitinfo.point;

                button = GameObject.FindWithTag(hitinfo.collider.tag).GetComponent<Button>();

                button.Select();

                Vector2 hitpoint = new Vector2(hitinfo.point.x, hitinfo.point.y);
                hitpoint.x = hitpoint.x / hitinfo.collider.bounds.size.x;
                hitpoint.y = hitpoint.y / hitinfo.collider.bounds.size.y;
                hitpoint.x *= cnv.pixelRect.width;//UIDims.x;//Screen.currentResolution.width;
                hitpoint.y *= cnv.pixelRect.height;//UIDims.y;//Screen.currentResolution.height;

                if (Input.GetKeyDown(KeyCode.H))
                {
                    if (button)
                    {
                    button.onClick.Invoke();
                    }
                }
            }
            else
            {
                lrend.SetPosition(1, pointT.position + pointT.forward * 40);
                button = null;
                EventSystem.current.SetSelectedGameObject(null);
            }


        }
    }

    public void ClickButton()
    {
        if (button)
        {
            button.onClick.Invoke();
        }
    }
}
