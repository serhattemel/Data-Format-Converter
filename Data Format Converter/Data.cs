using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Format_Converter
{
    internal class Data
    {
        private static String inputText = @"";
        private static String outputText = @"";
        private static String baseInputFormat = "";
        private static String convertedInputFormat = "";
        

        public string InputText {
            get => inputText;
            set => inputText = value;
        }
        
        public string OutputText
        {
            get => outputText;
            set => outputText = value;
        }

        public string BaseInputFormat
        {
            get => baseInputFormat;
            set => baseInputFormat = value;
        }

        public string ConvertedInputFormat
        {
            get => convertedInputFormat;
            set => convertedInputFormat = value;
        }
    }
}
