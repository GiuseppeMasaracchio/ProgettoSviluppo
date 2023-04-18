using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private bool canAttack, isAttacking;
    private StateSetter _stateSetter;

    private void Awake() {
        _stateSetter = GameObject.Find("ScriptsHolder").GetComponent<StateSetter>();
    }
    public void Attack() {
        _stateSetter.DetectAttack(true);
        Invoke(nameof(ResetCd), 0.7f);
    }

    private void ResetCd() {
        canAttack = true;
        isAttacking = false;
        _stateSetter.DetectAttack(false);
    }

    private void OnTriggerEnter(Collider other) {

        if (canAttack && !isAttacking) {
            isAttacking = true;
            canAttack = false;

            var foe = other.GetComponentInChildren<CombatModuleScript>();
            if(foe != null)
                foe.TakeDmg(1);
        }
    }

}
