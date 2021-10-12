using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCheck : MonoBehaviour
{
    [SerializeField]
    private InputField taskName;
    [SerializeField]
    private InputField className;
    [SerializeField]
    private InputField dayName;

    [SerializeField]
    private Toggle fileToggle;

    [SerializeField]
    private Toggle folderToggle;

    public OptionCheck(Toggle folderToggle)
    {
        this.FolderToggle = folderToggle;
    }

    public bool FileToggle { get => fileToggle.isOn; set => fileToggle.isOn = value; }
    public string ClassName { get => className.text; set => className.text = value; }
    public string TaskName { get => taskName.text; set => taskName.text = value; }
    public bool FolderToggle { get => folderToggle.isOn; set => folderToggle.isOn = value; }
	public string DayName { get => dayName.text; set => dayName.text = value; }

	public string GetTaskVersion()
    {
        if(FileToggle == true)
        {
            return "file";
        }
        else
        {
            return "folder";
        }
    }
}
