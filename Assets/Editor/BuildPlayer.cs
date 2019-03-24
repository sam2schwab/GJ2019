using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BuildPlayer : MonoBehaviour
{
    [MenuItem("Build/Build Win64")]
    public static void BuildForWindows64 ()
    {
        var scenes = new List<string>()
            {"IntroVideo", "Start", "main", "Credits", "Leaderboard"};
        var levels = from s in scenes select $"Assets/Scenes/{s}.unity";
        var options = new BuildPlayerOptions()
        {
            scenes = levels.ToArray(),
            target = BuildTarget.StandaloneWindows64,
            locationPathName = "Builds/Win64Build/Home World.exe"
        };

        BuildPipeline.BuildPlayer(options);
    }
}

