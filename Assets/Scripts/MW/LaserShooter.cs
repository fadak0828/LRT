﻿using System.Collections;
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
        lineRenderer.enabled = false;
    }

    void Start()
    {
        lineRenderer.material = MaterialManager.Instance.laserMaterial;
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
