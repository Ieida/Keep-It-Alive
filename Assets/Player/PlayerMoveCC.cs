using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoveCC : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private Quaternion orientation;
    [SerializeField] private InputAction input;
    [SerializeField] private float speed;
    [Space]
    [SerializeField] private float rotateSpeed;

    private void Awake() {controller = GetComponent<CharacterController>();}

    private void OnEnable() {input.Enable();}

    private void Update() {
        Vector2 inV = input.ReadValue<Vector2>();
        Vector3 finalV = orientation*new Vector3(inV.x, 0.0f, inV.y);
        controller.SimpleMove(finalV*speed);
        if(finalV == Vector3.zero) return;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation
            , Quaternion.LookRotation(finalV, transform.up)
            , rotateSpeed
        );
    }

    private void OnDisable() {input.Disable();}
}
