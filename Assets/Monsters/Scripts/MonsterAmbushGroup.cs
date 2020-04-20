using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MonsterAmbushGroup : MonoBehaviour
{
    private Collider coll;
    [SerializeField] private Transform[] targets;
    [SerializeField] private bool overrideTargets;
    [SerializeField] private Monster[] members;

    private void Awake() {
        coll = GetComponent<Collider>();
        foreach (Monster monster in members)
        {
            monster.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(overrideTargets)
        {
            foreach (Monster monster in members)
            {
                if(monster == null) continue;
                monster.Target = targets[Random.Range(0, targets.Length)];
                monster.enabled = true;
            }
        }
        else
        {
            foreach (Monster monster in members)
            {
                if(monster == null) continue;
                monster.enabled = true;
            }
        }
        coll.enabled = false;
        this.enabled = false;
    }
}
