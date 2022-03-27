using System;
using TMPro;
using UnityEngine;

namespace CryptoMandala
{
    public sealed class AddressSetter: MonoBehaviour
    {
        TMP_InputField _inputField;
        
        void Start()
        {
            _inputField = GetComponent<TMP_InputField>();
            _inputField.onEndEdit.AddListener(SetAddress);
            
            CheckAddress();
        }
        
        void CheckAddress()
        {
            if (!string.IsNullOrEmpty(LensProtocolSingleton.Instance.Address))
            {
                _inputField.text = LensProtocolSingleton.Instance.Address;
            }
        }

        void SetAddress(string address)
        {
            LensProtocolSingleton.Instance.SetAddress(address);
            LensProtocolSingleton.Instance.UpdateData();
        }
    }
}