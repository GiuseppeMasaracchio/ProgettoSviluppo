using System.Collections;
using System.Data;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public static class DBVault {
    //Ambient vars
    private static string path = "Data Source=file:DB.db";
    private static IDbConnection dbcon;
    private static IDbCommand dbcmd;
    private static IDataReader reader;
    private static string query;


    //Connection Handlers
    private static void OpenConnection() {
        dbcon = new SqliteConnection(path);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
    }

    private static void CloseConnection() {
        reader.Dispose();
        dbcmd.Dispose();
        dbcon.Close();
    }


    //Getters
    public static int GetActiveSlotIdx() {
        OpenConnection();

        query = "SELECT Slot_ID FROM Slot WHERE Runtime = 1;";
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();
        int retrieve = 0;

        while (reader.Read()) {
            retrieve = reader.GetInt32(reader.GetOrdinal("Slot_ID"));
        }

        CloseConnection();
        return retrieve;
    }

    public static object GetDataByIdx(int idx, string table, string column) {
        OpenConnection();

        query = "SELECT * FROM " + table + " WHERE " + table + "_ID = " + idx + ";";
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();
        object retrieve = null;

        while (reader.Read()) {
            retrieve = reader.GetValue(reader.GetOrdinal(column));          
        }

        CloseConnection();
        return retrieve;
    }

    public static List<object[]> GetHighscore() {
        OpenConnection();

        query = "SELECT * FROM Highscore ORDER BY highscore DESC";
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();

        List<object[]> highscore = new List<object[]>();
        highscore.Capacity = 0;

        while (reader.Read()) {
            highscore.Capacity++;
            object[] array = new object[] { reader["Highscore_ID"], reader["name"], reader["highscore"]};
            highscore.Add(array);
        }

        CloseConnection();
        return highscore;
    }


    //Setters
    public static void UpdateValueByIdx(int idx, string table, string column, int value) {
        OpenConnection();

        query = "UPDATE " + table + " SET " + column + " = " + value + " WHERE " + table + "_ID = " + idx + ";";
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();

        CloseConnection();
    }

    public static void InsertValue(string table, string column, int value) {
        OpenConnection();

        query = "INSERT INTO " + table + "( " + column + ") VALUES(" + value + ");";
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();

        CloseConnection();
    }

    public static void InitDB() {
        OpenConnection();

        query = "CREATE TABLE Slot (Slot_ID int PRIMARY Key, Name varchar(10), Powerups int, Score int, CurrentHp int, Runtime int); \n";
        query += "CREATE TABLE Checkpoint (Slot_ID int PRIMARY KEY, Level_idx int, CP_idx int, foreign key(Slot_ID) references Slot(Slot_ID)); \n";
        query += "CREATE TABLE Highscore (Highscore_ID int PRIMARY Key, Name varchar(10), Highscore int); \n";

        query += "INSERT INTO Slot VALUES('1', 'Test_1', '0', '0', '3', '0'); \n";
        query += "INSERT INTO Slot VALUES('2', 'Test_2', '0', '0', '3', '0'); \n";
        query += "INSERT INTO Slot VALUES('3', 'Test_3', '0', '0', '3', '0'); \n";

        query += "INSERT INTO Checkpoint VALUES('1', '0', '0'); \n";
        query += "INSERT INTO Checkpoint VALUES('2', '0', '0'); \n";
        query += "INSERT INTO Checkpoint VALUES('3', '0', '0'); \n";

        query += "INSERT INTO Highscore VALUES('1', 'Dev_1', '0'); \n";
        query += "INSERT INTO Highscore VALUES('2', 'Dev_2', '0'); \n";
        query += "INSERT INTO Highscore VALUES('3', 'Dev_2', '0'); \n";
        query += "INSERT INTO Highscore VALUES('4', 'Dev_1', '0'); \n";
        query += "INSERT INTO Highscore VALUES('5', 'Dev_1', '0');";


        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();

        CloseConnection();
    }


    //DROP
    public static void DropTable(string table) {
        OpenConnection();

        query = "DROP TABLE " + table + ";";

        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();

        CloseConnection();
    }
}
