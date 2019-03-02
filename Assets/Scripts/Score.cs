using System;

public class Score
{
    public DateTime DateTime { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }

    public Score()
    {
        
    }

    public Score(string name, int value)
    {
        Name = name;
        Value = value;
        DateTime = DateTime.Now;
    }
}