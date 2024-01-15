using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ButtonSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
    [SerializeField] private float verticalAmount = 30f;
    [SerializeField] private float moveTime = 0.1f;
    [Range(0f, 2f), SerializeField] private float scaleAmount = 1.1f;

    private Vector3 startPos;
    private Vector3 startScale;


    private void Awake() {
        Cursor.lockState = CursorLockMode.None;
    }

    void Start() {
        startPos = transform.position;
        startScale = transform.localScale;
    }

    private IEnumerator MoveButton(bool startingAnimation) {
        Vector3 endingPos;
        Vector3 endingScale;

        float elapsedTime = 0f;
        while(elapsedTime < moveTime) {
            
            elapsedTime += Time.deltaTime;
            
            if (startingAnimation) {
                endingPos = startPos + new Vector3(0f, verticalAmount, 0f);
                endingScale = startScale * scaleAmount;
            } else {
                endingPos = startPos;
                endingScale = startScale;
            }

            //Se si move il cursore allora s'interrompe(?)
            Vector3 lerpedPos = Vector3.Lerp(transform.position, endingPos, (elapsedTime/ moveTime));
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endingScale, (elapsedTime / moveTime));

            //Applica i lerp
            transform.position = lerpedPos;
            transform.localScale = lerpedScale;

            //Aspetta finché non finisce tutte le iterazioni
            yield return null;
        }
    }

    //Seleziona l'oggetto al posto di attivare l'highlight
    public void OnPointerEnter(PointerEventData eventData) {
        eventData.selectedObject = gameObject;
        Debug.Log("Entrato nell'oggetto: " + gameObject.name);
    }


    public void OnPointerExit(PointerEventData eventData) {
        eventData.selectedObject = null;
        Debug.Log("Uscito dall'oggetto: " + gameObject.name);

    }



    public void OnSelect(BaseEventData eventData) {
        StartCoroutine(MoveButton(true));
        Debug.Log("Selezionato oggetto: " + gameObject.name);

    }

    public void OnDeselect(BaseEventData eventData) {
        StartCoroutine(MoveButton(false));
        Debug.Log("Deselezionato oggetto: " + gameObject.name);
    }
}
