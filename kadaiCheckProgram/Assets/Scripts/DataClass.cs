using System;
using System.Collections.Generic;

[Serializable]
public class DataClass
{
    public string mainClassName;
    public string taskName;
    public List<lessonClass> lessonClasses = new List<lessonClass>();
    public List<ChallengeData> challengeDatas = new List<ChallengeData>();
    public List<lessonErrorClass> LEClasses = new List<lessonErrorClass>();
}

[Serializable]
public class lessonClass
{
    public string className;
    public string date;
    public string name;
    public string fileName;
}
[Serializable]
public class lessonErrorClass
{
    public bool classNameFlag;
    public bool dateFlag;
    public bool nameFlag;
    public bool fileNameFlag;
}


[Serializable]
public class ChallengeData
{
    public string number;
}