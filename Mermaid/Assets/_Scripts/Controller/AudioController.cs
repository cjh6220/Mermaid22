using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AudioController : MessageListener
{
    public enum Type_Audio
    {
        FX,
        BGM,
    }

    const float BaseVolum_BGM = 0.3f;
    const float BaseVolum_FX = 0.7f;

    const float BGM_MaxPitch = 1.0f;
    const float OneVoiceDelay = 0.3f;

    const string AudioSourceObjectName = "AudioSource";
    const string SoundDirectory = "Audio/";
    const int SOUND_PLAY_LIMIT_COUNT = 50;

    string _currentBGM = String_Audio.none;

    AudioSource _bgmClip;
    ObjectSpawnPool _spawnPool;

    Dictionary<string, AudioClip> _audioDic = new Dictionary<string, AudioClip>();

    Dictionary<string, AudioClip> _audioVoiceDic = new Dictionary<string, AudioClip>();

    AudioSource _speechClip = null;

    protected override void AwakeImpl()
    {
        base.AwakeImpl(); 

        gameObject.AddComponent<AudioSource>();

        _bgmClip = GetComponent<AudioSource>();
        _spawnPool = GetComponent<ObjectSpawnPool>();

        var audioSource = ResourceLoad.GetObject(AudioSourceObjectName);
        if (null != audioSource)
        {
            _spawnPool.AddObject(AudioSourceObjectName, audioSource);
        }
    }

    protected override void AddMessageListener()
    {
        AddListener(MessageID.Call_Audio_PlayFX);
        AddListener(MessageID.Call_Audio_PlayBGM);
        AddListener(MessageID.Call_Audio_PlaySkinSpeech);
        AddListener(MessageID.Call_Audio_PlaySkinSpeech_Enemy);
        AddListener(MessageID.Request_Speech_AudioResource);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {

            case MessageID.Call_Audio_PlayFX:
                PlayFX(data.ToString(), false);
                break;

            case MessageID.Call_Audio_PlayBGM:
                PlayBGM(data.ToString());
                break;
            case MessageID.Call_Audio_PlaySkinSpeech:
                PlaySpeech(data.ToString(),true);
            break;
            case MessageID.Call_Audio_PlaySkinSpeech_Enemy:
                PlaySpeech(data.ToString(),false);
            break;
            case MessageID.Request_Speech_AudioResource:
                {
                    var callback = data as Action<AudioSource>;
                    callback(_speechClip);
                }
                break;
        }
    }

    void PreLoadAudio(string audio)
    {
        if (true == _audioDic.ContainsKey(audio))
        {
            return;
        }

        AudioClip audioClip = null;

        audioClip = ResourceLoad.GetAudio(audio);

        if (null != audioClip)
        {
            _audioDic.Add(audio, audioClip);
        }
    }


    void PreLoadSpeechAudio(string audio)
    {
        if (true == _audioVoiceDic.ContainsKey(audio))
        {
            return;
        }

        AudioClip audioClip = null;

        audioClip = ResourceLoad.GetSkinSpeech_Audio(audio);

        if (null != audioClip)
        {
            _audioVoiceDic.Add(audio, audioClip);
        }
    }

    float PlayFX(string audio, bool loop, float volume = 1.0f, float pitch = 1.0f, bool force = false, bool muteForce = false, bool runtimeLoad = true)
    {
        float length = 0.0f;

        if (string.IsNullOrEmpty(audio))
        {
            return length;
        }

        if (SOUND_PLAY_LIMIT_COUNT >= _spawnPool.SpawnCount || true == force)
        {
            AudioClip fxClip = null;
            if (false == _audioDic.TryGetValue(audio, out fxClip))
            {
                PreLoadAudio(audio);

                if (false == _audioDic.TryGetValue(audio, out fxClip))
                {
                    LogManager.LogError("PlaySoundFX - Not loaded Runtime:" + audio);
                }
            }

            var audioSourceObject = _spawnPool.Spawn(AudioSourceObjectName);
            var audioSource = audioSourceObject.GetComponent<AudioSource>();
            if (null != audioSource)
            {
                audioSource.PlayOneShot(fxClip);
                audioSource.volume = BaseVolum_FX;

                length = fxClip.length;

                _spawnPool.Despawn(audioSourceObject, length);
            }
        }

        return length;
    }


    void PlayBGM(string bgm, bool muteForce = false, bool local = false)
    {
        if (false == _currentBGM.Equals(bgm))
        {
            _currentBGM = bgm;

            _bgmClip.Stop();

            if (String_Audio.none != bgm)
            {
                AudioClip bgmClip = null;
                if (false == _audioDic.TryGetValue(bgm, out bgmClip))
                {
                    PreLoadAudio(bgm);

                    _audioDic.TryGetValue(bgm, out bgmClip);

                    if (null == bgmClip)
                    {
                        LogManager.LogError("PlayBGM - Not Loaded:" + bgm);
                    }
                }

                if (null != bgmClip)
                {
                    _bgmClip.clip = bgmClip;
                    _bgmClip.loop = true;

                    _bgmClip.Play();
                    _bgmClip.volume = BaseVolum_BGM;
                }
            }
        }
    }

    void PlaySpeech(string audio, bool ismine ,float volume = 1.0f, float pitch = 1.0f, bool force = false, bool muteForce = false, bool runtimeLoad = true)
    {
        if(ismine == true)
        {
            if (_speechClip == null)
            {
                _speechClip = gameObject.AddComponent<AudioSource>();
            }
            else
            {
                _speechClip.Stop();
            }
            AudioClip speechClip = null;
            if (false == _audioVoiceDic.TryGetValue(audio, out speechClip))
            {
                PreLoadSpeechAudio(audio);

                _audioVoiceDic.TryGetValue(audio, out speechClip);

                if (null == speechClip)
                {
                    LogManager.LogError("PlaySpeech - Not Loaded:" + audio);
                }
            }

            if (speechClip != null)
            {
                _speechClip.clip = speechClip;
                _speechClip.loop = false;

                _speechClip.Play();
                _speechClip.volume = BaseVolum_FX;
            }
        }
        else
        {
            SendMessage<AudioSource>(MessageID.Request_Enemy_SpeechAudioSource,
            (audiocompo)=>
            {
                audiocompo.Stop();

                AudioClip speechClip = null;
                if (false == _audioVoiceDic.TryGetValue(audio, out speechClip))
                {
                    PreLoadSpeechAudio(audio);

                    _audioVoiceDic.TryGetValue(audio, out speechClip);

                    if (null == speechClip)
                    {
                        LogManager.LogError("PlaySpeech - Not Loaded:" + audio);
                    }
                }

                if (speechClip != null)
                {
                    audiocompo.clip = speechClip;
                    audiocompo.loop = false;

                    audiocompo.Play();
                    audiocompo.volume = BaseVolum_FX;
                }
            });
        }
    }

    void PlayBGM()
    {
        _bgmClip.Play();
    }

    void UnPauseBGM()
    {
        _bgmClip.UnPause();
    }

    void PauseBGM()
    {
        _bgmClip.Pause();
    }

    void StopBGM()
    {
        _currentBGM = String_Audio.none;
        _bgmClip.Stop();
    }
}