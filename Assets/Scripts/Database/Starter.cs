using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Starter : MonoBehaviour {
    private List<object[]> data;
    private TMP_Text text;
    private object[] activedata;
    private object[] checkpoint;
    private object[] newcp;
    private Text test;



    private void Awake() {
        text = GameObject.Find("Highscore").GetComponent<TMP_Text>();
        
    }

    void Start() {
        text.text = null;

        //DBVault.InitCP();
        //DBVault.InitDB();
        //DBVault.InitHS();

        //DBVault.DeleteFromTableByIdx("Highscore", 1);
        //DBVault.DeleteFromTableByIdx("Highscore", 2);
        //DBVault.DeleteFromTableByIdx("Highscore", 3);
        //DBVault.DeleteFromTableByIdx("Highscore", 4);
        //DBVault.DeleteFromTableByIdx("Highscore", 5);

        //DBVault.UpdateValueByIdx(1, "Slot", "Runtime", 0);
        //DBVault.UpdateValueByIdx(2, "Slot", "Runtime", 1);

        //Debug.Log(DBVault.GetActiveData("Slot", "name"));
        //Debug.Log(DBVault.GetHighscoreCount());

        //DBVault.SetHighscore("Pippo", 2);
        //DBVault.SetHighscore("Pazzo", 15);
        //DBVault.SetHighscore("Puzzo", 10);
        //DBVault.SetHighscore("Pezzo", 9);
        //DBVault.SetHighscore("Pazzo", 12);

        //DBVault.InsertValue("Checkpoint", "Level_idx", 1);

        //DBVault.SetHighscore("Lowtest", 3);

        //DBVault.DeleteFromTable("Highscore");

        /*
        data = DBVault.GetHighscore();

        for (int i = 0; i < DBVault.GetHighscoreCount(); i++) {
            text.text += (i+1) + ". " + data[i][1] + ": " + data[i][2] + "\n";
        }
        */

        //object[] newvalue = new object[] { "Dev_1", 0, 0, 3};
        //DBVault.UpdateActiveSlot("Powerups", 1);
        //DBVault.UpdateActiveSlot(newvalue);

        //DBVault.ReBuildDB();

        //DBVault.ResetDB();

        //DBVault.DisposeActiveSlot();

        //DBVault.UpdateActiveSlot("Powerups", 1);

        //DBVault.SetActiveSlot(1);
        /*
        activedata = DBVault.GetActiveData();
        
        if (activedata[0] != null) {
            text.text = activedata[0] + " - " + activedata[1] + " - " + activedata[2] + " - " + activedata[3] + " - " + activedata[4] + " - " + activedata[5];
        }
        */

        /*
        newcp = new object[] { 1, 2};
        DBVault.SetCheckpoint(newcp);
        
        checkpoint = DBVault.GetActiveCheckpoint();

        if (checkpoint[0] != null) {
            text.text = checkpoint[0] + " - " + checkpoint[1] + " - " + checkpoint[2];
        }
        */
    }
}
