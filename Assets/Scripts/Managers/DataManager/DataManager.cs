using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    public static DataManager Instance { get; private set; }

    [SerializeField] RecordInfo[] recordsInfo;
    [SerializeField] DataInfo[] slots;
    [SerializeField] PlayerInfo playerInfo;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
        
    }

    private void Start() {
        RefreshData();
        RefreshRecords();
    }

    public void RefreshData() {
        StartCoroutine("InitializeData");
    }

    public void RefreshRecords() {
        StartCoroutine("InitializeRecords");
    }

    public void ResumeData() {
        object[] activeData = DBVault.GetActiveData();
        object[] activeCheckpoint = DBVault.GetActiveCheckpoint();

        playerInfo.SlotID = (int)activeData[(int)SaveData.Slot_ID];
        playerInfo.Name = (string)activeData[(int)SaveData.Name];
        playerInfo.PowerUps = (int)activeData[(int)SaveData.PowerUps];
        playerInfo.Score = (int)activeData[(int)SaveData.Score];
        playerInfo.CurrentHp = (int)activeData[(int)SaveData.CurrentHp]; 
        playerInfo.Runtime = (int)activeData[(int)SaveData.Runtime];
        playerInfo.Checkpoint = new Vector2((int)activeCheckpoint[1], (int)activeCheckpoint[2]);

    }

    public void QuickSaveData() {
        object[] slotData = new object[4];
        object[] checkpointData = new object[2];

        slotData[0] = playerInfo.Name;
        slotData[1] = playerInfo.PowerUps;
        slotData[2] = playerInfo.Score;
        slotData[3] = playerInfo.CurrentHp;

        checkpointData[0] = playerInfo.Checkpoint.x;
        checkpointData[1] = playerInfo.Checkpoint.y;

        DBVault.UpdateActiveSlot(slotData);
        DBVault.SetCheckpoint(checkpointData);

    }

    public void OverwriteData(int slotID) {
        object[] slotData = new object[4];
        object[] checkpointData = new object[2];

        slotData[0] = playerInfo.Name;
        slotData[1] = playerInfo.PowerUps;
        slotData[2] = playerInfo.Score;
        slotData[3] = playerInfo.CurrentHp;

        checkpointData[0] = playerInfo.Checkpoint.x;
        checkpointData[1] = playerInfo.Checkpoint.y;

        DBVault.UpdateSlotByIdx(slotID, slotData);
        DBVault.UpdateCheckpoint(slotID, checkpointData);
    }

    public void DeleteData(int slotID) {
        DBVault.ResetSlotCPByIdx(slotID);
    }

    public void AssignSlotInfo(int slot) {
        DBVault.SetActiveSlot(slots[slot].SlotID);

        playerInfo.SlotID = slots[slot].SlotID;
        playerInfo.Name = slots[slot].Name;
        playerInfo.PowerUps = slots[slot].PowerUps;
        playerInfo.Score = slots[slot].Score;
        playerInfo.CurrentHp = slots[slot].CurrentHp;
        playerInfo.Runtime = 1;
        playerInfo.Checkpoint = slots[slot].Checkpoint;

        QuickSaveData();
    }

    public DataInfo GetSlotInfo(int slot) {
        return slots[slot];
    }

    public void SetRecord() {                
        DBVault.SetHighscore(playerInfo.Name, playerInfo.Score);
    }

    private IEnumerator InitializeData() {
        int i = 0;
        List<object[]> saves = DBVault.GetSlotsData();
        List<object[]> checkpoints = DBVault.GetSlotsCheckpoint();

        foreach (object[] save in saves) {
            slots[i].SlotID = (int)save[(int)SaveData.Slot_ID];
            slots[i].Name = (string)save[(int)SaveData.Name];
            slots[i].PowerUps = (int)save[(int)SaveData.PowerUps];
            slots[i].Score = (int)save[(int)SaveData.Score];
            slots[i].CurrentHp = (int)save[(int)SaveData.CurrentHp];
            slots[i].Runtime = (int)save[(int)SaveData.Runtime];
            slots[i].Checkpoint = new Vector2((int)checkpoints[i][1], (int)checkpoints[i][2]);
            i++;

            yield return null;
        }

        yield break;
    }

    private IEnumerator InitializeRecords() {
        int i = 0;
        List<object[]> records = DBVault.GetHighscore();       

        foreach (object[] record in records) {            
            recordsInfo[i].Name = (string)record[(int)Record.Name];
            recordsInfo[i].Score = (int)record[(int)Record.Score];

            i++;

            yield return null;
        }

        yield break;
    }

}
