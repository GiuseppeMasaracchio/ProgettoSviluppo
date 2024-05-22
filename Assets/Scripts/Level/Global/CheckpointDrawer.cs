using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct CP {
    public int lvl_idx;
    public int cp_idx;
    public Vector3 pos;
    public Vector3 rot;
}

public class CheckpointDrawer : ScriptableObject {
    [SerializeField] CP[] _checkpoint = new CP[4];

    private void OnEnable() {
        InizializeLvlIdx();
        
    }

    private void InizializeLvlIdx() {
        _checkpoint[(int)Scenes.Lab].lvl_idx = (int)Scenes.Lab;
        _checkpoint[(int)Scenes.Map1].lvl_idx = (int)Scenes.Map1;
        _checkpoint[(int)Scenes.Warp1].lvl_idx = (int)Scenes.Warp1;
        _checkpoint[(int)Scenes.Warp2].lvl_idx = (int)Scenes.Warp2;
    }
}
