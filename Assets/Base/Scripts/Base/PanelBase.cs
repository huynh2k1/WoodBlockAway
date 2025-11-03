using UnityEngine;
    public abstract class PanelBase : MonoBehaviour
    {
        //UI kế thừa phải override lại UIType VD: public override UIType Type => UIType.Home;
        public abstract UIType Type { get; }    
        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide() => gameObject.SetActive(false);
    }
    public enum UIType
    {
        Game,
        Home,
        Win,
        HowToPlay,
        Setting,
        Shop,
        SelectLevel,
    }
