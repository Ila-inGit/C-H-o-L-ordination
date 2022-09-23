using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;
public class Audio : MonoBehaviour
{


    public void Pressed()
    {
        PythonBoom();
    }


    private void PythonBoom()
    {

        // 1) Create Process Info
        var psi = new ProcessStartInfo();
        psi.FileName = "C:/Users/Ilaria/AppData/Local/Microsoft/WindowsApps/PythonSoftwareFoundation.Python.3.9_qbz5n2kfra8p0/python.exe";

        // 2) Provide script and arguments
        var script = @"D:\POLI_sw\Unity\projects\MixTry\Assets\Audio\prova_audio.py";

        var arg = "";

        psi.Arguments = string.Format("{0} {1}", script, arg);

        // 3) Process configuration
        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        // 4) Execute process and get output
        var errors = "";
        // var results = "";

        using (Process process = Process.Start(psi))
        {

            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Console.Write(result);

            }
            errors = process.StandardError.ReadToEnd();
            // results = process.StandardOutput.ReadToEnd();
            // Console.Write(results);
            process.WaitForExit();

        }
        readFromFile();
    }

    void readFromFile()
    {
        string text = System.IO.File.ReadAllText("D:/POLI_sw/Unity/projects/MixTry/Assets/MyOutput.txt");
    }
}
