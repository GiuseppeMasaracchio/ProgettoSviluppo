using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ICombat {
    
    public void TakeDmg(int dmg);
    void AddHp(int heal);
    
    void Death();
    

}
