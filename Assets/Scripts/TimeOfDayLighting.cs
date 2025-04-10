using System;
using UnityEngine;

public class TimeOfDayLighting : MonoBehaviour
{
    public Light directionalLight;             // 引用场景中的 Directional Light
    public Gradient sunriseGradient;           // 用于日出/日落渐变的颜色
    public float dayStartTime = 360f;          // 日出时间（分钟），例如：6:00 AM = 6 * 60 = 360
    public float dayEndTime = 1140f;           // 日落时间（分钟），例如：19:00 = 19 * 60 = 1140
    public float transitionTime = 1f;        // 颜色渐变过渡时间（分钟）

    void Update()
    {
        if (directionalLight == null)
        {
            directionalLight = GetComponent<Light>();
        }
        UpdateRealTimer();
    }

    private void UpdateRealTimer()
    {
        DateTime now = DateTime.Now;
        // 计算自午夜以来的分钟数
        float currentTimeMinutes = now.Hour * 60f + now.Minute;
        print("111   " + currentTimeMinutes);

        // 根据当前时间计算日出和日落的过渡参数 [0, 1]
        float tSunrise = Mathf.Clamp01((currentTimeMinutes - dayStartTime) / transitionTime);
        float tSunset = Mathf.Clamp01((dayEndTime - currentTimeMinutes) / transitionTime);

        // 调整光照颜色
        if (currentTimeMinutes >= dayStartTime && currentTimeMinutes <= dayEndTime)
        {
            // 日间，使用 sunriseGradient 对应 tSunrise 参数来计算颜色
            directionalLight.color = sunriseGradient.Evaluate(tSunrise);
        }
        else if (currentTimeMinutes > dayEndTime)
        {
            // 夜间过渡，若需要可以设置为其它颜色
            directionalLight.color = Color.black;
        }
        else // (currentTimeMinutes < dayStartTime)
        {
            // 清晨前的过渡，可以使用 sunset 的渐变色的反向效果
            directionalLight.color = sunriseGradient.Evaluate(1 - tSunset);
        }

        // 根据当天时间更新光源的方向（假设光源绕 X 轴旋转）
        // 全天 1440 分钟对应 360°，计算当前角度
        float angle = (currentTimeMinutes / 1440f) * 360f;
        directionalLight.transform.eulerAngles = new Vector3(angle, 0, 0);
    }
}
