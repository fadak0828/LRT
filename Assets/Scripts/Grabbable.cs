using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grabbable : MonoBehaviour
{
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    internal void 잡다(Vector3 pos, Transform parent)
    {
        // 만약 다른손이 잡고있던 물체였다면 다른손에게 "놔줘" 라고 요청하고싶다
        if (transform.parent != null)
        {
            transform.parent.GetComponent<Grip>().놔줘();
            transform.parent = null;
        }
        transform.position = pos;
        transform.parent = parent;
        GetComponent<Rigidbody>().isKinematic = true;

    }

    internal void 놓다(Valve.VR.SteamVR_Behaviour_Pose pose)
    {
        transform.parent = null;
    }
}
