using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR;

public class RightTrigger : MonoBehaviour
{
    public SteamVR_Action_Boolean trigger;
    public SteamVR_Input_Sources hand;
    public LineRenderer lr;

    public float kAdjust;

    void Start()
    {
        
    }


    void Update()
    {
        /*// 트리거를 당기면
         if (trigger.GetState(hand))
         {
            // 선을 그리고 싶다.
            lr.enabled = true;
            lr.startWidth = kAdjust;
            lr.endWidth = kAdjust;
            // Ray를 이용해서 바라보고
            Ray ray = new Ray(transform.position, transform.forward);
             lr.SetPosition(0, ray.origin);
             RaycastHit hitInfo;
             if (Physics.Raycast(ray, out hitInfo))
             {
                 lr.SetPosition(1, hitInfo.point);
             }
         }
         else
         {
             // 트리거를 놓으면 선을 그리지 않고싶다.
             lr.enabled = false;
         }*/

        // 선을 그리고 싶다.
        lr.enabled = true;
        lr.startWidth = kAdjust;
        lr.endWidth = kAdjust;

        // Ray를 이용해서 바라보고
        Ray ray = new Ray(transform.position, transform.forward);
        lr.SetPosition(0, ray.origin);
        lr.SetPosition(1, ray.origin + ray.direction * 100f);
     
        if (trigger.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            RaycastHit[] hits = Physics.RaycastAll(ray);
            for (int i = 0; i < hits.Length; i++)
            {
                Button btn = hits[i].transform.GetComponent<Button>();
                if (btn)
                {
                    btn.onClick.Invoke();
                    break;
                }
            }
        }

    }
}
