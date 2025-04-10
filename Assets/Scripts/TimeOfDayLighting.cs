using System;
using UnityEngine;

public class TimeOfDayLighting : MonoBehaviour
{
    public Light directionalLight;             // ���ó����е� Directional Light
    public Gradient sunriseGradient;           // �����ճ�/���佥�����ɫ
    public float dayStartTime = 360f;          // �ճ�ʱ�䣨���ӣ������磺6:00 AM = 6 * 60 = 360
    public float dayEndTime = 1140f;           // ����ʱ�䣨���ӣ������磺19:00 = 19 * 60 = 1140
    public float transitionTime = 1f;        // ��ɫ�������ʱ�䣨���ӣ�

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
        // ��������ҹ�����ķ�����
        float currentTimeMinutes = now.Hour * 60f + now.Minute;
        print("111   " + currentTimeMinutes);

        // ���ݵ�ǰʱ������ճ�������Ĺ��ɲ��� [0, 1]
        float tSunrise = Mathf.Clamp01((currentTimeMinutes - dayStartTime) / transitionTime);
        float tSunset = Mathf.Clamp01((dayEndTime - currentTimeMinutes) / transitionTime);

        // ����������ɫ
        if (currentTimeMinutes >= dayStartTime && currentTimeMinutes <= dayEndTime)
        {
            // �ռ䣬ʹ�� sunriseGradient ��Ӧ tSunrise ������������ɫ
            directionalLight.color = sunriseGradient.Evaluate(tSunrise);
        }
        else if (currentTimeMinutes > dayEndTime)
        {
            // ҹ����ɣ�����Ҫ��������Ϊ������ɫ
            directionalLight.color = Color.black;
        }
        else // (currentTimeMinutes < dayStartTime)
        {
            // �峿ǰ�Ĺ��ɣ�����ʹ�� sunset �Ľ���ɫ�ķ���Ч��
            directionalLight.color = sunriseGradient.Evaluate(1 - tSunset);
        }

        // ���ݵ���ʱ����¹�Դ�ķ��򣨼����Դ�� X ����ת��
        // ȫ�� 1440 ���Ӷ�Ӧ 360�㣬���㵱ǰ�Ƕ�
        float angle = (currentTimeMinutes / 1440f) * 360f;
        directionalLight.transform.eulerAngles = new Vector3(angle, 0, 0);
    }
}
