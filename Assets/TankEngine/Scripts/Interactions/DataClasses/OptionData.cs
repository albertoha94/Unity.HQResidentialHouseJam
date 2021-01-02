using System;
using UnityEngine.Events;

namespace TankEngine.Scripts.Interactions.DataClasses
{

    [Serializable]
    public class OptionData
    {
        public bool isSelected;
        public string conditionText;
        public UnityEvent eventToUse;
    }
}
