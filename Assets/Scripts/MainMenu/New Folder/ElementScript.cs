using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, ISubmitHandler{
    private TMP_Text label;
    private GroupHandler _parent;


    private Image _background;
    private Color tempColor;

    public Slider _slider;
    public TMP_Dropdown _dropdown;
    public Toggle _toggle;

    // Start is called before the first frame update
    void Start()
    {
        label = GetComponentInChildren<TMP_Text>();
        _parent = GetComponentInParent<GroupHandler>();

        _background = GetComponent<Image>();

        _slider = GetComponentInChildren<Slider>();
        _dropdown = GetComponentInChildren<TMP_Dropdown>();
        _toggle = GetComponentInChildren<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        
    }

    public void OnPointerClick(PointerEventData eventData) {
        //throw new System.NotImplementedException();
        tempColor.a = 1f;
        _background.color = tempColor;
        _parent.Dispatcher(this);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //throw new System.NotImplementedException();
        tempColor.a = 0.6f;
        _background.color = tempColor;
        
    }

    public void OnPointerExit(PointerEventData eventData) {
        //throw new System.NotImplementedException();
        tempColor.a = 0f;
        _background.color = tempColor;
    }

    public void OnDrag(PointerEventData eventData) {
        //throw new System.NotImplementedException();
        _parent.Dispatcher(this);
    }

    public void OnSubmit(BaseEventData eventData) {
        //throw new System.NotImplementedException();
        _parent.Dispatcher(this);
    }

}
