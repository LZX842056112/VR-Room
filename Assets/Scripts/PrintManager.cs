using UnityEngine;
using UnityEngine.UI;

public class PrintManager : MonoBehaviour
{
    [Header("Print Settings")]
    // �����Ϸ�������� 3D �����λ�ã������������Ϸ���һ����������Ϊ��ǣ�
    public Transform tableLocation;
    // ��С����������ű��������� 0.1f ��ʾ 10% �ߴ磩
    public float miniatureScale = 1.0f;


    private void Start()
    {
       
    }

    /// <summary>
    /// ��ӡ���������� creationParent ������ TrailRenderer��Bake ���񣬲���������渴�������������
    /// </summary>
    public void PrintCreation()
    {
        // ����һ����������Ϊ��ӡ�����������������ָ��λ��
        GameObject printedCreation = new GameObject("PrintedCreation");
        printedCreation.transform.position = tableLocation.position;
        printedCreation.transform.rotation = tableLocation.rotation;
        printedCreation.transform.localScale = Vector3.one * miniatureScale;

        // ���� creationParent ������ TrailRenderer ���
        TrailRenderer[] trails = FindObjectsOfType<TrailRenderer>();
        foreach (TrailRenderer trail in trails)
        {
            // ʹ�� BakeMesh ������������ע���������ɵ��Ǿ�̬����
            Mesh bakedMesh = new Mesh();
            trail.BakeMesh(bakedMesh, true);

            // Ϊÿ�� BakeMesh ����һ���µ� GameObject������������Ϊ��ӡ�����������
            GameObject meshObj = new GameObject("PrintedTrailMesh");
            meshObj.tag = "Trail";
            meshObj.transform.parent = printedCreation.transform;
            // Ϊ�˱������λ������תһ��
            meshObj.transform.localPosition = Vector3.zero;
            meshObj.transform.localRotation = Quaternion.Euler(0,0,0);
            meshObj.transform.localScale = Vector3.one; // �Ѿ��ڸ�������Ӧ��������

            // ��� MeshFilter �� MeshRenderer�����������������
            MeshFilter mf = meshObj.AddComponent<MeshFilter>();
            mf.mesh = bakedMesh;
            MeshRenderer mr = meshObj.AddComponent<MeshRenderer>();
            // ʹ�� trail �Ĳ��ʣ������Ҫ�������޸�Ϊ�������ʣ�
            mr.material = trail.sharedMaterial;
        }
    }
}
