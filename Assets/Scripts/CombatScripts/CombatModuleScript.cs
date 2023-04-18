using UnityEngine.AI;
using UnityEngine;

public class CombatModuleScript : MonoBehaviour, ICombat
{
    public int maxHp = 3, currentHp;
    [SerializeField] Rigidbody rb;

    private void Awake() {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void Start() {
        currentHp = maxHp;
    }

   
    public void TakeDmg(int dmg) {
        currentHp -= dmg;
        if (currentHp <= 0) Death();

        if (transform.CompareTag("Enemy")) {
            GetComponentInParent<NavMeshAgent>().enabled = false;
            Invoke(nameof(EnableNav), 1f);
        }
        rb.AddExplosionForce(10f, transform.position + Vector3.forward, 5f, 5f, ForceMode.VelocityChange);
    }

    private void EnableNav() {
        GetComponentInParent<NavMeshAgent>().enabled = true;
    }

    public void AddHp(int heal) {
        currentHp += heal;
    }

    public void Death() {
        if (transform.tag == "Player") {
            Time.timeScale = 0f;
            GameObject.Destroy(transform.parent.gameObject, 1f); //1s per far attivare le animazioni e puzzi
            GameMaster.Instance.Respawn();
            Time.timeScale = 1f;
        }

        GameObject.Destroy(transform.parent.gameObject, 0.7f); //1s per far attivare le animazioni e puzzi
    }



}
