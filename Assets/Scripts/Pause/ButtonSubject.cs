using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class ButtonSubject : MonoBehaviour {
    [Header("Text animation parameters")]
    [SerializeField] private TMP_Text[] textList;
    [SerializeField, Range(0f, 1f)] float delayB4, delayBtw = 0.05f;
    private string content;


    private bool show = false;
    private bool isModal;

    private void Awake() {
        isModal = CompareTag("Modal");
    }

    void OnEnable() //Chiamata ogni volta che un oggetto viene attivato nella gerarchia
    {
        StartCoroutine(Typewriter());
        if(!isModal)    InvokeRepeating("Flashing", .3f, 1f);

    }


    private void Update() {
       if(!isModal) textList[0].alpha = show ? 200 : 0;
    }

    private void Flashing() {
        show = !show;
    }

    private IEnumerator Typewriter() {
        if (textList == null) yield break;

       
        //Copia tutti i campi testo in un array
           for (int i = isModal ? 0 : 1; i < textList.Length; i++) {
                content = "";
                content = textList[i].text;
                textList[i].text = "";

                //Scrive le lettere una ad una
                yield return new WaitForSeconds(delayB4);
                foreach (char c in content) {
                    textList[i].text += c;
                    yield return new WaitForSeconds(delayBtw);
                }

            }
    }

}
