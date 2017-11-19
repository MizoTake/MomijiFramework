// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UniRx;

// public class SoundPlayer : Singleton<SoundPlayer>
// {

//     private AudioSource[] _total;
//     private AudioSource _bgm;
//     private AudioSource[] _se;
//     private AudioSource[] _voice;

//     private SoundParameter _data;
//     public static SoundParameter Data
//     {
//         get
//         {
//             if (Instance._data == null)
//             {
//                 // セットアップ
//                 Instance._data = Scriptable.GetData(InfoKey.SCRIPTABLE_SOUND_PARAMETER) as SoundParameter;
//                 Instance._total = new AudioSource[1 + Instance._data.SEClip.Length + Instance._data.VoiceClip.Length];
//                 Instance._total[0] = Instance._bgm = Instance.gameObject.AddComponent<AudioSource>();
//                 Instance._bgm.loop = true;
//                 Instance._se = new AudioSource[Instance._data.SEClip.Length];
//                 Instance._voice = new AudioSource[Instance._data.VoiceClip.Length];
//                 for (int i = 0; i < Instance._data.SEClip.Length; i++)
//                 {
//                     Instance._total[i + 1] = Instance._se[i] = Instance.gameObject.AddComponent<AudioSource>();
//                 }
//                 for (int i = 0; i < Instance._data.VoiceClip.Length; i++)
//                 {
//                     Instance._total[i + 1] = Instance._voice[i] = Instance.gameObject.AddComponent<AudioSource>();
//                 }
//             }
//             return Instance._data;
//         }
//         private set
//         {
//             Instance._data = value;
//         }
//     }

//     private Subject<SoundVolume> _volume = new Subject<SoundVolume>();

//     /// <summary>
//     /// Volumeとかを動的に変えたい時に使用
//     /// </summary>
//     public static Subject<SoundVolume> OnChangeVolume { get { return Instance._volume; } }

//     void Awake()
//     {
//         // Volume等の値が流れるので反映
//         OnChangeVolume
//             .Subscribe((_) =>
//             {
//                 _total.ForEach((audio, i) =>
//                 {
//                     audio.mute = _.Mute;
//                     if (i == 0)
//                     {
//                         audio.volume = _.BGM;
//                     }
//                     else if (i < _se.Length)
//                     {
//                         audio.volume = _.SE;
//                     }
//                     else
//                     {
//                         audio.volume = _.Voice;
//                     }
//                 });
//             })
//             .AddTo(this);
//     }

//     /// <summary>
//     /// 添え字から音源を指定して再生
//     /// </summary>
//     /// <param name="index">ScriptableObjectに設定したindex</param>
//     /// <param name="sType">SoundTypeより、どれなのか指定</param>
//     public static void Play(int index, SoundType sType)
//     {
//         switch (sType)
//         {
//             case SoundType.BGM:
//                 Instance.BGMPlay(index);
//                 break;
//             case SoundType.SE:
//                 Instance.SEPlay(index);
//                 break;
//             case SoundType.Voice:
//                 Instance.VoicePlay(index);
//                 break;
//         }
//     }

//     private void BGMPlay(int index)
//     {
//         if (index < 0 || index >= Data.BGMClip.Length)
//         {
//             return;
//         }
//         if (_bgm.clip == Data.BGMClip[index])
//         {
//             return;
//         }
//         _bgm.Stop();
//         _bgm.clip = Data.BGMClip[index];
//         _bgm.Play();
//     }

//     private void SEPlay(int index)
//     {
//         if (index < 0 || index >= Data.SEClip.Length)
//         {
//             return;
//         }
//         _se.ForEach((_) =>
//         {
//             if (!_.isPlaying)
//             {
//                 _.clip = Data.SEClip[index];
//                 _.Play();
//                 return;
//             }
//         });
//     }

//     private void VoicePlay(int index)
//     {
//         if (index < 0 || index >= Data.VoiceClip.Length)
//         {
//             return;
//         }
//         _se.ForEach((_) =>
//         {
//             if (!_.isPlaying)
//             {
//                 _.clip = Data.VoiceClip[index];
//                 _.Play();
//                 return;
//             }
//         });
//     }

// }

// public enum SoundType
// {
//     BGM,
//     SE,
//     Voice
// }
