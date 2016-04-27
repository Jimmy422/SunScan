using SunScan;
using System;
using System.ComponentModel;

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

        try
        {
            using (System.IO.StreamReader rdr = new System.IO.StreamReader(fileName))
            {
                while ((line = rdr.ReadLine()) != null)
                    cmds.Add("/C " + line);
            }

            foreach (string s in cmds)
                runCMD(s, outputFile);
        }
        catch(Exception e)
        {
            WriteFile(e.StackTrace, "ERRORexception.txt");
        }
    }

    /// <summary>
    /// takes care of the actual cmd process
    /// outputs the xml file
    /// </summary>
    /// <param name="command"></param>
    /// <param name="outputFileName"></param>
    static void runCMD(string command, string outputFileName)
    {
        Console.WriteLine(command);
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
            Console.WriteLine("started process");
            string output = rdr.ReadToEnd();
            Console.WriteLine(output);
            WriteFile(output, outputFileName);
        }

        pr.WaitForExit();
    }

    //easier to just call the streamreader using this function
    public static void WriteFile(string line, string fileName)
    {
        using (System.IO.StreamWriter wr = new System.IO.StreamWriter(fileName))
            wr.WriteLine(line);
    }

    public static void GetIPConfig(string outputFile)
    {
        ReadCommands("ipfind.txt", "ipconf.txt");

        string IPv4 = "";
        string subnetMask = "";
        string defaultGateway = "";
        using (System.IO.StreamReader rd = new System.IO.StreamReader("ipconf.txt"))
        {
            string line = "";

            while((line = rd.ReadLine()) != null)
            {
                if(line.Contains("IPv4 Address"))
                {
                    string [] lines = line.Split(' ');
                    IPv4 = lines[lines.Length - 1];
                }
                
                if(line.Contains("Subnet Mask"))
                {
                    string [] lines = line.Split(' ');
                    subnetMask = lines[lines.Length - 1];
                }

                if (line.Contains("Default Gateway") && (defaultGateway == ""))
                {
                    string[] lines = line.Split(' ');
                    defaultGateway = lines[lines.Length - 1];
                }
            }
        }
        //finish here
        //string range = "nmap -p 135 -oX " + xmlFile + " - 10.226.20.*";
        (App.Current as App).scanGateway = defaultGateway;

        string range = "nmap -p 135 -oX - " + GetRange(IPv4, subnetMask);
        //string range = "nmap -p 135 -oX - 10.226.20.*";
        WriteFile(range, outputFile);
    }

    //TODO it returns a string right now, maybe change to void?
    public static string GetRange(string ip4, string mask)
    {
        string[] ips = ip4.Split('.');
        string[] masks = mask.Split('.');

        for (int i = 0; i < masks.Length; ++i)
        {
            if (masks[i] != "255")
            {
                int change = 255 - int.Parse(masks[i]);
                int intIp = int.Parse(ips[i]);

                int lowChange = intIp - change;

                if (lowChange < 0)
                    lowChange = 0;

                int highChange = intIp + change;

                ips[i] = lowChange + "-" + highChange;
             
            }

            if (masks[i] == "0")
            {
                ips[i] = "*";
                break;
            }
        }

        string toWrite = "";

        //toWrite += "<";
        for (int i = 0; i < ips.Length; ++i)
        {
            toWrite += ips[i];

            if (i < ips.Length - 1)
                toWrite += ".";
        }
        //toWrite += ">";

        return toWrite;
    }


    //this is for debugging only!
    public static void RunTests()
    {

        System.Collections.Generic.List<string> outs = new System.Collections.Generic.List<string>();

        using (System.IO.StreamReader rd = new System.IO.StreamReader("testInput.txt"))
        {
            string line = "";

            while ((line = rd.ReadLine()) != null)
            {
                string[] parts = line.Split(' ');

                outs.Add(NMAPScan.GetRange(parts[0], parts[1]));
            }
        }

        using (System.IO.StreamWriter wr = new System.IO.StreamWriter("testOutput.txt"))
        {
            foreach (string s in outs)
                wr.WriteLine(s);
        }

    }
}

