using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RotateSliderScript : MonoBehaviour
{
    public GameObject targetObj;
    public bool shuiPing = true;

    public void RotateObj()
    {
        //��Ԫ��ת��ʸ��ֵ
        Vector3 initAngle = targetObj.transform.rotation.eulerAngles;
        if (shuiPing)
        {
            //targetObj.transform.Rotate(0, this.GetComponent<Slider>().value, 0);
            //��Ԫ��ŷ����
            targetObj.transform.rotation = Quaternion.Euler(initAngle.x, this.GetComponent<Slider>().value, initAngle.z);
        }
        else
        {
            targetObj.transform.rotation = Quaternion.Euler(initAngle.x, initAngle.y, this.GetComponent<Slider>().value);
        }
    }
}
