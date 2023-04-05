using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool canAttack = false;
    public void Attack() {
        canAttack = true;
        Invoke(nameof(ResetCd), 1.5f);
    }

    private void ResetCd() {
        canAttack = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (canAttack) {
            var foe = other.GetComponentInChildren<CombatModuleScript>();
            if(foe != null)
                foe.TakeDmg(1);
        }
    }

}
