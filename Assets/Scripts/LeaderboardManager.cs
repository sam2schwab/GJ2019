using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MongoDB.Driver;
using UnityEngine;

public class LeaderboardManager : SingletonMonoBehaviour<LeaderboardManager>
{
    private List<Score> _localBestScores;
    private List<int> _localScores;
    private const string BestScoresFileName = "best_scores";
    private const string ScoresFileName = "scores";
    private const string MongoConnectionString = "mongodb://app:2gA2qAtiS7daa2J@ds157895.mlab.com:57895/homeworld";
    private IMongoCollection<Score> _globalScores;

    protected override void Awake()
    {
        base.Awake();
        InitFiles();
        _localBestScores = ReadFromFile<List<Score>>(BestScoresFileName);
        _localScores = ReadFromFile<List<int>>(ScoresFileName);
        var client = new MongoClient(MongoConnectionString);
        _globalScores = client.GetDatabase("homeworld").GetCollection<Score>("scores");
    }
    
    private static void InitFiles()
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath, BestScoresFileName)))
            WriteToFile(BestScoresFileName, new List<Score>());
        if (!File.Exists(Path.Combine(Application.persistentDataPath, ScoresFileName)))
            WriteToFile(ScoresFileName, new List<int>());
    }
    
    public async Task<List<Score>> GetBestScores(bool global = false)
    {
        return global ? await GetGlobalBestScores() : _localBestScores;
    }

    private async Task<List<Score>> GetGlobalBestScores()
    {
        return await _globalScores.Find(bson => true)
            .SortByDescending(x => x.Value)
            .ThenByDescending(x => x.DateTime)
            .ToListAsync();
    }

    public async Task<int> GetPosition(int score, bool global)
    {
        return global ? await GetGlobalPosition(score) : GetLocalPosition(score);
    }

    private int GetLocalPosition(int score)
    {
        return _localScores.FindAll(x => x > score).Count + 1;
    }

    private async Task<int> GetGlobalPosition(int score)
    {
        return (int) (await _globalScores.Find(x => x.Value > score).CountDocumentsAsync() + 1);
    }

    public void ResetLocalScores()
    {
        _localScores = new List<int>();
        WriteToFile(ScoresFileName, _localScores);
        _localBestScores = new List<Score>();
        WriteToFile(BestScoresFileName, _localBestScores);
    }
    
    public void SaveScore(Score score)
    {
        SaveScoreLocally(score);
        SaveScoreGlobally(score);
    }

    private void SaveScoreGlobally(Score score)
    {
        _globalScores.InsertOneAsync(score);
    }

    private void SaveScoreLocally(Score score)
    {
        _localScores.Add(score.Value);
        WriteToFile(ScoresFileName, _localScores);
        if (GetLocalPosition(score.Value) < 10)
        {
            _localBestScores.Add(score);
            _localBestScores = _localBestScores.OrderByDescending(s => s.Value)
                .ThenByDescending(s => s.DateTime)
                .ToList();
            WriteToFile(BestScoresFileName, _localBestScores);
        }
    }

    private static void WriteToFile<T>(string fileName, T objectToWrite)
    {
        fileName = Path.Combine(Application.persistentDataPath, fileName);
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open (fileName, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize (fileStream, objectToWrite);
        }
    }

    private static T ReadFromFile<T>(string fileName)
    {
        fileName = Path.Combine(Application.persistentDataPath, fileName);
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open (fileName, FileMode.Open))
        {
            return (T) binaryFormatter.Deserialize (fileStream);
        }
    }

    public string GetDefaultName()
    {
        return _localBestScores.OrderByDescending(x => x.DateTime).First()?.Name ?? "AAA";
    }
}