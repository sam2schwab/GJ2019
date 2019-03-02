using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    private readonly List<Score> _globalBestScores;
    private List<Score> _localBestScores;
    private readonly List<int> _localScores;
    private const string BestScoresFileName = "best_scores.xml";
    private const string ScoresFileName = "scores.xml";

    public LeaderboardManager()
    {
        InitFiles();
        _localBestScores = ReadFromXmlFile<List<Score>>(BestScoresFileName);
        _localScores = ReadFromXmlFile<List<int>>(ScoresFileName);
        _globalBestScores = new List<Score>(); //TODO
    }

    private void InitFiles()
    {
        if (!File.Exists(BestScoresFileName))
            WriteToXmlFile(BestScoresFileName, new List<Score>(){new Score("bob", 200)});
        if (!File.Exists(ScoresFileName))
            WriteToXmlFile(ScoresFileName, new List<int>(){200, 70, 23, 15});
    }
    
    public List<Score> GetBestScores(bool global = false)
    {
        return global ? _globalBestScores : _localBestScores;
    }

    public int GetPosition(int score, bool global)
    {
        return global ? GetGlobalPosition(score) : GetLocalPosition(score);
    }

    private int GetLocalPosition(int score)
    {
        return _localScores.FindAll(x => x > score).Count;
    }

    private int GetGlobalPosition(int score)
    {
        return 1;
    }
    
    public void SaveScore(Score score)
    {
        SaveScoreLocally(score);
        SaveScoreGlobally(score);
    }

    private void SaveScoreGlobally(Score score)
    {
        
    }

    private void SaveScoreLocally(Score score)
    {
        _localScores.Add(score.Value);
        if (GetLocalPosition(score.Value) < 10)
        {
            _localBestScores.Add(score);
            _localBestScores = _localBestScores.OrderBy(s => s.Value).ToList();
            WriteToXmlFile(BestScoresFileName, _localBestScores);
        }
    }

    /// <summary>
    /// Writes the given object instance to an XML file.
    /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
    /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
    /// <para>Object type must have a parameterless constructor.</para>
    /// </summary>
    /// <typeparam name="T">The type of object being written to the file.</typeparam>
    /// <param name="filePath">The file path to write the object instance to.</param>
    /// <param name="objectToWrite">The object instance to write to the file.</param>
    /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
    private static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
    {
        TextWriter writer = null;
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            writer = new StreamWriter(filePath, append);
            serializer.Serialize(writer, objectToWrite);
        }
        finally
        {
            if (writer != null)
                writer.Close();
        }
    }

    /// <summary>
    /// Reads an object instance from an XML file.
    /// <para>Object type must have a parameterless constructor.</para>
    /// </summary>
    /// <typeparam name="T">The type of object to read from the file.</typeparam>
    /// <param name="filePath">The file path to read the object instance from.</param>
    /// <returns>Returns a new instance of the object read from the XML file.</returns>
    private static T ReadFromXmlFile<T>(string filePath) where T : new()
    {
        TextReader reader = null;
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            reader = new StreamReader(filePath);
            return (T)serializer.Deserialize(reader);
        }
        finally
        {
            if (reader != null)
                reader.Close();
        }
    }
}