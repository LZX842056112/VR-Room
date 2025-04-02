using UnityEngine;
using UnityEngine.UI;

public class PrintManager : MonoBehaviour
{
    [Header("Print Settings")]
    // �����Ϸ�������� 3D �����λ�ã������������Ϸ���һ����������Ϊ��ǣ�
    public Transform tableLocation;
    // ��С����������ű��������� 0.1f ��ʾ 10% �ߴ磩
    public float miniatureScale = 0.5f;

    /// <summary>
    /// ��ӡ���������� creationParent ������ TrailRenderer��Bake ���񣬲���������渴�������������
    /// </summary>
    public void PrintCreation()
    {
        GameObject myObject= GameObject.Find("PrintedCreation");
        if(myObject != null)
        {
            Destroy(myObject);
        }

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
        GameObject obj = GameObject.FindGameObjectWithTag("obj");
        GameObject obj2 = Instantiate(obj);
        obj2.tag = "Trail";
        obj2.transform.parent = printedCreation.transform;
        obj2.transform.localPosition = new Vector3(0,1.5f,0.5f);
        obj2.transform.localRotation = Quaternion.Euler(0, 0, 0);
        obj2.transform.localScale = Vector3.one * 0.5f;

    }
}
