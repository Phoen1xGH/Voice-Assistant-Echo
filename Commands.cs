using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VAEcho
{
    /// <summary>
    /// Class for working with application list
    /// </summary>
    internal class Commands
    {
        /// <summary>
        /// Command set
        /// </summary>
        private readonly Dictionary<string, string> _commandDictionary = new Dictionary<string, string>();

        public Commands() 
        {
            FileParse();
        }

        /// <summary>
        /// Getting command set
        /// </summary>
        /// <returns>Command set obtained from the file</returns>
        public Dictionary<string, string> GetCommand()
        {
            return _commandDictionary;
        }

        /// <summary>
        /// Method that parse a file and fill command set
        /// </summary>
        private void FileParse()
        {
            string fileName = "commands.txt";

            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);

            foreach (var line in lines)
            {
                string[] parse = line.Split(',');
                _commandDictionary.Add(parse[0], parse[1]);
            }
        }
    }
}
