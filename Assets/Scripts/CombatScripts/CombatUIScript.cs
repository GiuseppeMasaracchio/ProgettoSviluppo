using UnityEngine;
using UnityEngine.UI;

//restart
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class CombatUIScript : MonoBehaviour 
{
    [SerializeField] Transform camTransform;
    [SerializeField] Slider healthbar;
    private CombatModuleScript cms;

    private Canvas personalCanvas;

    private void Awake() {
        healthbar = GetComponent<Slider>();
        cms = GetComponentInParent<CombatModuleScript>();
        personalCanvas = GetComponentInParent<Canvas>();
    }


    void Start() {
        if (healthbar == null) return;
        healthbar.maxValue = cms.maxHp;

        HideHealthbar();
    }

    void Update() {
        TurnToCam();

        ShowHealthbar();
    }
    private void ShowHealthbar() {
        if (healthbar == null) return;
        if(cms.currentHp != healthbar.value) {
            personalCanvas.enabled = true;
            healthbar.value = cms.currentHp;
            Invoke(nameof(HideHealthbar), 1.5f);
        }
    }
    void HideHealthbar() {
        personalCanvas.enabled = false;
    }
    
    private void TurnToCam() {
        transform.rotation = Quaternion.LookRotation(transform.position - camTransform.position);
    }


}
