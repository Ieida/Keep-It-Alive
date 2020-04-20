using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private InputAction input;
    [SerializeField] private Vector3 areaPosition;
    [SerializeField] private Vector3 areaSize;
    [SerializeField] private LayerMask enemyLayers;
    [Space]
    [SerializeField] private float attackTime;
    private float attackTimer;
    [SerializeField] private int maxEnemies;
    [SerializeField] private int damage;
    [Space]
    [SerializeField] private Animator animator;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position+transform.rotation*areaPosition, transform.rotation*areaSize*2.0f);
    }

    private void OnEnable() {input.Enable();}

    private void Start() {
        input.performed += context => {
            if(Time.time < attackTimer) return;
            else attackTimer = Time.time+attackTime;
            animator.SetTrigger("attack");
            Collider[] colliders = Physics.OverlapBox(
                transform.position+transform.rotation*areaPosition
                , areaSize
                , transform.rotation
                , enemyLayers
                , QueryTriggerInteraction.Collide
            );
            if(colliders.Length < 1) return;
            for (int i = 0; i < (colliders.Length > maxEnemies?maxEnemies:colliders.Length); i++)
            {
                if(colliders[i].TryGetComponent(out Damageable damageable)) damageable.Damage(damage);
            }
        };
    }

    private void OnDisable() {input.Disable();}
}
