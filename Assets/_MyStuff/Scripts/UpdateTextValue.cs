using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using UnityEngine.UI;
using TMPro;

namespace garagekitgames
{
    public class UpdateTextValue : MonoBehaviour
    {

        public IntVariable inputValue;
        public TextMeshProUGUI outputText;

        
        // Use this for initialization

        void Awake()
        {

            outputText = this.gameObject.GetComponent<TextMeshProUGUI>();
            outputText.text = inputValue.value.ToString();

        }

        // Update is called once per frame
        void Update()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            outputText.text = inputValue.value.ToString();
        }

    }
}

