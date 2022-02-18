using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class DBHandler : MonoBehaviour
{
    private const string database_name = "db";

    public string db_connection_string;
    public static IDbConnection db_connection;

    private static UserData user;

    private string selectAllUsers = "SELECT * FROM Users";

    // Start is called before the first frame update
    void Start()
    {
        db_connection_string = "URI=file:" + Application.persistentDataPath + "/" + database_name;
        Debug.Log("db_connection_string" + db_connection_string);
        db_connection = new SqliteConnection(db_connection_string);
        db_connection.Open();
        initDB();
        initUsers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static IDbCommand getDbCommand()
    {
        return db_connection.CreateCommand();
    }

    private void initDB()
    {
        IDbCommand dbCommand = getDbCommand();
        dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS \"Users\" (\"user_id\"   INTEGER NOT NULL UNIQUE, \"username\"  TEXT NOT NULL UNIQUE, \"password\"  TEXT NOT NULL, PRIMARY KEY(\"user_id\" AUTOINCREMENT))";
        dbCommand.ExecuteScalar();
        dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS \"Levels\" (\"level_id\"  INTEGER NOT NULL UNIQUE, \"level_name\"    TEXT NOT NULL, \"level_description\" TEXT, PRIMARY KEY(\"level_id\" AUTOINCREMENT))";
        dbCommand.ExecuteScalar();
        dbCommand.CommandText = "CREATE TABLE  IF NOT EXISTS\"ProgressData\" (\"note_id\"   INTEGER NOT NULL UNIQUE, \"level_id\"  INTEGER NOT NULL, \"user_id\"   INTEGER NOT NULL, \"easy_star\" INTEGER NOT NULL DEFAULT 0, \"easy_code\" TEXT, \"medium_star\"   INTEGER NOT NULL DEFAULT 0, \"medium_code\"   TEXT, \"hard_star\" INTEGER NOT NULL DEFAULT 0, \"hard_code\" TEXT, PRIMARY KEY(\"note_id\" AUTOINCREMENT), FOREIGN KEY(\"level_id\") REFERENCES \"Levels\"(\"level_id\"), FOREIGN KEY(\"user_id\") REFERENCES \"Users\"(\"user_id\"))";
        dbCommand.ExecuteScalar();
        dbCommand.CommandText = "CREATE TRIGGER IF NOT EXISTS newUser AFTER INSERT ON Users BEGIN INSERT INTO ProgressData(level_id, user_id, easy_star, easy_code, medium_star, medium_code, hard_star, hard_code) SELECT level_id, NEW.user_id, 0, \"\", 0, \"\", 0, \"\" FROM Levels; END";
        dbCommand.ExecuteScalar();
        dbCommand.CommandText = "CREATE TRIGGER IF NOT EXISTS deleteUser BEFORE DELETE ON Users BEGIN DELETE FROM ProgressData WHERE ProgressData.user_id == OLD.user_id; END";
        dbCommand.ExecuteScalar();
        dbCommand.CommandText = "CREATE TRIGGER IF NOT EXISTS newLevel AFTER INSERT ON Levels BEGIN INSERT INTO ProgressData(level_id, user_id, easy_star, easy_code, medium_star, medium_code, hard_star, hard_code) SELECT NEW.level_id, user_id, 0, \"\", 0, \"\", 0, \"\" FROM Users; END";
        dbCommand.ExecuteScalar();
        dbCommand.CommandText = "CREATE TRIGGER IF NOT EXISTS deleteLevel BEFORE DELETE ON Levels BEGIN DELETE FROM ProgressData WHERE ProgressData.level_id == OLD.level_id; END";
        dbCommand.ExecuteScalar();
        dbCommand.CommandText = "INSERT INTO Levels(level_name, level_description) VALUES(\"Обучение\", \"Найдите количество окружающих эти горы клеток\");" +
            "INSERT INTO Levels(level_name, level_description) VALUES(\"Площадь 1\", \"Найдите внутреннюю площадь этой долины меж скал\");" +
            "INSERT INTO Levels(level_name, level_description) VALUES(\"Ширина 1\", \"Найдите минимальную ширину этого перевала\");" +
            "INSERT INTO Levels(level_name, level_description) VALUES(\"Граница 1\", \"Посчитайте количество клеток с воротами\");" +
            "INSERT INTO Levels(level_name, level_description) VALUES(\"Граница 2\", \"Посчитайте количество клеток с воротами\");" +
            "INSERT INTO Levels(level_name, level_description) VALUES(\"Граница 3\", \"Посчитайте количество клеток с воротами\");" +
            "INSERT INTO Levels(level_name, level_description) VALUES(\"Граница 4\", \"Посчитайте количество клеток с воротами\");" +
            "INSERT INTO Levels(level_name, level_description) VALUES(\"Граница 5\", \"Посчитайте количество клеток с воротами\");";
        dbCommand.ExecuteScalar();
    }

    private void initUsers()
    {
        IDbCommand dbCommand = getDbCommand();
        dbCommand.CommandText = selectAllUsers;
        IDataReader reader = dbCommand.ExecuteReader();
        while (reader.Read())
        {
            user = new UserData(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString());
        }
    }

    public static UserData authorize(string username, string password)
    {
        if (username?.Length != 0 && password?.Length != 0)
        {
            return UserData.authorization(username, password);
        }
        else
        {
            return null;
        }
    }

    public static bool registration(string username, string password)
    {
        if (username?.Length != 0 && password?.Length != 0)
        {
            try
            {
                if (UserData.isNameFree(username))
                {
                    IDbCommand dbCommand = getDbCommand();
                    dbCommand.CommandText = "INSERT INTO Users (username, password) VALUES (\"" + username + "\", \"" + password + "\");";
                    dbCommand.ExecuteNonQuery();
                    dbCommand = getDbCommand();
                    dbCommand.CommandText = "SELECT * FROM Users WHERE username == \"" + username + "\"";
                    IDataReader reader = dbCommand.ExecuteReader();
                    reader.Read();
                    user = new UserData(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString());
                    return true;
                }
            }
            catch
            {
                IDbCommand dbCommand = getDbCommand();
                dbCommand.CommandText = "INSERT INTO Users (username, password) VALUES (\"" + username + "\", \"" + password + "\");";
                dbCommand.ExecuteNonQuery();
                dbCommand = getDbCommand();
                dbCommand.CommandText = "SELECT * FROM Users WHERE username == \"" + username + "\"";
                IDataReader reader = dbCommand.ExecuteReader();
                reader.Read();
                user = new UserData(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString());
                return true;
            }
        }
        return false;
    }

    public static void updateStarNCode(int user_id, int level_id, int difficulty, string code, int star)
    {
        IDbCommand dbCommand = getDbCommand();
        dbCommand.CommandText = "UPDATE ProgressData SET " + ((difficulty == 0) ? "easy" : ((difficulty == 1) ? "medium" : "hard")) + "_code = \"" + code + "\", " + ((difficulty == 0) ? "easy" : ((difficulty == 1) ? "medium" : "hard")) + "_star = " + star + " WHERE user_id == " + user_id + " AND level_id == " + level_id;
        dbCommand.ExecuteNonQuery();
    }

    public static int[] getStars(int user_id, int level_id)
    {
        int[] stars = new int[3];
        IDbCommand dbCommand = getDbCommand();
        dbCommand = getDbCommand();
        dbCommand.CommandText = "SELECT easy_star, medium_star, hard_star FROM ProgressData WHERE user_id == " + user_id + " AND level_id == " + level_id;
        IDataReader reader = dbCommand.ExecuteReader();
        reader.Read();
        stars[0] = int.Parse(reader[0].ToString());
        stars[1] = int.Parse(reader[1].ToString());
        stars[2] = int.Parse(reader[2].ToString());
        return stars;
    }

    public static string getCode(int user_id, int level_id, int difficulty)
    {
        int[] stars = new int[3];
        IDbCommand dbCommand = getDbCommand();
        dbCommand = getDbCommand();
        dbCommand.CommandText = "SELECT " + ((difficulty == 0) ? "easy_code" : ((difficulty == 1) ? "medium_code" : "hard_code")) + " FROM ProgressData WHERE user_id == " + user_id + " AND level_id == " + level_id;
        IDataReader reader = dbCommand.ExecuteReader();
        reader.Read();
        return reader[0].ToString();
    }
}
