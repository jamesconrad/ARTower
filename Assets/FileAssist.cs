using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class FileAssist : MonoBehaviour
{
    private StreamReader ifile;
    private StreamWriter ofile;

    public void OpenFile(string filepath, bool readOnly)
    {
        if (readOnly)
            ifile = new StreamReader(filepath);
        else
            ofile = new StreamWriter(filepath);
    }

    public List<string> ReadAllToMemory()
    {
        List<string> filebyline = new List<string>();

        while (ifile.Peek() >= 0)
            filebyline.Add(ifile.ReadLine());

        return filebyline;
    }
}
