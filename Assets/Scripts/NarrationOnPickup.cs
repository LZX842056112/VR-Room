using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NarrationOnPickup : MonoBehaviour
{
    // 需要朗读的文本（英文）
    [TextArea]
    public string narrationText = "Hello, you have picked up the object.";

    // 物体上的 XRGrabInteractable 组件
    private XRGrabInteractable grabInteractable;

    // 用于播放生成的音频
    public AudioSource audioSource;

    // 如果使用 TTS 插件，可能直接获取生成的 AudioClip
    public AudioClip ttsClip;

    void Start()
    {
        // 获取 XRGrabInteractable 组件
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("没有找到 XRGrabInteractable 组件！");
            return;
        }

        // 订阅拿起事件
        grabInteractable.selectEntered.AddListener(OnPickup);
    }

    // 拿起物体时调用的方法
    private void OnPickup(SelectEnterEventArgs args)
    {
        // 方法一：如果已经预先生成好了音频，直接播放
        if (ttsClip != null)
        {
            PlayAudioClip(ttsClip);
        }
        else
        {
            // 方法二：调用 TTS 服务生成音频（伪代码，根据您使用的 TTS 插件调整）
            GenerateTTSAudio(narrationText);
        }
    }

    // 播放音频的方法
    private void PlayAudioClip(AudioClip clip)
    {
        if (audioSource == null)
        {
            Debug.LogError("没有 AudioSource 组件！");
            return;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }

    // 生成 TTS 音频（伪代码示例）
    private void GenerateTTSAudio(string text)
    {
        // 假设使用 RT-Voice 插件：
        // RTVoice.Speak(text, audioSource);
        // 或者，如果您使用在线 API，您需要发送网络请求获取音频数据并转换为 AudioClip
        Debug.Log("调用 TTS 服务生成音频: " + text);
        // 在获取到音频后，记得调用 PlayAudioClip(generatedClip);
    }

    private void OnDestroy()
    {
        // 注销事件，防止内存泄漏
        if (grabInteractable != null)
            grabInteractable.selectEntered.RemoveListener(OnPickup);
    }
}
