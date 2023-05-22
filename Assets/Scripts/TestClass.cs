using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HighscoreManager {
    public static void SetScore(string newname, int newint) {
        PlayerPrefs.SetInt("[score]" + newname, newint);
        PlayerPrefs.SetString("[name]" + newname, newname);
    }

    public static (int, string) GetScore(string name) {
        return (PlayerPrefs.GetInt("[score]" + name), PlayerPrefs.GetString("[name]" + name));
    }
}
