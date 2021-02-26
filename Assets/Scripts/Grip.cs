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

    public GameObject grabObject;

    private void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    void Update()
    {
        // 트리거를 당기면
        if (trigger.GetState(hand))
        {
            // 선을 그리고 싶다.
            lr.enabled = true;

            // Ray를 이용해서 바라보고
            Ray ray = new Ray(transform.position, transform.forward);
            lr.SetPosition(0, ray.origin);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                lr.SetPosition(1, hitInfo.point);
                // 만약 부딪힌것이 Grabbable 이면 당겨오고싶다.
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
                {
                    hitInfo.transform.position = Vector3.Lerp(hitInfo.transform.position, transform.position, Time.deltaTime * 5);
                }
            }
        }
        else
        {
            // 트리거를 놓으면 선을 그리지 않고싶다.
            lr.enabled = false;
        }

        if (grip.GetStateDown(hand))
        {
            Catch();
        }
        if (grip.GetStateUp(hand))
        {
            Throw();
        }
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
            grabObject.transform.parent = null;
            grabObject = null;
        }
    }

    private void Catch()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 100f, LayerMask.GetMask("Item"));
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                grabObject = cols[i].gameObject;
                if (grabObject != null)
                {
                    //grabObject.잡다(transform.position, transform);
                    // 만약 다른손이 잡고있던 물체였다면 다른손에게 "놔줘" 라고 요청하고싶다
                    if (grabObject.transform.parent != null)
                    {
                        grabObject.transform.parent = null;
                    }
                    grabObject.transform.position = transform.position;
                    grabObject.transform.localPosition += new Vector3(0, 0, 0.3f);
                    grabObject.transform.parent = gameObject.transform;
                    break;
                }
            }
        }
    }

}
