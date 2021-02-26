using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


// 만약 Grip 버튼을 뗐는데 손에 Item을 쥐고 있다면 놓고싶다
public class Grip : MonoBehaviour
{
    public SteamVR_Behaviour_Pose pose;
    public SteamVR_Action_Boolean grip;
    public float Addforce = 3;
    GameObject gripObject;

    private void OnTriggerStay(Collider other)
    {
        // 만약 OnTriggerStay에서 부딪힌 상대가 item이고 컨트롤러의 Grip 버튼을 눌렀다면
        if (grip.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            if (other.name.Contains("Item"))
            {
                // 손에 item을 쥐고싶다
                GameObject item = Instantiate(other.gameObject);
                item.name = "Item";
                item.layer = LayerMask.NameToLayer("Item");
                item.transform.position = transform.position;
                item.transform.parent = transform.parent;
                item.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                item.GetComponent<Collider>().isTrigger = false;
                item.GetComponent<Rigidbody>().isKinematic = true;

                gripObject = item;
            }
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        // 만약 Grip 버튼을 뗐는데
        if (grip.GetStateUp(SteamVR_Input_Sources.RightHand))
        {
            // 손에 item을 쥐고 있다면
            if (gripObject != null)
            {
                // 놓고싶다
                gripObject.transform.parent = null;
                gripObject.GetComponent<Collider>().isTrigger = false;

                gripObject = null;
            }

        }
    }
}
