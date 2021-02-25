using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Player가 물건을 집고 내려놓게 하고 싶다
public class GrabAndDrop : MonoBehaviour
{
    public GameObject handleObject;
    public Transform hand;

    void Start()
    {

    }

    void Update()
    {
        if (handleObject != null)
        {
            handleObject.transform.position = hand.transform.position + hand.transform.forward * 2;
            handleObject.transform.forward = hand.forward;
        }

        // 1. 마우스 왼쪽 버튼을 눌렀을 때
        if (Input.GetButtonDown("Fire1"))
        {
            // 2. 만약 플레이어가 물건을 들고있다면
            if (handleObject != null)
            {
                // 3. 물건을 놓는다
                handleObject.transform.SetParent(null, true);
                handleObject = null;
            }
            // 4. 만약 플레이어가 물건을 들고있지 않으면
            else
            {
                // 5. Ray를 이용해서 시선을 바라보고
                Ray ray = new Ray(hand.position, hand.forward);
                // 6. Ray를 쏜다
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    // 7. 레이에 맞은 물건이 잡을수 있는 물건이면
                    if (hitInfo.collider.gameObject.name.Contains("Cube"))
                    {
                        // 8. 물건을 잡는다
                        handleObject = hitInfo.collider.gameObject;

                        handleObject.transform.SetParent(gameObject.transform);
                        handleObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        handleObject.transform.localPosition = Vector3.zero;

                    }
                }

            }

        }
    }


}