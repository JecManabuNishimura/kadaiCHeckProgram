using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class FileDataCreate : MonoBehaviour
{
    [SerializeField]
    private InputField fullfilePath;

    private string[] fileList;

    private List<string> csvDataList = new List<string>();

    [SerializeField]
    FileUpLoad fu;

    private string shortFilePath;

    private ReadCSV rCSV;

    [SerializeField]
    private InputField scriptPath;

    [SerializeField]
    private OptionCheck optionData;

    private void Start()
    {
        rCSV = GetComponent<ReadCSV>();
        try
        {
            StreamReader sr = new StreamReader(Application.dataPath + @"\dataPath.ini", Encoding.GetEncoding("utf-8"));
            int count = 0;
            while(sr.EndOfStream == false)
            {
                switch(count)
                {
                    case 0:
                        fullfilePath.text = sr.ReadLine();
                        break;
                    case 1:
                        rCSV.SetChallengeFilePath(sr.ReadLine());
                        break;
                    case 2:
                        scriptPath.text = sr.ReadLine();
                        break;
                    case 3:
                        optionData.TaskName = sr.ReadLine();
                        break;
                    case 4:
                        optionData.ClassName = sr.ReadLine();
                        break;
                    case 5:
                        optionData.FileToggle = bool.Parse(sr.ReadLine());
                        break;
                    default:
                        break;
                }
                count++;
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    public void StartCreate()
    {
        if(optionData.TaskName == "")
        {
            CreateText.SendText("<color=red>課題名が設定されていません</color>");
            return;
        }
        if (optionData.ClassName == "")
        {
            CreateText.SendText("<color=red>クラス名が設定されていません</color>");
            return;
        }
        CreateText.SendText("処理開始");

        if (!ReadFile())
            // 読み込み失敗
            return;

        CreateFileListData();
        CreateCSVData();
        CreateJson.WriteJson();

        CreateText.SendText("サーバーデータ送信開始");
        fu.SetGASURL(scriptPath.text);
        CreateJson.GetJsonData();
        CreateText.SendText(fu.CallPost());
    }

    bool ReadFile()
    {
        CreateText.SendText("ファイル読み込み中");
        try
        {
            string tmp = fullfilePath.text;
            tmp = tmp.Replace("\r", "");
            shortFilePath = tmp;
            string[] files = Directory.GetFiles(shortFilePath, "*.*", SearchOption.AllDirectories);
            fileList = new string[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                Debug.Log(files[i]);
            }
            fileList = files;
        }
        catch
        {
            CreateText.SendText("<color=red>ファイルパスが不正です。</color>");;
            return false;
        }

        // 挑戦課題一覧読み込み
        if(rCSV.GetChallengeFilePath() != "")
            rCSV.ReadFile();
        return true;
    }

    void CreateFileListData()
    {
        CreateText.SendText("ファイルJSON化");
        CreateJson.SetDataList(optionData.ClassName, optionData.TaskName);
        foreach (var data in fileList)
        {
            string shortpath = data.Substring(shortFilePath.Length);
            string temp1 = shortpath.Replace('\\', ',');
            if(optionData.GetTaskVersion() == "file")
            {
                if (temp1.IndexOf(".exe") != -1)
                {
                    continue;
                }
                string temp2 = temp1.Replace(".cpp", "");
                string[] ldata = temp2.Split(',');
                try
                {
                    if (ldata.Length != 4)
                    {
                        continue;
                    }
                    lessonClass tmp = new lessonClass();

                    tmp.className = ldata[0];
                    tmp.date = ldata[1];
                    tmp.name = ldata[2];
                    tmp.fileName = ldata[3];

                    CreateJson.SetDataList(tmp);
                    csvDataList.Add(temp2);
                }
                catch (IOException e)
                {
                    Debug.Log(e.Message);
                }
            }
            else
            {
                string[] ldata = temp1.Split(',');
                // ファイルが存在していた場合は次へ
                if(CreateJson.CheckData(ldata[2],ldata[3]))
                {
                    continue;
                }
                lessonClass tmp = new lessonClass();
                tmp.className = ldata[0];
                tmp.date = ldata[1];
                tmp.name = ldata[2];
                tmp.fileName = ldata[3];

                CreateJson.SetDataList(tmp);
                csvDataList.Add(temp1);
            }
        }
        if (rCSV.GetChallengeFilePath() != "")
        {
            foreach (var tmp in rCSV.GetChallengeJsonData())
            {
                // 挑戦課題一覧の書き出し
                CreateJson.SetDataList(tmp);
            }
        }
    }

    void CreateCSVData()
    {
        CreateText.SendText("ファイルCSV化");
        string filePath = Application.dataPath + @"\Data.csv";
        FileStream file = null;
        try
        {
           // ファイルのチェック
           // ファイルが存在しない場合は作成
            file = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
        catch(IOException e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            if(file != null)
            {
                try
                {
                    file.Dispose();
                }catch(IOException e2)
                {
                    Debug.Log(e2.Message);
                }
            }
        }
        StreamWriter sw;
        sw = new StreamWriter(filePath, false, Encoding.UTF8);
        foreach(var list in csvDataList)
        {
            sw.WriteLine(list);
        }
        sw.Close();
    }

    private void OnApplicationQuit()
    {
        FileStream file = null;
        try
        {
            // ファイルのチェック
            // ファイルが存在しない場合は作成
            file = File.Open(Application.dataPath + @"\dataPath.ini", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
        catch (IOException e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            if (file != null)
            {
                try
                {
                    file.Dispose();
                }
                catch (IOException e2)
                {
                    Debug.Log(e2.Message);
                }
            }
        }

        StreamWriter sw;
        sw = new StreamWriter(Application.dataPath + @"\dataPath.ini", false, Encoding.UTF8);
        sw.WriteLine(fullfilePath.text);
        sw.WriteLine(rCSV.GetChallengeFilePath());
        sw.WriteLine(scriptPath.text);
        sw.WriteLine(optionData.TaskName);
        sw.WriteLine(optionData.ClassName);
        sw.WriteLine(optionData.FileToggle);
        sw.Close();
    }
}
