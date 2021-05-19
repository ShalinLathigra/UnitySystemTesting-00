using System.Data.Common;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    public class TMPTextScroll : MonoBehaviour
    {
        private AnimationCurve displacement => DialogueEngine.d.displacement;
        private AnimationCurve alpha => DialogueEngine.d.alpha;
        private AnimationCurve words => DialogueEngine.d.words;
        private float minDuration => DialogueEngine.d.minDuration;

        private TextMeshPro _tmp;
        private float _startTime;
        private bool _active;
        private float _baseHeight;
        private float _duration;

        private void Start()
        {
            _active = false;
            _tmp = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
            Reset();

            _duration = Mathf.Max(displacement.keys[displacement.keys.Length - 1].time, minDuration);
        }
        private void Update()
        {
            if (!_active) return;
            var a = (byte) (alpha.Evaluate(Time.time - _startTime) * 255.0f);

            _tmp.faceColor = new Color32(
                    _tmp.faceColor.r, 
                    _tmp.faceColor.g, 
                    _tmp.faceColor.b,
                    a
                );
            _tmp.outlineColor = new Color32(
                _tmp.outlineColor.r,
                _tmp.outlineColor.g,
                _tmp.outlineColor.b,
                a
                );
            transform.localPosition = new Vector3(0, _baseHeight + displacement.Evaluate(Time.time - _startTime), 0);
            
            var t = (Time.time - _startTime) / _tmp.preferredHeight;
            _tmp.maxVisibleCharacters = (int) (words.Evaluate(t) * _tmp.text.Length); 
            _active = Time.time - _startTime < _duration;
        }

        public void Reset()
        {
            _startTime = Time.time;
            _tmp.faceColor = new Color32(
                _tmp.faceColor.r,
                _tmp.faceColor.g,
                _tmp.faceColor.b,
                255);
            _tmp.outlineColor = new Color32(
                _tmp.outlineColor.r,
                _tmp.outlineColor.g,
                _tmp.outlineColor.b,
                255);

            _active = true;

            var pv = _tmp.GetPreferredValues();
            _baseHeight = 1.0f + 0.5f * pv.y;
            var r = GetComponent<RectTransform>();
            _tmp.maxVisibleCharacters = 0;
        }
        public void ResetAndPlay(string str)
        {
            _tmp.text = str;
            Reset();
        }
    }
}
