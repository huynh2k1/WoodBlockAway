using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace H_Utils
{
    public class BaseUICtrl : MonoBehaviour
    {
        public PanelBase[] _arrUI;
        protected Dictionary<UIType, PanelBase> _uis = new Dictionary<UIType, PanelBase>();

        protected virtual void Awake()
        {

            foreach(var ui in _arrUI)
            {
                _uis[ui.Type] = ui; 
            }
        }

        public void Show(UIType type)
        {
            if (!_uis.ContainsKey(type))
            {
                Debug.LogError($"UI {type} is not found!!!");
                return;
            }
            _uis[type].Show();
        }

        public void Hide(UIType type)
        {
            if (!_uis.ContainsKey(type))
            {
                Debug.LogError($"UI {type} is not found!!!");
                return;
            }
            _uis[type].Hide();
        }
    }
}

