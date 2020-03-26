using Acr.UserDialogs;
using Newtonsoft.Json;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace _13033
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
                string[] data = File.ReadAllText(fileName).Split(',');
                name.Text = data[0];
                address.Text = data[1];
                Message.Text = "";
            }
            catch (Exception e)
            {

                name.Text = "";
                address.Text = "";
            }
        }

        private void Send_Clicked(object sender, EventArgs e)
        {
            string reason_num;


            try
            {
                 reason_num = reason.SelectedItem.ToString().Split('.')[0];
            }
            catch (Exception )
            {
                reason_num = "";
            }
            if (name.Text == "" || address.Text==""|| reason_num=="")
            {
                UserDialogs.Instance.Alert("Συμπληρώστε όλα τα πεδία", "ΠΡΟΣΟΧΗ"); 
            }
            else
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
                File.WriteAllText(fileName, name.Text + "," + address.Text);

                var smsMessanger = CrossMessaging.Current.SmsMessenger;
                smsMessanger.SendSms("13033", reason_num + " " + name.Text + " " + address.Text);
            }
            
        }
    }
}
