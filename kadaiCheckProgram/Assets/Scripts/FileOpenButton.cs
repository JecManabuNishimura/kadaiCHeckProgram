using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class FileOpenButton : MonoBehaviour
{
    [SerializeField]
    private InputField filePath;
    public void BrowseFolder()
    {
        using (var ofd = new OpenFileDialog() { FileName = "SelectFolder", Filter = "Folder|.", CheckFileExists = false })
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath.text = Path.GetDirectoryName(ofd.FileName) + @"\";
            }
        }
    }

    public void TextFlolder()
    {
        using (var ofd = new OpenFileDialog() { FileName = "SelectFolder", Filter = "csvファイル|*.csv", CheckFileExists = false })
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath.text = ofd.FileName;
            }
        }
    }
}
