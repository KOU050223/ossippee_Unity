using UnityEngine;

public class SoundFlowManager : MonoBehaviour
{
    public AudioSource loopAudioSource;   // ループ再生用 AudioSource
    public AudioClip loopClip;            // ループ用 AudioClip（例：pour_loop）

    public AudioSource oneShotAudioSource; // 効果音用 AudioSource（再生・停止可）
    public AudioClip oneShotClip;          // 再生する効果音

    void Start()
    {
        // null チェックして AudioSource を初期設定
        if (loopAudioSource != null)
        {
            loopAudioSource.loop = true;
            loopAudioSource.playOnAwake = false;
        }

        if (oneShotAudioSource != null)
        {
            oneShotAudioSource.loop = false;
            oneShotAudioSource.playOnAwake = false;
        }

        // ループ音を自動再生
        if (loopAudioSource != null && loopClip != null)
        {
            loopAudioSource.loop = true;
            loopAudioSource.clip = loopClip;
            loopAudioSource.Play();
            Debug.Log("Start: ループ音を自動再生します");
        }
        else
        {
            Debug.LogWarning("Start: loopAudioSource または loopClip が設定されていません");
        }
    }

    public void OnOkButtonClicked()
    {
        if (loopClip != null && loopAudioSource != null)
        {
            if (!loopAudioSource.isPlaying)
            {
                loopAudioSource.clip = loopClip;
                loopAudioSource.loop = true;
                loopAudioSource.Play();
                Debug.Log("OKボタン：ループ音を再生します");
            }
            else
            {
                Debug.Log("OKボタン：ループ音はすでに再生中です");
            }
        }
        else
        {
            Debug.LogWarning("OKボタン：loopClip または loopAudioSource が設定されていません");
        }
    }


    public void StopLoopSound()
    {
        // ループ音の停止
        if (loopAudioSource != null && loopAudioSource.isPlaying)
        {
            // loopAudioSource.Stop();
            Debug.Log("ループ音を停止しました");
        }
    }

    // 効果音を再生
    public void OnStartButtonClicked()
    {
        if (oneShotClip != null && oneShotAudioSource != null)
        {
            if (oneShotAudioSource.isPlaying)
            {
                oneShotAudioSource.Stop(); // ← すでに再生中なら止めてから再再生
            }

            oneShotAudioSource.clip = oneShotClip;
            oneShotAudioSource.Play();
            Debug.Log("Start ボタン：効果音を再生");
        }
        else
        {
            Debug.LogWarning("oneShotClip または oneShotAudioSource が設定されていません");
        }
    }

    public void OnStopButtonClicked()
    {
        Debug.Log("Stop ボタンがクリックされました");
        if (oneShotAudioSource != null)
        {
            Debug.Log("Stop ボタン：効果音を停止");
            if (oneShotAudioSource.isPlaying)
            {
                oneShotAudioSource.Stop();
                Debug.Log("Stop ボタン：効果音を停止");
            }
            else
            {
                Debug.Log("Stop ボタン：すでに停止中です");
            }
        }
    }

    public void TestClick()
    {
        Debug.Log("ボタンが反応しています");
    }

}
