using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LaserColor {
    NONE,
    RED,
    GREEN,
    BLUE,
}

public interface LaserInput {
    void OnInputStart(Laser laser);
    void OnInputEnd(Laser laser);
}

public class Laser : MonoBehaviour
{
    public LaserColor laserColor = LaserColor.RED;
    public float width = 0.2f;

    private LineRenderer lineRenderer;
    private int reflectionCount = 0;

    private List<LaserInput> prevInputList;
    private List<LaserInput> nextInputList;

    private void Awake() {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    void Start()
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));

        prevInputList = new List<LaserInput>();
        nextInputList = new List<LaserInput>();
    }

    void Update()
    {
        Color lineColor = LaserColorToColor(laserColor);
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        lineRenderer.SetPosition(0, transform.position);
        reflectionCount = 0;

        nextInputList = new List<LaserInput>();

        ReflectLaser(0, transform.position, transform.forward);
        CalculateInput();

        prevInputList = nextInputList;
    }

    private void CalculateInput() {
        // 추가된 인풋들
        foreach(LaserInput li in nextInputList) {
            if (prevInputList.Find(pli => li.Equals(pli)) == null) {
                li.OnInputStart(this);
            }
        }

        // 제거된 인풋들
        foreach(LaserInput li in prevInputList) {
            if (nextInputList.Find(nli => li.Equals(nli)) == null) {
                li.OnInputEnd(this);
            }
        }
    }

    private void ReflectLaser(int lineIndex, Vector3 point, Vector3 dir) {
        lineRenderer.positionCount = reflectionCount + 2;

        RaycastHit hit;
        // 레이캐스트
        if (Physics.Raycast(point, dir, out hit))
        {
            // print(hit.collider.name);

            // 맞은곳까지 라인 그리기
            lineRenderer.SetPosition(lineIndex + 1, hit.point);

            string hitObjName = hit.transform.name;
            // 맞은 오브젝트가 거울이면
            if (hitObjName.Contains("Mirror")) {
                reflectionCount++;
                // 반사벡터 = 빛 - 2 * (노말dot빛) * 노말
                Vector3 reflectionDir = dir - 2 * (Vector3.Dot(hit.normal, dir)) * hit.normal;
                ReflectLaser(lineIndex + 1, hit.point, reflectionDir);
            } else if (hitObjName.Contains("Input")) {
                Divider div = hit.collider.gameObject.GetComponent<Divider>();
                nextInputList.Add(div);
            }
        }
        else
        {
            // 안맞았으면 1000유닛까지 라인 그리기
            lineRenderer.SetPosition(lineIndex + 1, dir * 1000);
        }
    }

    private Color LaserColorToColor(LaserColor lc) {
        switch (lc)
        {
            case LaserColor.RED :
                return Color.red;
            case LaserColor.BLUE:
                return Color.blue;
            case LaserColor.GREEN:
                return Color.green;
            default:
                throw new System.Exception("존재하지 않는 LaserColor 값 입니다.");
        }
    }
}