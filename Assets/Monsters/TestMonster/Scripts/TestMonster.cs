using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class TestMonster : Monster
{
    private NavMeshAgent agent;
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackTime;
    [SerializeField] private int attackDamage;
    private bool isAttacking;
    public bool IsAttacking{
        get{return isAttacking;}
        private set{
            agent.isStopped = value;
            isAttacking = value;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

    private void Awake() {agent = GetComponent<NavMeshAgent>();}

    private void Start() {Updater.Instance.onUpdate += FrameUpdate;}

    private void FrameUpdate() {
        if(isAttacking) return;
        if(Vector3.Distance(transform.position, Target.position) < attackDistance)
        {
            StartCoroutine(nameof(Attack));
            return;
        }
        if(agent.remainingDistance < agent.stoppingDistance) agent.SetDestination(Target.position);
    }

    private IEnumerator Attack() {
        IsAttacking = true;
        yield return new WaitForSeconds(attackTime);
        if(Vector3.Distance(transform.position, Target.position) > attackDistance)
        {
            IsAttacking = false;
            yield break;
        }
        else if(Target.TryGetComponent(out Damageable damageable)) damageable.Damage(attackDamage);
        IsAttacking = false;
    }
}
