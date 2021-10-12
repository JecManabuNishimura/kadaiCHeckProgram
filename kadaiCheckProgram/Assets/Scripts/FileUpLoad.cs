using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


/*=============================================
 * GASのほうを更新した場合は、新しいバージョンで保存をしないと、反応しない。
 * URLの変更はないため、そのままでよい。
 =============================================*/
public class FileUpLoad : MonoBehaviour
{
    private string URL = "";

    private string result;

    public void SetGASURL(string url)
    {
        URL = url;
    }

    public string CallPost()
    {
        if(URL == "")
        {
            Debug.Log("スクリプトURLが記述されていません。");
            return "スクリプトURLが記述されていません。";
        }
        //StartCoroutine(Post());
        return "送信処理開始";
    }


    public IEnumerator Post()
    {
        var request = new UnityWebRequest(URL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(CreateJson.GetJsonData());
        //byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        
        

        if (request.isHttpError || request.isNetworkError)
        {
            UnityEngine.Debug.Log(request.error);
            CreateText.SendText(request.error);
        }
        else
        {
            result = request.downloadHandler.text;
            var response = JsonUtility.FromJson<ResponseData>(result);
            CreateText.SendText(response.message);
            UnityEngine.Debug.Log(result);
        }
    }
    [System.Serializable]
    public class ResponseData
    {
        public string message;
    }
}

