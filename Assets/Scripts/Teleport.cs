﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleport : MonoBehaviour
{
    public GameObject maker;
    public Transform hand;
    public SteamVR_Action_Boolean teleport;
    public LineRenderer lr;

    // 마커의 기본크기를 기억하고 싶다
    // 마커를 이동시키면 (새로운 크기 = 기본크기 * 거리)
    Vector3 makerOriginScale;
    public float kAdjust = 1;

    void Start()
    {
        maker.SetActive(false);
        lr.enabled = false;
        makerOriginScale = maker.transform.localScale;
    }

    void Update()
    {
        // 만약 왼쪽 컨트롤러의 telefort 버튼을 누르면
        if (teleport.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            // 조준점이 보이고
            maker.SetActive(true);
            // lr을 켜고싶다
            lr.enabled = true;
            
        }

        // Ray를 이용해서 왼쪽 컨트롤러의 앞방향으로 바라보고싶다
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, ray.origin);
        RaycastHit hitInfo;
        bool isRayCast = Physics.Raycast(ray, out hitInfo);
        if (isRayCast)
        {
            lr.SetPosition(1, hitInfo.point);
            maker.transform.position = hitInfo.point + hitInfo.normal * 0.1f;
            maker.transform.localScale = makerOriginScale * kAdjust * hitInfo.distance;
            maker.transform.forward = hitInfo.normal;
        }
        else
        {
            // 허공
            Vector3 pos = ray.origin + ray.direction * 100;
            lr.SetPosition(1, pos);
            maker.transform.position = pos;
            maker.transform.localScale = makerOriginScale * kAdjust * 100;
            maker.transform.forward = ray.origin;
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
                    transform.position = hitInfo.point;
                    // tower 같은 곳으로 이동할때 사용함
                    //transform.position = hitInfo.transform.position;
                }

            }

        }

    }
}