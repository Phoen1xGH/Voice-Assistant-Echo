using System;
using System.Globalization;
using Microsoft.Speech.Recognition;

namespace VAEcho
{
    /// <summary>
    /// Class for handling calls to the assistant
    /// </summary>
    internal static class EchoCall
    {
        /// <summary>
        /// Method for checking the Echo call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obsolete]
        static void SreSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence > 0.5)
            {
                UserRequest.Start();//start request recognition
            }
        }

        /// <summary>
        /// Call recognition method
        /// </summary>
        public static void EchoRecognize()
        {
            var ci = new CultureInfo("ru-ru");
            var sre = new SpeechRecognitionEngine(ci);
            sre.SetInputToDefaultAudioDevice();

            sre.SpeechRecognized += SreSpeechRecognized;


            var numbers = new Choices();
            numbers.Add("Слушай Эхо");//echo call keyword


            var gb = new GrammarBuilder
            {
                Culture = ci
            };
            gb.Append(numbers);


            var g = new Grammar(gb);
            sre.LoadGrammar(g);

            sre.RecognizeAsync(RecognizeMode.Multiple);
        }
    }
}
