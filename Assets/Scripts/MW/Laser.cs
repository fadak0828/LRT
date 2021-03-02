using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LaserInput : MonoBehaviour {
    abstract public void OnLaserInput(LaserHit hit);
    abstract public void OnLaserInputEnd(LaserHit hit);
}

public class LaserHit {
    public RaycastHit raycastHit;
    public LaserInput hitLaserInput;
    public float width;
    public LaserColor color;
    public Vector3 inputDir;
    public LaserHit(RaycastHit hit, LaserInput hitLaserInput, Vector3 inputDir, LaserColor color, float width) {
        this.raycastHit = hit;
        this.hitLaserInput = hitLaserInput;
        this.inputDir = inputDir;
        this.color = color;
        this.width = width;
    }
}
public enum LaserColor {
    /*
        NONE    = 000
        RED     = 001
        GREEN   = 010
        BLUE    = 100
        YELLOW  = (R + G)     = 011
        PURPLE  = (R + B)     = 101
        CYAN    = (G + B)     = 110
        WHITE   = (R + G + B) = 111
    */
    NONE = 0,
    RED = 1 << 0,
    GREEN = 1 << 1,
    BLUE = 1 << 2,
    YELLOW = LaserColor.RED | LaserColor.GREEN,
    PURPLE = LaserColor.RED | LaserColor.BLUE,
    CYAN = LaserColor.GREEN | LaserColor.BLUE,
    WHITE = LaserColor.RED | LaserColor.GREEN | LaserColor.BLUE
}

public class Laser {
    public static void Shoot(LineRenderer lineRenderer, Vector3 startPos, Vector3 dir, float width, LaserColor color, ref LaserHit prevLaserHit) {
        lineRenderer.positionCount = 2;

        Color lineColor = Laser.GetColor(color);
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        lineRenderer.SetPosition(0, startPos);

        RaycastHit hit;
        LaserHit laserHit = null;
        // 레이캐스트
        if (Physics.Raycast(startPos, dir, out hit))
        {
            // 맞은곳까지 라인 그리기
            lineRenderer.SetPosition(1, hit.point);

            LaserInput laserHitObj = hit.collider.GetComponent<LaserInput>();
            if (laserHitObj != null) {
                laserHit = new LaserHit(hit, laserHitObj, dir, color, width);
            }
        }
        else
        {
            // 안맞았으면 1000유닛까지 라인 그리기
            lineRenderer.SetPosition(1, dir * 1000);
        }

        if (laserHit != null)
        {
            if (prevLaserHit != null && !GameObject.ReferenceEquals(prevLaserHit.hitLaserInput.gameObject, laserHit.hitLaserInput.gameObject))
            {
                prevLaserHit.hitLaserInput.OnLaserInputEnd(prevLaserHit);
            } 
            laserHit.hitLaserInput.OnLaserInput(laserHit);
            prevLaserHit = laserHit;
        }
        else
        {
            if (prevLaserHit != null)
            {
                prevLaserHit.hitLaserInput.OnLaserInputEnd(prevLaserHit);
                prevLaserHit = null;
            }
        }
    }

    public static Color GetColor(LaserColor lc) {
        switch (lc)
        {
            case LaserColor.RED:
                return Color.red;
            case LaserColor.BLUE:
                return Color.blue;
            case LaserColor.GREEN:
                return Color.green;
            case LaserColor.CYAN:
                return Color.cyan;
            case LaserColor.PURPLE:
                return Color.magenta;
            case LaserColor.WHITE:
                return Color.white;
            case LaserColor.YELLOW:
                return Color.yellow;
            default:
                throw new System.Exception("존재하지 않는 LaserColor 값 입니다.");
        }
    }
}
