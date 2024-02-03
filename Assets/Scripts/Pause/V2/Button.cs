using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 startSize, endSize;
    [Header("Size animation params")]
    [SerializeField] float animTime = 0.1f, sizeMult = 0.3f;

    [Header("Typewriter anim params")]
    [SerializeField] private float delayB4 = 0f, delayBtwChar = 0.05f;
    private TMP_Text _buttonText;

    public void OnSelect(BaseEventData eventData) {
        eventData.selectedObject = gameObject;
    }

    public void OnDeselect(BaseEventData eventData) {
        eventData.selectedObject = null;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        StartCoroutine(ButtonAnim(true));
    }

    public void OnPointerExit(PointerEventData eventData) {
        StartCoroutine(ButtonAnim(false));
    }

    // Start is called before the first frame update
    private void Start() {
        _buttonText = GetComponentInChildren<TMP_Text>();
    }

    void OnEnable()
    {
        startSize = transform.localScale;

        _buttonText?.StartCoroutine(TypewriterAnim());
    }

  
    private IEnumerator ButtonAnim(bool isStarting) {

        float timeSinceStart = 0f;

        while(timeSinceStart < animTime) {
            timeSinceStart += Time.deltaTime;

            endSize = isStarting ? (startSize * sizeMult) : startSize; 

            transform.localScale = Vector3.Lerp(transform.localScale, endSize, (timeSinceStart/ animTime));

            yield return null;
        }
    }

    private IEnumerator TypewriterAnim() {
        yield return new WaitForSeconds(delayB4);

        string content = _buttonText.text;
        _buttonText.text = "";

        foreach(char c in content) {
            yield return new WaitForSeconds(delayBtwChar);
            _buttonText.text += c;
        }
    }

    public void Quit() {
        Application.Quit();
    }

    public void Continue() {
        Time.timeScale = 1f;
        //camera switch

    }
}
