// See https://aka.ms/new-console-template for more information

using Microsoft.Data.Sqlite;

Console.WriteLine("Hello, World!");

string connectionString = @"Data Source=F:\Database\PictureTools.db3";
string queryReadFolders = "Select id,name from folders";
string queryInsertFolder = "Insert into folders (name) values (\"{0}\")";


List<Folder> folders = new List<Folder>();

using (SqliteConnection conn = new SqliteConnection(connectionString))
{
    conn.Open();
    using (SqliteCommand cmd = new SqliteCommand(string.Format(queryInsertFolder,"Psyko_live"), conn))
    {
        var result = cmd.ExecuteNonQuery();
        if (result == 1)
        {
            Console.WriteLine("Insert Ok)");
        }
    }
}


using (SqliteConnection conn = new SqliteConnection(connectionString))
{
    conn.Open();
    using (SqliteCommand cmd = new SqliteCommand(queryReadFolders, conn))
    {
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            folders.Add(new Folder
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
            });
        }
    }
}

folders.ForEach(f => Console.WriteLine($"{f.Id} : {f.Name}"));

class Folder
{
    public int Id { get; set; }
    public string Name { get; set; }
}
