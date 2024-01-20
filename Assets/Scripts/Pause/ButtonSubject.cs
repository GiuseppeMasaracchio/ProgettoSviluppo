using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using System;

public class ButtonSubject : MonoBehaviour {


    [Header("Text animation parameters")]
    [SerializeField] private TMP_Text[] textList;
    [SerializeField, Range(0f, 1f)] float delayB4, delayBtw;
    private string content;

    private bool show = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Typewriter());
        InvokeRepeating("Flashing", .3f, 1f);

    }


    private void Update() {
        textList[0].alpha = show ? 200 : 0;
    }

    private void Flashing() {
        show = !show;
    }

    private IEnumerator Typewriter() {
        if (textList == null) yield break;


        //Copia tutti i campi testo in un array
           for(int i = 1; i < textList.Length; i++) {
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
