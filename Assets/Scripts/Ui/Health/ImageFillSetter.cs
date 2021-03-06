using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFillSetter : MonoBehaviour
    {
        [Tooltip("Value to use as the current ")]
        public FloatReference Variable;

        [Tooltip("Min value that Variable to have no fill on Image.")]
        public FloatReference Min;

        [Tooltip("Max value that Variable can be to fill Image.")]
        public FloatReference Max;

        [Tooltip("Image to set the fill amount on." )]
        public Image healthFill;
        public Image healthHeart;

        private void Update()
        {
            healthFill.fillAmount = Mathf.Clamp01(
                Mathf.InverseLerp(Min, Max, Variable));

            healthHeart.fillAmount = Mathf.Clamp01(
                Mathf.InverseLerp(Min, Max, Variable));
        }
    }
