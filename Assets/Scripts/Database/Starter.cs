using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Starter : MonoBehaviour {
    private List<object[]> data;
    private TMP_Text text;

    // Start is called before the first frame update
    void Start() {
        //DBVault.DropTable("Slot");
        //DBVault.DropTable("Highscore");
        //DBVault.DropTable("Checkpoint");
        //DBVault.InitDB();
        //DBVault.UpdateValueByIdx(2, "Slot", "Runtime", 0);

        text = GameObject.Find("Highscore").GetComponent<TMP_Text>();
        data = DBVault.GetHighscore();
        text.text = "Il primo classificato è: " + data[0][1] + ". Ed ha totalizzato " + data[0][2] + " Punti! \n";
        
    }
}
