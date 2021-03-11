using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


// Player 컨트롤러의 Grip 버튼을 누르면 반경 1M안에 있는 충돌체를 검사후 Grabbable 컴포넌트가 있는 Item을 잡고싶다
public class Grip : MonoBehaviour
{
    public SteamVR_Behaviour_Pose pose;
    public SteamVR_Action_Boolean grip;
    public SteamVR_Action_Boolean trigger;
    public SteamVR_Input_Sources hand;
    public LineRenderer lr;

    public float kAdjust;
    
    public GameObject grabObject;

    private void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    void Update()
    {
        /*if (grip.GetStateDown(hand))
        {
            Catch();
        }
        if (grip.GetStateUp(hand))
        {
            Throw();
        }*/
    }

    internal void 놔줘()
    {
        grabObject = null;
    }

    private void Throw()
    {
        if (grabObject != null)
        {
            //grabObject.놓다(pose);
            //grabObject = null;
            grabObject.transform.SetParent(null, true);
            grabObject = null;
        }
    }

    private void Catch()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 0.05f, LayerMask.GetMask("Item"));
        if (cols.Length > 0)
        {
            grabObject = cols[0].gameObject;
            if (grabObject != null)
            {
                //grabObject.잡다(transform.position, transform);
                // 만약 다른손이 잡고있던 물체였다면 다른손에게 "놔줘" 라고 요청하고싶다
                if (grabObject.transform.parent != null)
                {
                    grabObject.transform.parent = null;
                }
                // grabObject.transform.position = transform.position + transform.forward * 0.01f;
                grabObject.transform.parent = gameObject.transform;
            }
        }
    }

}
