using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInfo : ScriptableObject {
    [SerializeField] private int _currentHp;
    [SerializeField] private int _powerUps;
    [SerializeField] private int _score;
    [SerializeField] private Vector2 _checkpoint;    

    public int CurrentHp { get { return _currentHp; } set { _currentHp = value; } }
    public int PowerUps { get { return _powerUps; } set { _powerUps = value; } }
    public int Score { get { return _score; } set { _score = value; } }
    public Vector2 Checkpoint { get { return _checkpoint; } set { _checkpoint = value; } }

}
