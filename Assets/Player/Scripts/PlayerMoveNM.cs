using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMoveNM : MonoBehaviour
{
    [SerializeField] private Camera orientator = null;
    public Camera Orientator{
        get{return orientator;}
        set{orientator = value;}
    }
    [SerializeField] private InputAction pressInput;
    [SerializeField] private InputAction positionInput;
    private NavMeshAgent navMeshAgent;

    private void Awake() {
        if(orientator == null) Orientator = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable() {
        pressInput.Enable();
        positionInput.Enable();
    }

    private void Start() {
        pressInput.performed += context => {
            Ray ray = orientator.ScreenPointToRay(positionInput.ReadValue<Vector2>());
            if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f)) navMeshAgent.SetDestination(hit.point);
        };
    }

    private void OnDisable() {
        pressInput.Disable();
        positionInput.Disable();
    }
}
