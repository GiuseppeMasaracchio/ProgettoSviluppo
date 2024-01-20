using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] float sizeMult;
    [SerializeField] float animTime;

    private Vector3 startScale, endScale;


    [SerializeField, Range(0f, 2f)] float delayBeforeStart, delayBtwChar;

    

    // Start is called before the first frame update
    void Awake()
    {
        startScale = transform.localScale;
    }


    private IEnumerator ButtonAnim(bool isStarted) {
        float timer = 0f;
        while (timer < animTime) {
            timer += Time.deltaTime;
            
            endScale = isStarted ? (startScale * sizeMult) : startScale;
            
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (timer / animTime));
            transform.localScale = lerpedScale;

            yield return null;
        }
    }

   

    public void OnSelect(BaseEventData eventData) {
        StartCoroutine(ButtonAnim(true));
    }

    public void OnDeselect(BaseEventData eventData) {
        StartCoroutine(ButtonAnim(false));
    }

    public void OnPointerEnter(PointerEventData eventData) {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData) {
        eventData.selectedObject = null;
    }

}
