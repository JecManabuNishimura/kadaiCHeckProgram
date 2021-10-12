using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrerList : MonoBehaviour
{
    [SerializeField]
    private Text numText;
    [SerializeField]
    private Image numImage;
    [SerializeField]
    private Text dayText;
    [SerializeField]
    private Image dayImage;
    [SerializeField]
    private Text classText;
    [SerializeField]
    private Image classImage;
    [SerializeField]
    private Text numAndNameText;
    [SerializeField]
    private Image numAndNameImage;
    [SerializeField]
    private Text fileNameText;
    [SerializeField]
    private Image fileNameImage;

    private List<GameObject> dateList = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetErrerData(int num, string sDay,string sClass,string sNumAName,string sFileName)
	{
        numText.text = num.ToString();
        dayText.text = sDay;
        classText.text = sClass;
        numAndNameText.text = sNumAName;
        fileNameText.text = sFileName;
        

    }

    public void SetErrorColor(lessonErrorClass lec)
	{
        if(!lec.classNameFlag)
		{
            classImage.color = Color.red;
		}
        if (!lec.dateFlag)
        {
            dayImage.color = Color.red;
        }
        if (!lec.fileNameFlag)
        {
            fileNameImage.color = Color.red;
        }
        if (!lec.nameFlag)
        {
            numAndNameImage.color = Color.red;
        }
    }
}
