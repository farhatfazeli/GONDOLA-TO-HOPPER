using System;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View
{
    public class QRCodeHandler : MonoBehaviour
    {
        [SerializeField] private Image qrCodeImage;

        [Header("FadeIn Settings")]
        [SerializeField] private float fadeInTime = 0.2f;
        
        private bool _safety;
        
        private bool _isVisible;

        private void Start()
        {
            qrCodeImage.canvasRenderer.SetAlpha(0f);
        }

        private void Update()
        {
            // if (_safety)
            // {
            //     if (Input.GetKeyDown(KeyCode.Q))
            //     {
            //         _isVisible = !_isVisible;
            //         _safety = false;
            //         Debug.Log("Going to show QR Code");
            //         AudioManager.I.PlayTrainCrossing();
            //         float targetAlpha = _isVisible ? 1f : 0f;
            //         qrCodeImage.CrossFadeAlpha(targetAlpha, fadeInTime, true);
            //     }
            // }
            // else if (Input.GetKeyDown(KeyCode.Q))
            // {
            //     Debug.Log("Setting safety to true");
            //     _safety = true;
            // }
        }
    }
}