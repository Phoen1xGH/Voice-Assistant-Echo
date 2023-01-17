using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using NAudio.Wave;

namespace VAEcho
{
    /// <summary>
    /// Request processing class 
    /// </summary>
    internal static class UserRequest
    {
        #region Fields

        static WaveIn _waveIn;
        static WaveFileWriter _writer;
        static readonly string OutputFilename = "demo.wav";//file for recognition

        #endregion

        #region Recognition Methods

        /// <summary>
        /// Start of recording
        /// </summary>
        [Obsolete]
        public static void Start()
        {
            _waveIn = new WaveIn();
            _waveIn.DeviceNumber = 0;//default device
            _waveIn.DataAvailable += WaveInDataAvailable;
 
            _waveIn.RecordingStopped += WaveInRecordingStopped;
            _waveIn.WaveFormat = new WaveFormat(16000, 1);
            _writer = new WaveFileWriter(OutputFilename, _waveIn.WaveFormat);

            _waveIn.StartRecording();

            //request time limit (5 seconds)
            var timer = new Timer(Stop, null, 5000, Timeout.Infinite);
        }


        
        [Obsolete]
        static void WaveInDataAvailable(object sender, WaveInEventArgs e)
        {
            _writer.WriteData(e.Buffer, 0, e.BytesRecorded);
        }

        /// <summary>
        /// Stop of recording
        /// </summary>
        /// <param name="o"></param>
        [Obsolete]
        static void Stop(object o)
        {
            _waveIn.StopRecording();
            _writer.Close();
            Recognizing();
        }

        static void WaveInRecordingStopped(object sender, StoppedEventArgs stoppedEventArgs)
        {
            _waveIn.Dispose();
            _waveIn = null;
            _writer.Close();
            _writer = null;
        }

        /// <summary>
        /// Request recognition and execution
        /// </summary>
        [Obsolete]
        private static void Recognizing()
        {
            //converting speech to text using the API
            WebRequest request = WebRequest.Create("https://www.google.com/speech-api/v2/recognize?output=json&lang=ru-RU&key=AIzaSyBOti4mM-6x9WDnZIjIeyEU21OpBXqWBgw");
            request.Method = "POST";
            byte[] byteArray = File.ReadAllBytes(OutputFilename);
            request.ContentType = "audio/l16; rate=16000";
            request.ContentLength = byteArray.Length;
            request.GetRequestStream().Write(byteArray, 0, byteArray.Length);


            //get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(response.GetResponseStream());
            
            //read the content.
            string content = reader.ReadToEnd();
            var rg = new Regex(@"transcript" + '"' + ":" + '"' + "([A-Z, А-Я, a-z,а-я, ,0-9]*)");//regular expression
                                                                                                        //for recognition
            var result = rg.Match(content).Groups[1].Value; //recognized text
            //internet search
            if (result.ToUpper().Contains("НАЙДИ"))
            {
                string searchQuery = result;
                searchQuery = searchQuery.Replace("Найди ", "");
                searchQuery = searchQuery.Replace("найди ", "");
                Process.Start("https://yandex.ru/search/?text=" + searchQuery);
            }
            //run an applications/opening files
            else if (result.ToUpper().Contains("ЗАПУСТИ"))
            {
                string searchQuery = result;
                searchQuery = searchQuery.ToUpper().Replace("ЗАПУСТИ ", "");
                Commands com = new Commands();
                try
                {
                    Process.Start(com.GetCommand()[searchQuery]);//run/open
                }
                catch
                {
                    MessageBox.Show("Команда не распознанна.\nПовторите попытку.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
            reader.Close();
            response.Close();
            
        }
        #endregion
    }
}
