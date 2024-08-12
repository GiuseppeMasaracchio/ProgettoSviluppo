using UnityEngine;

[CreateAssetMenu]
public class PlayerInfo : ScriptableObject {
    [SerializeField] private int _currentHp;
    [SerializeField] private int _powerUps;
    [SerializeField] private int _score;
    [SerializeField] private Vector2 _checkpoint;
    [Space]
    [SerializeField] private string _playerRootState;
    [SerializeField] private string _playerSubState;

    public int CurrentHp { get { return _currentHp; } set { _currentHp = value; } }
    public int PowerUps { get { return _powerUps; } set { _powerUps = value; } }
    public int Score { get { return _score; } set { _score = value; } }
    public Vector2 Checkpoint { get { return _checkpoint; } set { _checkpoint = value; } }
    public string PlayerRootState { get { return _playerRootState; } set { _playerRootState = value; } }
    public string PlayerSubState { get { return _playerSubState; } set { _playerSubState = value; } }

}
