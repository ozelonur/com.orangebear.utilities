using System;
using System.Collections;
using TMPro;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace OrangeBear.Utilities
{
    public class TextIndicator : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Configurations")] [SerializeField]
        private float upSpeed;

        [SerializeField] private Color defaultColor;
        [SerializeField] private Color positiveColor;
        [SerializeField] private Color negativeColor;

        [SerializeField] private float fadeDuration = .3f;

        [Header("References")] [SerializeField]
        private TMP_Text valueText;

        #endregion

        #region Private Variables

        private readonly Transform[] _transforms = new Transform[1];
        private TransformAccessArray _transformAccessArray;

        private bool _canMove;

        #endregion

        #region MonoBehaviour Methods

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }
            TextIndicatorJob job = new TextIndicatorJob
            {
                UpSpeed = upSpeed,
                DeltaTime = Time.deltaTime
            };

            JobHandle handle = job.Schedule(_transformAccessArray);

            handle.Complete();
        }

        #endregion

        #region Public Methods

        public void Init(float value, bool useDefaultColor = true, bool isPositive = true, Action onComplete = null)
        {
            _transforms[0] = transform;
            _transformAccessArray = new TransformAccessArray(_transforms);

            valueText.text = isPositive ? $"+${value}" : $"-${value}";

            _canMove = true;

            valueText.color = useDefaultColor ? defaultColor : isPositive ? positiveColor : negativeColor;

            StartCoroutine(Fade(onComplete));

        }

        public void Reset()
        {
            _canMove = false;
            if (_transformAccessArray.isCreated)
            {
                _transformAccessArray.Dispose();
            }
        }

        #endregion

        #region Private Methods

        private IEnumerator Fade(Action callBack = null)
        {
            float elapsedTime = 0;

            while (elapsedTime < fadeDuration)
            {
                valueText.color = Color.Lerp(valueText.color,
                    new Color(valueText.color.r, valueText.color.g, valueText.color.b, 0),
                    (elapsedTime / fadeDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            callBack?.Invoke();
        }

        #endregion
    }
}