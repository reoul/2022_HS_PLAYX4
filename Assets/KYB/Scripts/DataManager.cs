using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;
using Random = UnityEngine.Random;

public class DataManager : Singleton<DataManager>
{
    private const string _settingFilePath = "./setting.csv";
    private const string _scoreFilePath = "./score.csv";
    public SettingData Data;
    private List<Score> _scores;
    public int LastPlayerIndex = 100;

    void Awake()
    {
        Data = new SettingData();
        ScoreSystem.SumScore = 100;
        _scores = new List<Score>();
        SettingLoad();
        GetScore();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ScoreSystem.SumScore += Random.Range(-10, 10);
            SaveNewScore();
        }
        Debug.Log(LastPlayerIndex);
    }

    /// <summary>
    /// 게임 세팅을 설정 할 수 있는 파일 생성(기본값으로 생성됨)
    /// </summary>
    private SettingData CreateDefaultSettingFile()
    {
        var writer = new StreamWriter(_settingFilePath, false);
        Data = new SettingData();
        string strData = JsonUtility.ToJson(Data, true);
        writer.Write(strData);
        writer.Flush();
        writer.Close();
        return Data;
    }

    // 반드시 게임 시작시 무조건 호출해서
    // 각 플레이 요소에 로드된 세팅값을 적용해야한다.
    /// <summary>
    /// 세팅값 로드 및 적용
    /// </summary>
    private void SettingLoad()
    {
        try
        {
            using (StreamReader reader = new StreamReader(_settingFilePath))
            {
                while (!reader.EndOfStream)
                {
                    string[] data = { };
                    string line = reader.ReadLine();
                    try
                    {
                        data = line.Split(',');
                        switch (data[2].Trim().ToLower())
                        {
                            case "int32":
                            case "int":
                                ApplyData(data[0].Trim(), Convert.ToInt32(data[1].Trim()));
                                break;
                            case "single":
                            case "float":
                                ApplyData(data[0].Trim(), Convert.ToSingle(data[1].Trim()));
                                break;
                            case "bool":
                                ApplyData(data[0].Trim(), Convert.ToBoolean(data[1].Trim()));
                                break;
                            default:
                                Debug.LogError($"{data[0]} 변수의 자료형인 {data[2]} 타입은 지원하지 않는 자료형입니다");
                                break;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Debug.LogError($"IndexOutOfRangeException : {data[0]} 변수의 데이터 타입이 없습니다");
                    }
                    catch (FormatException)
                    {
                        Debug.LogError($"FormatException : {data[0]} 변수의 {data[1]} 값이 {data[2]} 타입에 맞지 않습니다");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"{data[0]} 변수가 클래스에 없습니다");
                    }
                }
            }
        }
        catch (Exception e) //파일이 없음
        {
            return;
        }

        // todo : 불러온 세팅값 실제 플레이에 적용
    }

    void ApplyData<T>(string fieldName, T value)
    {
        FieldInfo fieldInfo = Type.GetType("SettingData").GetField(fieldName);
        if (fieldInfo.FieldType != typeof(T))
        {
            Debug.LogError($"{fieldName} 변수의 타입 {fieldInfo.FieldType}이 입력 데이터 타입 {typeof(T)}이랑 같지 않습니다");
            return;
        }

        fieldInfo.SetValue(Data, value);
        Debug.Log($"{fieldName}의 값이 {value}로 변경되었습니다");
    }

    public Score SaveNewScore()
    {
        
        _scores.Add(new Score($"HUNTER{LastPlayerIndex.ToString()}",ScoreSystem.SumScore));
        using (StreamWriter writer = new StreamWriter(_scoreFilePath, true))
        {
            string data = $"HUNTER{(LastPlayerIndex++).ToString()},{ScoreSystem.SumScore.ToString()}\n";
            writer.Write(data);
            writer.Flush();
        }

        return new Score(name, ScoreSystem.SumScore);
    }

    public List<Score> GetScore()
    {
        _scores.Clear();
        try
        {
            using (StreamReader reader = new StreamReader(_scoreFilePath))
            {
                string[] data = {"100","100"};
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    data = line.Split(',');
                    Debug.Log(data[0]);
                    _scores.Add(new Score(data[0], Convert.ToInt32(data[1])));
                    data[0] = data[0].Substring(6);
                }
                
                LastPlayerIndex = Convert.ToInt32(data[0]);
            }
        }
        catch (Exception e) //파일이 없음
        {
            // ignored
        }

        return _scores;
    }
}