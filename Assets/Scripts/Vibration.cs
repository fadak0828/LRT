using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Vibration : MonoBehaviour
{
    public static Vibration instance;
    private void Awake()
    {
        instance = this;
    }

    public SteamVR_Action_Vibration vib;

    public void PlayVibration(SteamVR_Input_Sources hand)
    {
        Pulse(1, 150, 75, hand);
    }

    public void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        if (duration > 1) {
            StartCoroutine(IePluse(duration, frequency, amplitude, source));
        } else {
            vib.Execute(0, duration, frequency, amplitude, source);
        }
    }

    public void Stop(SteamVR_Input_Sources source) {
        vib.Execute(0, 0, 0, 0, source);
    }

    private IEnumerator IePluse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source) {
        float interval = 0.05f;
        float vibCount = duration / interval;
        for(int i = 0; i < vibCount; i++) {
            vib.Execute(0, interval, frequency, 1 - (i / vibCount), source);
            yield return new WaitForSeconds(interval);
        }
    }
}