using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDayLighting : MonoBehaviour
{
    public Light directionalLight; // ���ó����е�Directional Light
    public Gradient sunriseGradient; // �����ճ����������ɫ����
    public float dayStartTime = 6.0f; // �ճ�ʱ�䣨Сʱ��
    public float dayEndTime = 18.0f; // ����ʱ�䣨Сʱ��
    public float transitionTime = 2.0f; // ��ɫ�仯�Ĺ���ʱ�䣨Сʱ��

    void Start()
    {
        if (directionalLight == null)
        {
            directionalLight = GetComponent<Light>();
        }
    }

    void Update()
    {
        float currentTime = (Time.time / 3600) % 24; // ��ȡ��ǰʱ���Сʱ����0-23��
        float t = Mathf.Clamp01((currentTime - dayStartTime) / transitionTime); // �����ճ����ɵĲ���
        float sunsetColor = Mathf.Clamp01((dayEndTime - currentTime) / transitionTime); // ����������ɵĲ���

        // ���ݵ�ǰʱ����������ɫ
        if (currentTime >= dayStartTime && currentTime <= dayEndTime)
        {
            directionalLight.color = sunriseGradient.Evaluate(t); // ʹ�ý�����ɫ
        }
        else if (currentTime > dayEndTime) // ҹ����ɫ���Ը�����Ҫ���ã������ʹ�ú�ɫ
        {
            directionalLight.color = Color.black;
        }
        else if (currentTime < dayStartTime) // ʹ��������ɫ�������ʹ�����佥��ķ�����ɫ
        {
            directionalLight.color = sunriseGradient.Evaluate(1 - sunsetColor); // ʹ�����佥����ɫ�ķ���Ч��
        }
    }
}