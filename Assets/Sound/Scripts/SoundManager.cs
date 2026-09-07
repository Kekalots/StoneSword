using UnityEngine;

//音效管理器：运行时程序化生成 8bit 复古音效（无需任何音频文件）
//挂玩家身上（每场景都有），单例调用 SoundManager.Instance.PlayXxx()
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource sfxSource;
    private AudioSource bgmSource;

    private AudioClip attackClip;
    private AudioClip coinClip;
    private AudioClip jumpClip;
    private AudioClip hurtClip;

    private const int SampleRate = 22050;

    private void Awake()
    {
        Instance = this;
        sfxSource = gameObject.AddComponent<AudioSource>();
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        attackClip = GenerateSwing();
        coinClip = GenerateCoin();
        jumpClip = GenerateJump();
        hurtClip = GenerateHurt();
    }

    public void PlayAttack() => PlaySfx(attackClip, 0.9f);
    public void PlayCoin() => PlaySfx(coinClip, 0.7f);
    public void PlayJump() => PlaySfx(jumpClip, 0.8f);
    public void PlayHurt() => PlaySfx(hurtClip, 0.8f);

    //背景音乐预留：后续可换真实素材（AudioClip 资源）
    public void SetBgm(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    private void PlaySfx(AudioClip clip, float volume)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    private static AudioClip CreateClip(string name, float duration, System.Func<float, float> sample)
    {
        int samples = (int)(SampleRate * duration);
        float[] data = new float[samples];
        for (int i = 0; i < samples; i++)
        {
            data[i] = sample((float)i / SampleRate);
        }
        AudioClip clip = AudioClip.Create(name, samples, 1, SampleRate, false);
        clip.SetData(data, 0);
        return clip;
    }

    //攻击挥砍：白噪声短爆 + 快速衰减（"唰"）
    private static AudioClip GenerateSwing()
    {
        return CreateClip("swing", 0.15f, t =>
        {
            float noise = Random.value * 2f - 1f;
            return noise * Mathf.Exp(-t * 30f) * 0.9f;
        });
    }

    //拾取金币：高频双音 "叮~"
    private static AudioClip GenerateCoin()
    {
        return CreateClip("coin", 0.18f, t =>
        {
            float env = Mathf.Exp(-t * 25f);
            float tone = Mathf.Sin(2 * Mathf.PI * 880 * t) + 0.5f * Mathf.Sin(2 * Mathf.PI * 1760 * t);
            return tone * env * 0.5f;
        });
    }

    //跳跃：上升扫频（chirp，相位积分保证音高连续）
    private static AudioClip GenerateJump()
    {
        float f0 = 300f, f1 = 700f, dur = 0.2f;
        float k = (f1 - f0) / dur;
        return CreateClip("jump", dur, t =>
        {
            float phase = 2 * Mathf.PI * (f0 * t + 0.5f * k * t * t);
            return Mathf.Sin(phase) * Mathf.Exp(-t * 8f);
        });
    }

    //受伤：低频下降闷响
    private static AudioClip GenerateHurt()
    {
        float f0 = 220f, f1 = 90f, dur = 0.25f;
        float k = (f1 - f0) / dur;
        return CreateClip("hurt", dur, t =>
        {
            float phase = 2 * Mathf.PI * (f0 * t + 0.5f * k * t * t);
            return Mathf.Sin(phase) * Mathf.Exp(-t * 12f);
        });
    }
}