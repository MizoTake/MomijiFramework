using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Momiji {
    public class SoundPlay : MonoBehaviour {

        [SerializeField]
        private bool _auto = true;
        [SerializeField]
        private SoundType _type;
        [SerializeField]
        private BGM _bgm;
        [SerializeField]
        private SE _se;
        [SerializeField]
        private Voice _voice;

        void Start () {
            if (_auto) Play ();
        }

        public void Play () {
            var result = -1;
            switch (_type) {
                case SoundType.BGM:
                    result = (int) _bgm;
                    break;
                case SoundType.SE:
                    result = (int) _se;
                    break;
                case SoundType.Voice:
                    result = (int) _voice;
                    break;
            }
            SoundPlayer.Play (result, _type);
        }

#if UNITY_EDITOR
        [CustomEditor (typeof (SoundPlay))]
        [CanEditMultipleObjects]
        public class CharaManagerEditor : Editor {

            SerializedProperty _auto;
            SerializedProperty _selectSoundType;
            SerializedProperty _bgm;
            SerializedProperty _se;
            SerializedProperty _voice;

            /// <summary>
            /// 初回処理
            /// </summary>
            void OnEnable () {
                _auto = serializedObject.FindProperty ("_auto");
                _selectSoundType = serializedObject.FindProperty ("_type");
                _bgm = serializedObject.FindProperty ("_bgm");
                _se = serializedObject.FindProperty ("_se");
                _voice = serializedObject.FindProperty ("_voice");
            }

            /// <summary>
            /// GUI表示処理
            /// </summary>
            public override void OnInspectorGUI () {
                serializedObject.Update ();

                _auto.boolValue = EditorGUILayout.Toggle ("Start Play", _auto.boolValue);

                EditorGUI.BeginChangeCheck ();
                _selectSoundType.enumValueIndex = (int) (SoundType) EditorGUILayout.EnumPopup ("Sound Type", (SoundType) _selectSoundType.enumValueIndex);
                EditorGUI.EndChangeCheck ();

                switch ((SoundType) _selectSoundType.enumValueIndex) {
                    case SoundType.BGM:
                    EditorGUILayout.PropertyField (_bgm, true);
                    break;
                    case SoundType.SE:
                    EditorGUILayout.PropertyField (_se, true);
                    break;
                    case SoundType.Voice:
                    EditorGUILayout.PropertyField (_voice, true);
                    break;
                }

                serializedObject.ApplyModifiedProperties ();
            }
        }
#endif
    }
}