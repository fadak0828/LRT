using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;

    public Material laserMaterial;

    private void Awake() {
        Instance = this;
    }
}
