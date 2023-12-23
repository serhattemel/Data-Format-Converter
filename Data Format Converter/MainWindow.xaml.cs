using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Data_Format_Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Data data = new Data();
        Conversion conversion = new Conversion();
        public MainWindow()
        {
            InitializeComponent();
            Window window = this;
            window.ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


        }

        private void solTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            data.InputText = solTextBox.Text;
        }

        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            string input = data.InputText;
            if (data.InputText == "" || data.InputText == null)
            {
                MessageBox.Show("Lütfen dönüştürülecek veriyi giriniz.");
                return;
            }
            switch (data.BaseInputFormat)
            {
                case "JSON":
                    if (data.ConvertedInputFormat == "XML")
                        sagTextBox.Text = conversion.ConvertJSONToXML(input);
                    else if (data.ConvertedInputFormat == "XSD")
                        sagTextBox.Text = conversion.ConvertJSONToXSD(input);
                    else
                        MessageBox.Show("Lütfen dönüştürülecek verinin formatını seçiniz.");
                    break;
                case "XML":
                    if (data.ConvertedInputFormat == "JSON")
                        sagTextBox.Text = conversion.ConvertXMLToJSON(input);
                    else if (data.ConvertedInputFormat == "XSD")
                        sagTextBox.Text = conversion.ConvertXMLToXSD(input);
                    else
                        MessageBox.Show("Lütfen dönüştürülecek verinin formatını seçiniz.");
                    break;
                case "XSD":
                    if (data.ConvertedInputFormat == "JSON")
                        sagTextBox.Text = conversion.ConvertXSDToJSON(input);
                    else if (data.ConvertedInputFormat == "XML")
                        sagTextBox.Text = conversion.ConvertXSDToXML(input);
                    else
                        MessageBox.Show("Lütfen dönüştürülecek verinin formatını seçiniz.");
                    break;
                default:
                    MessageBox.Show("Lütfen dönüştürülecek verinin formatını seçiniz.");
                    break;
            }

            

        }
        private void JSON_Checked(object sender, RoutedEventArgs e)
        {
            data.BaseInputFormat = "JSON";
            
            if(toJSON.IsChecked==true)
                toJSON.IsChecked = false;
            
            UncheckBoxes("left");
        }

        private void XML_Checked(object sender, RoutedEventArgs e)
        {
            data.BaseInputFormat = "XML";

            if (toXML.IsChecked == true)
                toXML.IsChecked = false;

            UncheckBoxes("left");
        }

        private void XSD_Checked(object sender, RoutedEventArgs e)
        {
            data.BaseInputFormat = "XSD";

            if (toXSD.IsChecked == true)
                toXSD.IsChecked = false;
            
            UncheckBoxes("left");
        }

        private void toJSON_Checked(object sender, RoutedEventArgs e)
        {
            data.ConvertedInputFormat = "JSON";

            if (JSON.IsChecked == true)
                JSON.IsChecked = false;
            
            UncheckBoxes("right");
        }

        private void toXML_Checked(object sender, RoutedEventArgs e)
        {
            data.ConvertedInputFormat = "XML";

            if (XML.IsChecked == true)
                XML.IsChecked = false;
            
            UncheckBoxes("right");
        }
        private void toXSD_Checked(object sender, RoutedEventArgs e)
        {
            data.ConvertedInputFormat = "XSD";

            if (XSD.IsChecked == true)
                XSD.IsChecked = false;

            UncheckBoxes("right");
        }

        private void switchButton_Click(object sender, RoutedEventArgs e)
        {
            if (data.InputText != null || data.InputText != "" && data.OutputText != null || data.OutputText != "")
            
            {
                string temp = data.BaseInputFormat;
                data.BaseInputFormat = data.ConvertedInputFormat;
                data.ConvertedInputFormat = temp;
                string temp2 = solTextBox.Text;
                solTextBox.Text = sagTextBox.Text;
                sagTextBox.Text = temp2;
                UncheckBoxes("left");
                UncheckBoxes("right");
            }
        }
        private void UncheckBoxes(String side)
        {
            if (side == "left")
            {
                switch (data.BaseInputFormat)
                {
                    case "JSON":
                        XML.IsChecked = false;
                        XSD.IsChecked = false;
                        break;
                    case "XML":
                        JSON.IsChecked = false;
                        XSD.IsChecked = false;
                        break;
                    case "XSD":
                        JSON.IsChecked = false;
                        XML.IsChecked = false;
                        break;
                }
            }
            else if (side == "right")
            {
                switch (data.ConvertedInputFormat)
                {
                    case "JSON":
                        toXML.IsChecked = false;
                        toXSD.IsChecked = false;
                        break;
                    case "XML":
                        toJSON.IsChecked = false;
                        toXSD.IsChecked = false;
                        break;
                    case "XSD":
                        toJSON.IsChecked = false;
                        toXML.IsChecked = false;
                        break;
                }
            }
        
        }


        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(sagTextBox.Text);
        }

        private void saveAsButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();

            save.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            save.FilterIndex = 2;
            save.FileName = "ConvertedFile."+ data.ConvertedInputFormat.ToLower();
            save.ShowDialog();
            File.WriteAllText(save.FileName, sagTextBox.Text);
            
        }
    }
}
