using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerPhysics : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private Vector3 gravity;
    public Vector3 Gravity{
        get{return gravity;}
        set{gravity = value;}
    }
    public Vector3 Velocity{get;set;}
    [SerializeField, Range(0.0f, 1.0f)] private float airResistance;
    public float AirResistance{
        get{return airResistance;}
        set{airResistance = Mathf.Clamp01(value);}
    }

    private void Awake() {controller = GetComponent<CharacterController>();}

    private void LateUpdate() {
        if(!controller.isGrounded) Velocity -= gravity*Time.deltaTime;
        Velocity *= airResistance;
        controller.Move(Velocity);
    }
}
