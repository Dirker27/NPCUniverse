using Npgsql;

public class DatabaseInterface
{
    bool writeToDatabase = true;
    public void Save(CharacterSheet sheet)
    {
        if (!writeToDatabase)
        {
            return;
        }
        string connectionString =
          "Server=127.0.0.1;" +
          "Database=greyraven;" +
          "User ID=postgres;" +
          "Password=emc921l;";
        // IDbConnection dbcon; ## CHANGE THIS TO
        NpgsqlConnection dbcon;

        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        //IDbCommand dbcmd = dbcon.CreateCommand();## CHANGE THIS TO
        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        // requires a table to be created named employee
        // with columns firstname and lastname
        // such as,
        //        CREATE TABLE employee (
        //           firstname varchar(32),
        //           lastname varchar(32));
        string sql =
            "INSERT INTO people (name, health,hunger,energy,job,previousstate,currentstate) VALUES ('" + sheet.name + "','" + sheet.health + "','" + sheet.hunger + "','" + sheet.energy + "','" + sheet.job.ToString() + "','" + sheet.previousState.ToString() + "','" + sheet.currentState.ToString() + "') returning id;";
        dbcmd.CommandText = sql;
        NpgsqlDataReader reader = dbcmd.ExecuteReader();

        int id = -1;
        while (reader.Read())
        {
            id = reader.GetInt32(0);
        }
        sheet.id = id;

        Logger log = new Logger();
        log.Log(true, "New pill id is " + id);
        // clean up
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }

    public void Update(CharacterSheet sheet)
    {
        if (!writeToDatabase)
        {
            return;
        }
        string connectionString =
          "Server=127.0.0.1;" +
          "Database=greyraven;" +
          "User ID=postgres;" +
          "Password=;";
        // IDbConnection dbcon; ## CHANGE THIS TO
        NpgsqlConnection dbcon;

        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();

        NpgsqlCommand dbcmd = dbcon.CreateCommand();

        string sql =
            "UPDATE people SET (name,health,hunger,energy,job,previousstate,currentstate) = ('" + sheet.name + "','" + sheet.health + "','" + sheet.hunger + "','" + sheet.energy + "','" + sheet.job.ToString() + "','" + sheet.previousState.ToString() + "','" + sheet.currentState.ToString() + "') WHERE id=" + sheet.id + ";";
        dbcmd.CommandText = sql;
        dbcmd.ExecuteNonQuery();

        // clean up
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }

    public void Save(BaseBuilding building)
    {
        if (!writeToDatabase)
        {
            return;
        }
        string connectionString =
          "Server=127.0.0.1;" +
          "Database=greyraven;" +
          "User ID=postgres;" +
          "Password=emc921l;";
        // IDbConnection dbcon; ## CHANGE THIS TO
        NpgsqlConnection dbcon;

        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        //IDbCommand dbcmd = dbcon.CreateCommand();## CHANGE THIS TO
        NpgsqlCommand dbcmd = dbcon.CreateCommand();
        // requires a table to be created named employee
        // with columns firstname and lastname
        // such as,
        //        CREATE TABLE employee (
        //           firstname varchar(32),
        //           lastname varchar(32));
        string sql =
            "INSERT INTO building (name) VALUES ('" + building.name + "');";
        dbcmd.CommandText = sql;
        dbcmd.ExecuteNonQuery();


        // clean up
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }
}