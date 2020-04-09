using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using UnityEngine.UI;
using TMPro;
using System;

namespace garagekitgames
{
    public class UpdateSliderValuePercentage : MonoBehaviour
    {

        public IntVariable currentValue;
        public IntVariable maxValue;

        public IntVariable Percentage;

        public Slider slider;
        // Start is called before the first frame update
        void Awake()
        {
            slider = this.gameObject.GetComponent<Slider>();
            slider.value = ((float)currentValue.value / (float)maxValue.value) * 100.0f;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateSliderInt();

        }

        public void UpdateSliderInt()
        {
            if (!slider)
                return;

            slider.value = ((float)currentValue.value / (float)maxValue.value) * 100.0f;

            Percentage.value = Mathf.RoundToInt(((float)currentValue.value / (float)maxValue.value) * 100.0f);
        }
    }
}

