using UnityEngine;

/// <summary>
/// This script creates a trail at the location of a gameobject with a particular width and color.
/// </summary>

public class CreateTrail : MonoBehaviour
{
    public GameObject trailPrefab = null;

    private float width = 0.05f;
    private Color color = Color.white;

    private GameObject currentTrail = null;

    public LayerMask paintableLayer; // 设置可绘制物体的层级
    public float checkRadius = 0.1f; // 检测周围物体的半径

    public void StartTrail()
    {
        if (!currentTrail)
        {
            //// 定义检测位置：画笔前方0.01米处
            Vector3 detectionPosition = transform.position;
            Vector3 trailPosition = detectionPosition; // 默认位置

            //// 优先使用射线检测正前方的碰撞点
            //RaycastHit hit;
            //bool hasHit = Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, paintableLayer);

            //if (hasHit)
            //{
            //    trailPosition = hit.point;
            //}
            //else
            //{
            //    // 射线未命中，使用OverlapSphere检测附近的物体并获取最近点
            //    Collider[] colliders = Physics.OverlapSphere(detectionPosition, checkRadius, paintableLayer);
            //    float minDistance = Mathf.Infinity;
            //    Vector3 closestPoint = detectionPosition;

            //    foreach (Collider collider in colliders)
            //    {
            //        Vector3 point = collider.ClosestPoint(detectionPosition);
            //        float distance = Vector3.Distance(detectionPosition, point);
            //        if (distance < minDistance)
            //        {
            //            minDistance = distance;
            //            closestPoint = point;
            //        }
            //    }
            //    trailPosition = (colliders.Length > 0) ? closestPoint : detectionPosition;
            //}
            currentTrail = Instantiate(trailPrefab, trailPosition, transform.rotation, transform);
            ApplySettings(currentTrail);
        }
    }

    private void ApplySettings(GameObject trailObject)
    {
        TrailRenderer trailRenderer = trailObject.GetComponent<TrailRenderer>();
        trailRenderer.widthMultiplier = width;
        trailRenderer.startColor = color;
        trailRenderer.endColor = color;
    }

    public void EndTrail()
    {
        if (currentTrail)
        {
            currentTrail.transform.parent = null;
            currentTrail = null;
        }
    }

    public void SetWidth(float value)
    {
        width = value;
    }

    public void SetColor(Color value)
    {
        color = value;
    }
}
