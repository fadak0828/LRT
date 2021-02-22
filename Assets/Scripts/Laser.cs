using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int reflectionCount = 0;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        reflectionCount = 0;
        ReflectLaser(0, transform.position, transform.forward);
    }

    private void ReflectLaser(int lineIndex, Vector3 point, Vector3 dir) {
        lineRenderer.positionCount = reflectionCount + 2;

        RaycastHit hit;
        // 레이캐스트
        if (Physics.Raycast(point, dir, out hit))
        {
            // 맞은곳까지 라인 그리기
            lineRenderer.SetPosition(lineIndex + 1, hit.point);

            // 맞은 오브젝트가 거울이면
            if (hit.transform.name.Contains("Mirror")) {
                reflectionCount++;
                // 반사벡터 = 빛 - 2 * (노말dot빛) * 노말
                Vector3 reflectionDir = dir - 2 * (Vector3.Dot(hit.normal, dir)) * hit.normal;
                ReflectLaser(lineIndex + 1, hit.point, reflectionDir);
            }
        }
        else
        {
            // 안맞았으면 1000유닛까지 라인 그리기
            lineRenderer.SetPosition(lineIndex + 1, dir * 1000);
        }
    }
}