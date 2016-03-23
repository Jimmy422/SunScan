using System;
//
public class NMAPScan
{

	public NMAPScan()
	{
        // ReadCommands("test1.txt", "nmapTest.xml");
    }

    /// <summary>
    /// This is the main function you want to 
    /// call, input the file name that contains 
    /// the command you are using and it will
    /// output an xml file
    /// example: 
    ///      ReadCommands("test1.txt", "nmapTest.xml");
    /// </summary>
    /// <param name="fileName"></param>
    public static void ReadCommands(string fileName, string outputFile)
    {
        System.Collections.Generic.List<string> cmds = new System.Collections.Generic.List<string>();
        string line = "";
        using (System.IO.StreamReader rdr = new System.IO.StreamReader(fileName))
        {
            while ((line = rdr.ReadLine()) != null)
                cmds.Add("/C " + line);
        }

        foreach (string s in cmds)
            runCMD(s, outputFile);
    }

    /// <summary>
    /// takes care of the actual cmd process
    /// outputs the xml file
    /// </summary>
    /// <param name="command"></param>
    /// <param name="outputFileName"></param>
    static void runCMD(string command, string outputFileName)
    {
        //Console.WriteLine(command);
        System.Diagnostics.ProcessStartInfo prIn = new System.Diagnostics.ProcessStartInfo("cmd.exe");
        prIn.Arguments = command;
        prIn.UseShellExecute = false;
        prIn.CreateNoWindow = true;
        prIn.RedirectStandardOutput = true;
        System.Diagnostics.Process pr = new System.Diagnostics.Process();
        pr.StartInfo = prIn;
        pr.Start();

        using (System.IO.StreamReader rdr = pr.StandardOutput)
        {
            //Console.WriteLine("started process");
            string output = rdr.ReadToEnd();
            //Console.WriteLine(output);
            WriteFile(output, outputFileName);
        }

        pr.WaitForExit();
    }

    //easier to just call the streamreader using this function
    static void WriteFile(string line, string fileName)
    {
        using (System.IO.StreamWriter wr = new System.IO.StreamWriter(fileName))
            wr.WriteLine(line);
    }
}

