using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
public class LogManager : MonoBehaviour
{
    public static LogManager Instance;
    [SerializedDictionary("LogType", "Logger")]
    public SerializedDictionary<string, Logger> loggers;
    void Awake()
    {
        if (Instance == null || Instance == this)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }
}
