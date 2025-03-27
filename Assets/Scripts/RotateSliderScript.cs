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
        //四元数转换矢量值
        Vector3 initAngle = targetObj.transform.rotation.eulerAngles;
        if (shuiPing)
        {
            //targetObj.transform.Rotate(0, this.GetComponent<Slider>().value, 0);
            //四元数欧拉角
            targetObj.transform.rotation = Quaternion.Euler(initAngle.x, this.GetComponent<Slider>().value, initAngle.z);
        }
        else
        {
            targetObj.transform.rotation = Quaternion.Euler(initAngle.x, initAngle.y, this.GetComponent<Slider>().value);
        }
    }
}
