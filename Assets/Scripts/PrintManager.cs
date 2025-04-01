using UnityEngine;
using UnityEngine.UI;

public class PrintManager : MonoBehaviour
{
    [Header("Print Settings")]
    // 桌子上放置迷你版 3D 物体的位置（建议在桌面上放置一个空物体作为标记）
    public Transform tableLocation;
    // 缩小版整体的缩放比例（例如 0.1f 表示 10% 尺寸）
    public float miniatureScale = 1.0f;


    private void Start()
    {
       
    }

    /// <summary>
    /// 打印函数：遍历 creationParent 下所有 TrailRenderer，Bake 网格，并生成迷你版复制体放在桌子上
    /// </summary>
    public void PrintCreation()
    {
        // 创建一个父对象作为打印结果，并放置在桌子指定位置
        GameObject printedCreation = new GameObject("PrintedCreation");
        printedCreation.transform.position = tableLocation.position;
        printedCreation.transform.rotation = tableLocation.rotation;
        printedCreation.transform.localScale = Vector3.one * miniatureScale;

        // 遍历 creationParent 下所有 TrailRenderer 组件
        TrailRenderer[] trails = FindObjectsOfType<TrailRenderer>();
        foreach (TrailRenderer trail in trails)
        {
            // 使用 BakeMesh 方法生成网格，注意这里生成的是静态网格
            Mesh bakedMesh = new Mesh();
            trail.BakeMesh(bakedMesh, true);

            // 为每个 BakeMesh 创建一个新的 GameObject，并将其设置为打印结果的子物体
            GameObject meshObj = new GameObject("PrintedTrailMesh");
            meshObj.tag = "Trail";
            meshObj.transform.parent = printedCreation.transform;
            // 为了保持相对位置与旋转一致
            meshObj.transform.localPosition = Vector3.zero;
            meshObj.transform.localRotation = Quaternion.Euler(0,0,0);
            meshObj.transform.localScale = Vector3.one; // 已经在父对象上应用了缩放

            // 添加 MeshFilter 和 MeshRenderer，并设置网格与材质
            MeshFilter mf = meshObj.AddComponent<MeshFilter>();
            mf.mesh = bakedMesh;
            MeshRenderer mr = meshObj.AddComponent<MeshRenderer>();
            // 使用 trail 的材质（如果需要，可以修改为其他材质）
            mr.material = trail.sharedMaterial;
        }
    }
}
