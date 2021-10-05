using System;
using System.Collections.Generic;

[Serializable]
public class DataClass
{
    public string mainClassName;
    public string taskName;
    public List<lessonClass> lessonClasses = new List<lessonClass>();
    public List<ChallengeData> challengeDatas = new List<ChallengeData>();

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
public class ChallengeData
{
    public string number;
}