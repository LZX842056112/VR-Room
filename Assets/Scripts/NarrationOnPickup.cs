using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NarrationOnPickup : MonoBehaviour
{
    // ��Ҫ�ʶ����ı���Ӣ�ģ�
    [TextArea]
    public string narrationText = "Hello, you have picked up the object.";

    // �����ϵ� XRGrabInteractable ���
    private XRGrabInteractable grabInteractable;

    // ���ڲ������ɵ���Ƶ
    public AudioSource audioSource;

    // ���ʹ�� TTS ���������ֱ�ӻ�ȡ���ɵ� AudioClip
    public AudioClip ttsClip;

    void Start()
    {
        // ��ȡ XRGrabInteractable ���
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("û���ҵ� XRGrabInteractable �����");
            return;
        }

        // ���������¼�
        grabInteractable.selectEntered.AddListener(OnPickup);
    }

    // ��������ʱ���õķ���
    private void OnPickup(SelectEnterEventArgs args)
    {
        // ����һ������Ѿ�Ԥ�����ɺ�����Ƶ��ֱ�Ӳ���
        if (ttsClip != null)
        {
            PlayAudioClip(ttsClip);
        }
        else
        {
            // ������������ TTS ����������Ƶ��α���룬������ʹ�õ� TTS ���������
            GenerateTTSAudio(narrationText);
        }
    }

    // ������Ƶ�ķ���
    private void PlayAudioClip(AudioClip clip)
    {
        if (audioSource == null)
        {
            Debug.LogError("û�� AudioSource �����");
            return;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }

    // ���� TTS ��Ƶ��α����ʾ����
    private void GenerateTTSAudio(string text)
    {
        // ����ʹ�� RT-Voice �����
        // RTVoice.Speak(text, audioSource);
        // ���ߣ������ʹ������ API������Ҫ�������������ȡ��Ƶ���ݲ�ת��Ϊ AudioClip
        Debug.Log("���� TTS ����������Ƶ: " + text);
        // �ڻ�ȡ����Ƶ�󣬼ǵõ��� PlayAudioClip(generatedClip);
    }

    private void OnDestroy()
    {
        // ע���¼�����ֹ�ڴ�й©
        if (grabInteractable != null)
            grabInteractable.selectEntered.RemoveListener(OnPickup);
    }
}
