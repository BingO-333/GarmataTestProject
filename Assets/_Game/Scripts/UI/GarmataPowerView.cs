using System;
using TMPro;
using UnityEngine;

namespace Game
{
    public class GarmataPowerView : MonoBehaviour
    {
        [SerializeField] GarmataShooter _garmata;
        [SerializeField] TextMeshProUGUI _powerDisplay;

        private void Awake()
        {
            _garmata.OnPowerChanged += UpdatePowerDisplay;
        }

        private void Start()
        {
            UpdatePowerDisplay();
        }

        private void UpdatePowerDisplay()
        {
            _powerDisplay.text = $"Power: {(int)_garmata.CurrentShootPower}";
        }
    }
}