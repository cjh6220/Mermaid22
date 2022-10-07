using System.Threading;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;

public class LogManager
{
    class LogWriter
    {
        object _lock = new object();
        string _filePath = "";

        public LogWriter(string fileName)
        {
#if UNITY_EDITOR
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), $"Logs/{fileName}.log");
#else
            _filePath = Path.Combine(Application.persistentDataPath, $"{fileName}.log");
#endif

            if (false == File.Exists(_filePath))
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(_filePath))
                    {
                        sw.WriteLine("");
                        sw.Close();
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        public void Write(object tag, object log)
        {
            Task.Run(() =>
                {
                    lock (_lock)
                    {
                        try
                        {
                            using (StreamWriter sw = File.AppendText(_filePath))
                            {
                                sw.WriteLine($"{DateTime.Now.ToString("yyy-MM-dd HH:mm:ss:fff")}\t[{tag}]\t{log}");
                                sw.Close();
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(e);
                        }
                    }
                });
        }
    }

    static LogWriter _logWriter = null;
    static LogWriter _battle_logWriter = null;

    public static void Init()
    {
        if (null == _logWriter)
        {
            _logWriter = new LogWriter($"Log_Game_{DateTime.Now.ToString("yyyMMdd_HHmmss")}");
        }
    }

    public static void Battle_Start()
    {
        _battle_logWriter = new LogWriter($"Log_Battle_{DateTime.Now.ToString("yyyMMdd_HHmmss")}");
    }

    public static void Battle_End()
    {
        _battle_logWriter = null;
    }

    public static void Log(object log, bool onlyWrite = false)
    {
        if (false == onlyWrite)
        {
            Debug.Log(log);
        }

        WriteLine("Info", log);
    }

    public static void LogPhoton(object log, bool onlyWrite = false)
    {
        if (false == onlyWrite)
        {
            Debug.Log($"<color=lightblue>[Photon] {log} </color>");
        }

        WriteLine("Photon", log);

        WriteLine_Battle("Photon", log);

    }

    public static void LogHexa(object log, bool onlyWrite = false)
    {
        if (false == onlyWrite)
        {
            Debug.Log($"<color=white>[Hexa] {log} </color>");
        }

        WriteLine_Battle("Hexa", log);
    }

    public static void LogNetwork(object log, bool onlyWrite = false)
    {
        if (false == onlyWrite)
        {
            Debug.Log($"<color=orange>[Net] {log} </color>");
        }

        WriteLine("Net", log);

        WriteLine_Battle("Net", log);
    }

    public static void LogWarning(object log, bool onlyWrite = false)
    {
        if (false == onlyWrite)
        {
            Debug.LogWarning(log);
        }

        WriteLine("Warning", log);

        WriteLine_Battle("Warning", log);
    }

    public static void LogError(object log, bool onlyWrite = false)
    {
        if (false == onlyWrite)
        {
            Debug.LogError(log);
        }

        WriteLine("Error", log);

        WriteLine_Battle("Error", log);
    }

    public static void LogCheat(object log, bool onlyWrite = false)
    {
        if (false == onlyWrite)
        {
            Debug.Log($"<color=#ff00ffff>[CHEAT] {log} </color>");
            Debug.Log("");
        }

        WriteLine_Battle("Cheat", log);
    }

    static void WriteLine(string tag, object log)
    {
        _logWriter?.Write(tag, log);
    }

    static void WriteLine_Battle(string tag, object log)
    {
        _battle_logWriter?.Write(tag, log);
    }
}