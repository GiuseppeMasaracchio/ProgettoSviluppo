using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor.ShortcutManagement;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Utilities;

public class EnemyScript : MonoBehaviour
{
    //References
    private NavMeshAgent navMesh;
    private Rigidbody rb;
    private Collider pg;

    //Variabili
    private bool canAttack = true;
    private bool isAttacking = false;
    private bool isJumping = false;
    
    private void Awake() {
        navMesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }



    private void Update() {
        FindPlayer();
        if (pg != null) {
            if(!isJumping) FollowPlayer(pg);
            //EnemyJump(pg);
        }
    }

    void FollowPlayer(Collider pgCol) {
        navMesh.SetDestination(pgCol.transform.position);
    }

    private void ResetCd() {
        canAttack = true;

        isAttacking = false;
    }

    public void EnemyJump(Collider pgCol) {
        if(Vector3.Distance(transform.position, pgCol.transform.position) <= 2f && !isJumping) {
            navMesh.enabled = false;

            isJumping = true;

            Vector3 dir = (pgCol.transform.position - transform.position);

            rb.velocity = Vector3.zero;
            StartCoroutine(Jump(dir));

            Invoke(nameof(ResetJump), 1.6f);
        }
    }

    private IEnumerator Jump(Vector3 dir) {
        rb.AddForce(Vector3.up * 25f, ForceMode.Impulse);
        yield return new WaitForSeconds(2);
        rb.AddForce(dir * 15f, ForceMode.Impulse);
    }

    private void ResetJump() {
        isJumping = false;

        StopAllCoroutines();

        navMesh.enabled = true;
    }

    //trova player
    private void FindPlayer() {
        Collider[] temp = Physics.OverlapSphere(transform.position, 15f, LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);
        for(int i = 0; i < temp.Length; i++) {
            if (temp[i].CompareTag("Player")) pg = temp[i];
        }
    }

    //Provvisorio
    private void OnCollisionEnter(Collision collision) {
        if(collision.collider == pg && canAttack && !isAttacking) {
            canAttack = false;
            isAttacking = true;

            collision.collider.GetComponentInChildren<CombatModuleScript>().TakeDmg(1);

            Invoke(nameof(ResetCd), 1f);
        }

    }
}
