using UnityEngine;

[DisallowMultipleComponent]
public class Updater : MonoBehaviour
{
    public static Updater Instance{get;private set;}
    public delegate void UpdateDelegate();
    private static UpdateDelegate onUpdate;
    public static UpdateDelegate OnUpdate{
        get{return onUpdate;}
        set{
            if(Instance == null) return;
            if(value == null) Instance.CancelInvoke();
            else Instance.InvokeRepeating(nameof(FrameUpdate), UpdateTime, UpdateTime);
            onUpdate = value;
        }
    }
    [SerializeField] private float updateTime = 0.33f;
    public static float UpdateTime{
        get{return Instance.updateTime;}
        set{
            if(Instance == null) return;
            Instance.CancelInvoke();
            Instance.InvokeRepeating(nameof(FrameUpdate), value, value);
            Instance.updateTime = value;
        }
    }

    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(this);
    }

    private void FrameUpdate() {onUpdate();}
}
