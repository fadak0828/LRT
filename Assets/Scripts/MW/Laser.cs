using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LaserInput {
    void OnLaserInput(LaserHit hit);
    void OnLaserInputEnd(LaserHit hit);
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

    public override bool Equals(object obj)
    {
        GameObject n = ((LaserHit)obj).raycastHit.collider.gameObject;
        GameObject nn = raycastHit.collider.gameObject;

        Debug.Log($"{n.name} {nn.name} {n == nn} {n.Equals(nn)}");
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
public enum LaserColor {
    RED,
    GREEN,
    BLUE,
    YELLOW,
    PURPLE,
    CYAN,
    WHITE,
    NONE
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
            if (prevLaserHit != null && prevLaserHit.Equals(laserHit))
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
