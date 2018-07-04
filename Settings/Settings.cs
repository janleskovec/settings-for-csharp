using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Settings
{
    class Settings
    {
        static readonly string path = "settings.conf";

        public static void SetValue(string key, string value)
        {
            string[] settings = ReadFile();
            string scannedKey = String.Copy(key);
            scannedKey.Replace('\n', '_').Replace('=', '_');

            int index = GetKeyIndex(scannedKey, settings);
            string encoded = Encode(value);

            string data = "";
            if (index == -1)
            {
                for (int i = 0; i < settings.Length; i++)
                {
                    data += settings[i] + "\n";
                }
                data += scannedKey + "=" + encoded;
            }
            else
            {
                for (int i = 0; i < settings.Length; i++)
                {
                    if (i == index)
                    {
                        data += scannedKey + "=" + encoded + "\n";
                    }
                    else
                    {
                        data += settings[i] + "\n";
                    }
                }
            }
            WriteFile(data);
        }

        public static string GetValue(string key)
        {
            string[] settings = ReadFile();
            string scannedKey = String.Copy(key);
            scannedKey.Replace('\n', '_').Replace('=', '_');

            int index = GetKeyIndex(scannedKey, settings);
            if (index >= 0)
            {
                string line = settings[index];
                int start = line.IndexOf('=') + 1;
                string decoded = Decode(line.Substring(start, line.Length - start));
                return decoded;
            }
            return "";
        }

        static string[] ReadFile()
        {
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader sr = File.OpenText(path))
                    {
                        LinkedList<string> data = new LinkedList<string>();
                        string line = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("="))
                            {
                                data.AddLast(line);
                            }
                        }
                        sr.Close();
                        return data.ToArray();
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return new string[0];
        }

        static void WriteFile(string data)
        {
            try
            {
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(data);
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static int GetKeyIndex(string key, string[] settings)
        {
            for (int i = 0; i < settings.Length; i++)
            {
                string elementKey = settings[i].Substring(0, settings[i].IndexOf('='));
                if (key == elementKey)
                {
                    return i;
                }
            }
            return -1;
        }

        static string Encode(string input)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\n')
                {
                    output += "\\n";
                }
                else if (input[i] == '\\')
                {
                    output += "\\\\";
                }
                else
                {
                    output += input[i];
                }
            }
            return output;
        }

        static string Decode(string input)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\\' && input[i + 1] == 'n')
                {
                    output += "\n";
                    i++;
                }
                else if (input[i] == '\\' && input[i + 1] == '\\')
                {
                    output += "\\";
                    i++;
                }
                else
                {
                    output += input[i];
                }
            }
            return output;
        }
    }
}
