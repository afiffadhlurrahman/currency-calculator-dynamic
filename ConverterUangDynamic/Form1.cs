using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConverterUangDynamic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double getRate(string fromCurrency, string toCurrency)
        {
            var json = "";
            double rate;
            try
            {
                string url = string.Format("https://free.currconv.com/api/v7/convert?q={0}_{1}&compact=ultra&apiKey=76c71c81f6c0b2424e9c", fromCurrency.ToUpper(), toCurrency.ToUpper());
                string key = string.Format("{0}_{1}", fromCurrency.ToUpper(), toCurrency.ToUpper());
                
                json = new WebClient().DownloadString(url);
                dynamic stuff = JsonConvert.DeserializeObject(json);
                rate = stuff[key];
            }
            catch
            {
                rate = 0;
            }
            return rate;
        }
        /*
        private string getSimbol(string key)
        {
            var json_logo = "";
            string logo_out = "";
            try
            {
                string url_logo = string.Format("https://free.currconv.com/api/v7/currencies?apiKey=76c71c81f6c0b2424e9c");
                
                json_logo = new WebClient().DownloadString(url_logo);
                dynamic stuff_logo = JsonConvert.DeserializeObject(json_logo);
                
                logo_out = stuff_logo["results"][key]["currencySymbol"].ToString();
                MessageBox.Show("isinya  " + logo_out);
            }
            catch
            {
                Console.WriteLine("Error");
            }
            return logo_out;
        }
        */
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double hasilConvert;
                string finfromVal = "", fintoVal = "";
                string fromVal = comboBox1.Text;
                string toVal = comboBox2.Text;
                //string simbol = "";

                int posFrom = fromVal.IndexOf("(") + 1;
                int posTo = toVal.IndexOf("(") + 1;

                //MessageBox.Show("aman? " + posTo.ToString() + " " + toVal.Substring(posTo, 3));
                finfromVal = fromVal.Substring(posFrom, 3);
                fintoVal = toVal.Substring(posTo, 3);

                //Console.WriteLine(fromVal);

                double rate = getRate(finfromVal, fintoVal);
                hasilConvert = Math.Round(rate * double.Parse(textBox1.Text), 3);
                
                // simbol = getSimbol(fintoVal);
                //MessageBox.Show(simbol);
                //MessageBox.Show("HASIL : " + hasilConvert.ToString() + simbol);
                textBox2.Text = hasilConvert.ToString();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Nilai yang dimasukkan harus integer");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Currency from harus dipilih");
            }
            catch ( Exception ex)
            {
                MessageBox.Show("ERROR BOS  " + ex);
            }
        }

    }
}
