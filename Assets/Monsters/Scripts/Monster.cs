using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    public Transform Target{
        get{return target;}
        set{target = value;}
    }
}
