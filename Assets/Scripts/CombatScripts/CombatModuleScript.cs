using UnityEngine.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update() {
        
        if (currentHp <= 0) Death();
        
    }


  
    public void TakeDmg(int dmg) {
        currentHp -= dmg;
        if (transform.CompareTag("Enemy")) {
            Debug.Log("Enemy ha preso danno");

            GetComponentInParent<NavMeshAgent>().enabled = false;

            Invoke(nameof(EnableNav), 1f);
        }
        rb.AddExplosionForce(5f, transform.position + Vector3.forward, 4f, 4f, ForceMode.Impulse);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }

        GameObject.Destroy(transform.parent.gameObject, 0.7f); //1s per far attivare le animazioni e puzzi
    }



}
