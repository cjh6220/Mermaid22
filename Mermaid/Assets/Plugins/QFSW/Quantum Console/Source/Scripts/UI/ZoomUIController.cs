using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QFSW.QC.UI
{
    [ExecuteInEditMode]
    public class ZoomUIController : MonoBehaviour
    {
        [SerializeField] private float _zoomIncrement = 0.1f;
        [SerializeField] private float _minZoom = 0.1f;
        [SerializeField] private float _maxZoom = 2f;

        [SerializeField] private Button _zoomDownBtn = null;
        [SerializeField] private Button _zoomUpBtn = null;

        [SerializeField] private DynamicCanvasScaler _scaler = null;
        [SerializeField] private TextMeshProUGUI _text = null;

        [SerializeField] private TextMeshProUGUI _logText = null;

        private float _lastZoom = -1;

        float _zoomMagnification = 1.0f;
        float _baseFontSize = 0.0f;

        private void Awake()
        {
            _baseFontSize = _logText.fontSize;
        }

        private float ClampAndSnapZoom(float zoom)
        {
            float clampedZoom = Mathf.Min(_maxZoom, Mathf.Max(_minZoom, zoom));
            float snappedZoom = Mathf.Round(clampedZoom / _zoomIncrement) * _zoomIncrement;
            return snappedZoom;
        }

        public void ZoomUp()
        {
            _zoomMagnification = ClampAndSnapZoom(_zoomMagnification + _zoomIncrement);
            //_scaler.ZoomMagnification = ClampAndSnapZoom(_scaler.ZoomMagnification + _zoomIncrement);

            _logText.fontSize = _baseFontSize * _zoomMagnification;

            _text.text = $"{Mathf.RoundToInt(100 * _zoomMagnification)}%";
        }

        public void ZoomDown()
        {
            _zoomMagnification = ClampAndSnapZoom(_zoomMagnification - _zoomIncrement);
            //_scaler.ZoomMagnification = ClampAndSnapZoom(_scaler.ZoomMagnification - _zoomIncrement);

            _logText.fontSize = _baseFontSize * _zoomMagnification;

            _text.text = $"{Mathf.RoundToInt(100 * _zoomMagnification)}%";
        }

        private void LateUpdate()
        {
            if (_scaler && _text)
            {
                float zoom = _zoomMagnification;
                if (zoom != _lastZoom)
                {
                    _lastZoom = zoom;

                    int percentage = Mathf.RoundToInt(100 * zoom);
                    _text.text = $"{percentage}%";
                }
            }

            if (_zoomDownBtn)
            {
                _zoomDownBtn.interactable = _lastZoom > _minZoom;
            }

            if (_zoomUpBtn)
            {
                _zoomUpBtn.interactable = _lastZoom < _maxZoom;
            }
        }
    }
}
