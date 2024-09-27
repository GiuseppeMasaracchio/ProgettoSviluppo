using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    public static DataManager Instance { get; private set; }

    [SerializeField] DataInfo[] slots;
    [SerializeField] PlayerInfo playerInfo;

    private List<object[]> saves; 
    private List<object[]> checkpoints;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    private void Start() {
        StartCoroutine("InitializeData");
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

    public void AutoSaveData(PlayerInfo _playerInfo) {
        object[] slotData = new object[4];
        object[] checkpointData = new object[2];

        slotData[0] = _playerInfo.Name;
        slotData[1] = _playerInfo.PowerUps;
        slotData[2] = _playerInfo.Score;
        slotData[3] = _playerInfo.CurrentHp;

        checkpointData[0] = _playerInfo.Checkpoint.x;
        checkpointData[1] = _playerInfo.Checkpoint.y;

        DBVault.UpdateActiveSlot(slotData);
        DBVault.SetCheckpoint(checkpointData);

    }

    public void AssignSlotInfo(SaveSlot slot) {
        playerInfo.SlotID = slots[(int)slot].SlotID;
        playerInfo.Name = slots[(int)slot].Name;
        playerInfo.PowerUps = slots[(int)slot].PowerUps;
        playerInfo.Score = slots[(int)slot].Score;
        playerInfo.CurrentHp = slots[(int)slot].CurrentHp;
        playerInfo.Runtime = slots[(int)slot].Runtime;
        playerInfo.Checkpoint = slots[(int)slot].Checkpoint;

        DBVault.SetActiveSlot(playerInfo.SlotID);

    }


    public DataInfo GetSlotInfo(int slot) {
        return slots[slot];
    }

    private IEnumerator InitializeData() {
        int i = 0;

        saves = DBVault.GetSlotsData();
        checkpoints = DBVault.GetSlotsCheckpoint();

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
    
}
