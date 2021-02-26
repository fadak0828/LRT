using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour, LaserInput
{
    private LineRenderer lr;
    private LaserHit prevLaserHit;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
    }

    public void OnLaserInput(LaserHit hit) {
        lr.enabled = true;
        Vector3 dir = Vector3.Reflect(hit.inputDir, hit.raycastHit.normal);
        Laser.Shoot(lr, hit.raycastHit.point, dir, hit.width, hit.color, ref prevLaserHit);
    }

    public void OnLaserInputEnd(LaserHit hit) {
        lr.enabled = false;
        if (prevLaserHit != null) {
            prevLaserHit.hitLaserInput.OnLaserInputEnd(prevLaserHit);
            prevLaserHit = null;
        }
    }
}
