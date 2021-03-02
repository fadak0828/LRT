using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleport : MonoBehaviour
{
    public GameObject maker;
    public GameObject cam;
    public Transform hand;
    public float turnDegree = 30;
    public SteamVR_Action_Boolean teleport;
    public SteamVR_Action_Boolean snapTurnLeft;
    public SteamVR_Action_Boolean snapTurnRight;
    public LineRenderer lr;

    // 마커의 기본크기를 기억하고 싶다
    // 마커를 이동시키면 (새로운 크기 = 기본크기 * 거리)
    Vector3 makerOriginScale;
    public float kAdjust = 1;

    void Start()
    {
        maker.SetActive(false);
        lr.enabled = false;
        lr.startWidth = 0.003f;
        lr.endWidth = 0.003f;
        makerOriginScale = maker.transform.localScale;
    }

    void Update()
    {
        Turn();
        // 만약 왼쪽 컨트롤러의 telefort 버튼을 누르면
        bool teleportButtonDown = teleport.GetStateDown(SteamVR_Input_Sources.LeftHand);
        if (teleportButtonDown)
        {
            // lr을 켜고싶다
            lr.enabled = true;
            maker.SetActive(true);
        }

        // Ray를 이용해서 왼쪽 컨트롤러의 앞방향으로 바라보고싶다
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, ray.origin);
        RaycastHit hitInfo;
        bool isRayCast = Physics.Raycast(ray, out hitInfo, 10, LayerMask.GetMask("Floor", "Wall"));
        if (isRayCast)
        {
            // 조준선이 보이고
            lr.SetPosition(1, hitInfo.point);
        }
        else
        {
            // 허공
            Vector3 pos = ray.origin + ray.direction * 100;
            lr.SetPosition(1, pos);
        }

        
        // ray가 floor에 맞았으면 마커의 position을 hitInfo.point와 같게한다 
        if (teleport.GetState(SteamVR_Input_Sources.LeftHand) && hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Floor")) {
            maker.transform.position = hitInfo.point;
            maker.SetActive(true);
        } else {
            maker.SetActive(false);
        }

        // 그렇지 않고 왼쪽 컨트롤러의 teleport 버튼을 떼면
        if (teleport.GetStateUp(SteamVR_Input_Sources.LeftHand))
        {
            // 조준점을 안보이게 하고싶다
            maker.SetActive(false);
            // lr을 끄고싶다
            lr.enabled = false;

            if (isRayCast)
            {
                // 이때 Ray로 바라본곳에 Floor가 있다면
                int hitlayer = hitInfo.transform.gameObject.layer;
                if (hitlayer == LayerMask.NameToLayer("Floor"))
                {
                    // 그곳으로 이동하고 싶다
                    transform.position = hitInfo.point + new Vector3(0, transform.position.y, 0);

                    Vector3 headPos = SteamVR_Render.Top().head.position;
                    SteamVR_Render.Top().head.position = new Vector3(0, headPos.y, 0);
                    
                    // tower 같은 곳으로 이동할때 사용함
                    //transform.position = hitInfo.transform.position;
                }

            }

        }

    }

    private void Turn() {
        if (snapTurnLeft.GetStateDown(SteamVR_Input_Sources.LeftHand)) {
            print("turn left");
            transform.Rotate(0, -turnDegree, 0);
        } else if (snapTurnRight.GetStateDown(SteamVR_Input_Sources.LeftHand)) {
            print("turn right");
            transform.Rotate(0, turnDegree, 0);
        }
    }
}
