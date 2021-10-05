using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ReadCSV : MonoBehaviour
{
    [SerializeField]
    private InputField challengeFilePath;
    private string[] number;

    private List<ChallengeData> cdata = new List<ChallengeData>();
    public void ReadFile()
    {
        StreamReader sr = new StreamReader(challengeFilePath.text, Encoding.GetEncoding("utf-8"));
        string readText = sr.ReadToEnd();
        string[] tmp = readText.Split(',');
        number = tmp;

        foreach(var data in number)
        {
            ChallengeData ctmp = new ChallengeData();
            ctmp.number = data;
            cdata.Add(ctmp);
        }
    }

    public List<ChallengeData> GetChallengeJsonData()
    {
        return cdata;
    }

    public string GetChallengeFilePath()
    {
        return challengeFilePath.text;
    }

    public void SetChallengeFilePath(string text)
    {
        challengeFilePath.text = text;
    }

    [System.Serializable]
    public class RespoinseData
    {
        public string message;
    }
}
