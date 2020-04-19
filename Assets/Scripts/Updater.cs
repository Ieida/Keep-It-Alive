using UnityEngine;

[DisallowMultipleComponent]
public class Updater : MonoBehaviour
{
    public static Updater Instance{get;private set;}
    public delegate void UpdateDelegate();
    public UpdateDelegate onUpdate;
    [SerializeField] private float updateTime = 0.33f;
    public float UpdateTime{
        get{return updateTime;}
        set{updateTime = value;}
    }

    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start() {InvokeRepeating(nameof(FrameUpdate), updateTime, updateTime);}

    private void FrameUpdate() {if(onUpdate != null) onUpdate();}
}
