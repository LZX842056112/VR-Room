using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDayLighting : MonoBehaviour
{
    public Light directionalLight; // 引用场景中的Directional Light
    public Gradient sunriseGradient; // 用于日出和日落的颜色渐变
    public float dayStartTime = 6.0f; // 日出时间（小时）
    public float dayEndTime = 18.0f; // 日落时间（小时）
    public float transitionTime = 2.0f; // 颜色变化的过渡时间（小时）

    void Start()
    {
        if (directionalLight == null)
        {
            directionalLight = GetComponent<Light>();
        }
    }

    void Update()
    {
        float currentTime = (Time.time / 3600) % 24; // 获取当前时间的小时数（0-23）
        float t = Mathf.Clamp01((currentTime - dayStartTime) / transitionTime); // 计算日出过渡的参数
        float sunsetColor = Mathf.Clamp01((dayEndTime - currentTime) / transitionTime); // 计算日落过渡的参数

        // 根据当前时间调整光的颜色
        if (currentTime >= dayStartTime && currentTime <= dayEndTime)
        {
            directionalLight.color = sunriseGradient.Evaluate(t); // 使用渐变颜色
        }
        else if (currentTime > dayEndTime) // 夜间颜色可以根据需要设置，这里简单使用黑色
        {
            directionalLight.color = Color.black;
        }
        else if (currentTime < dayStartTime) // 使用日落颜色，这里简单使用日落渐变的反向颜色
        {
            directionalLight.color = sunriseGradient.Evaluate(1 - sunsetColor); // 使用日落渐变颜色的反向效果
        }
    }
}