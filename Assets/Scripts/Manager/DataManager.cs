using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private const string _settingFilePath = "./setting.json";

    void Start()
    {
        SettingLoad();
    }

    /*private void Save(string content)
    {
        string path = "";
        var writer = new StreamWriter(path, false);
        string strData = JsonUtility.ToJson(new SettingData());
        writer.Write(strData);
        writer.Flush();
        writer.Close();
    }*/

    /// <summary>
    /// 게임 세팅을 설정 할 수 있는 파일 생성(기본값으로 생성됨)
    /// </summary>
    private SettingData CreateDefaultSettingFile()
    {
        var writer = new StreamWriter(_settingFilePath, false);
        var settingData = new SettingData();
        string strData = JsonUtility.ToJson(settingData, true);
        writer.Write(strData);
        writer.Flush();
        writer.Close();
        return settingData;
    }

    /// <summary>
    /// 세팅값 로드 및 적용
    /// </summary>
    private void SettingLoad()
    {
        string content;
        try
        {
            using (StreamReader reader = new StreamReader(_settingFilePath))
            {
                content = reader.ReadToEnd();
            }
        }
        catch (Exception e) //파일이 없음
        {
            // 다시 세팅 파일 생성
            content = JsonUtility.ToJson(CreateDefaultSettingFile());
        }

        SettingData settingData;
        if (!string.IsNullOrWhiteSpace(content))
        {
            try //json 파일 유효성 검사
            {
                settingData = JsonUtility.FromJson<SettingData>(content);
            }
            catch (Exception e)
            {
                // Json이 회손되었습니다
                // 다시 세팅 파일 생성
                settingData = CreateDefaultSettingFile();
            }
        }
        else
        {
            settingData = CreateDefaultSettingFile();
        }
        
        // todo : 불러온 세팅값 실제 플레이에 적용
    }
}
