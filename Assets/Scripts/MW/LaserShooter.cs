using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public LaserColor laserColor = LaserColor.RED;
    public float width = 0.2f;

    public LaserHit prevLaserHit;
    private LineRenderer lineRenderer;

    private void Awake() {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    void Start()
    {
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
    }

    void Update()
    {
        Laser.Shoot(lineRenderer, transform.position, transform.forward, width, laserColor, ref prevLaserHit);
    }

    private void OnEnable() {
        lineRenderer.enabled = true;    
        prevLaserHit = null;
    }
    private void OnDisable() {
        lineRenderer.enabled = false;
        prevLaserHit = null;
    }
}
