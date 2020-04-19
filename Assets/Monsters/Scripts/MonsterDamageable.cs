using UnityEngine;

public class MonsterDamageable : Damageable
{
    [SerializeField] protected int health;
    public int Health{
        get{return health;}
    }

    public override void Damage(int damage = 0) {
        health -= damage;
        if(health == 0) Destroy(gameObject);
    }

    private void OnDestroy() {Debug.Log("defeated "+gameObject.name);}
}
