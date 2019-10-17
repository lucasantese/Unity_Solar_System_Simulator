using System.IO;
using System.Collections.Generic;

//Custom KeyList class used to store both a key and a list of values.
public class KeyList
{
    //Takes a string input on declaration.
    public KeyList(string thisKey)
    {
        _key = thisKey;
    }
    //String list is stored as "_list" and retrieved as "list".
    private List<string> _list = new List<string>();
    public List<string> list
    {
        get
        {
            return _list;
        }
        set
        {
            _list = value;
        }
    }
    //String key is stored as "_key" and retrieved as "key".
    private string _key = "";
    public string key
    {
        get
        {
            return _key;
        }
        set
        {
            _key = value;
        }
    }
    //Returns the length of the list "list".
    public int length
    {
        get
        {
            return _list.Count;
        }
    }
}

//Custom CSV class used to load, store and create CSV files.
public class CSV
{
    //Table stores a list of KeyList values, working effectively as a 2D array with a key for each stored array.
    public List<KeyList> Table = new List<KeyList>();

    //List of keys used to find referenced keys, specified in the CSV.
    public string[] keys;

    //Theisfunction called when the class is declared without any parameters, all internal variables are empty.
    public CSV(string[] titles) { }

    //Takes the file location as a string on declaration.
    public CSV(string file)
    {
        //Reads all lines from the file and splits it up into string array rows.
        string[] rows = File.ReadAllLines(file);

        //Gets all of the keys from each of the rows.
        keys = rows[0].Split(',');

        //Takes each of the keys from the array and modifies the Table according to the new keys.
        foreach (string key in keys)
        {
            KeyList newList = new KeyList(key);
            Table.Add(newList);
        }
        //Iterates through each of the rows and fills in each of the KeyLists list.
        for (int row = 1; row < rows.Length; row++)
        {
            string[] values = rows[row].Split(',');
            for (int value = 0; value < values.Length; value++)
            {
                Table[value].list.Add(values[value]);
            }
        }
    }
    //Gets the column with the key "keyName".
    public List<string> GetColumn(string keyName)
    {
        foreach (KeyList list in Table)
        {
            if (list.key == keyName)
                return list.list;
        }
        return null;
    }
    //Takes an integer index and returns the corresponding key.
    public string IndexToKey(int index)
    {
        foreach (KeyList list in Table)
        {
            if (list.key == keys[index])
                return list.key;
        }
        return null;
    }
    //Takes a string and stores a CSV file in the specified location, taking its values from Table.
    public void Save(string file)
    {
        //Declares a string to write each of the lines in the contents CSV.
        string writer = string.Join(",", keys) + "\n";
        //Gets the index of the longest row.
        int longestRow = 0;
        foreach (KeyList list in Table)
        {
            if (list.length > longestRow)
            {
                longestRow = list.length;
            }
        }
        //Iterates through the rows and parses the table into columns.
        for (int row = 0; row < longestRow; row++)
        {
            for (int key = 0; key < keys.Length; key++)
            {
                writer = writer + Table[key].list[row];
                if (key < keys.Length - 1)
                {
                    writer = writer + ",";
                }
            }
            if (row < longestRow - 1)
            {
                writer = writer + "\n";
            }
        }
        //Writes the string to a file.
        File.WriteAllText(file, writer);
    }
}
