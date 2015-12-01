using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml.Linq;
using System.IO;


namespace RssDemo_forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                XmlReader FD_readxml = XmlReader.Create(textBox1.Text); // Could be the Url directly here
                
                SyndicationFeed FD_feed = SyndicationFeed.Load(FD_readxml);

                TabPage FD_tab = new TabPage(FD_feed.Title.Text);

                tabControl1.TabPages.Add(FD_tab);

                ListBox FD_list = new ListBox();

                FD_tab.Controls.Add(FD_list);

                FD_list.Dock = DockStyle.Fill;

                FD_list.HorizontalScrollbar = true;

                int event_count = 0;
                foreach (SyndicationItem FD_item in FD_feed.Items)
                {
                    FD_list.Items.Add("The Title of Event: ");
                    FD_list.Items.Add(FD_item.Title.Text);
                    
                    string address = "";
                    string venue = "";
                    string description = ((TextSyndicationContent)FD_item.Content).Text;
                    string formatedDateTime = "";
                    
                    foreach (SyndicationElementExtension extension in FD_item.ElementExtensions)
                    {
                        XElement ele = extension.GetObject<XElement>();
                     
                        if (ele.Name.LocalName == "address")
                        {
                            address = ele.Value;
                        }
                        else if (ele.Name.LocalName == "venue")
                        {
                            venue = ele.Value;
                        }
                        
                        else if (ele.Name.LocalName == "formatteddatetime")
                        {
                            formatedDateTime = ele.Value;
                        }
                        event_count++;
                    }
                    
                    FD_list.Items.Add("The Address of event is at: ");
                    FD_list.Items.Add(address);
                    FD_list.Items.Add("Description: ");
                    FD_list.Items.Add(description);
                    FD_list.Items.Add("The Date of the Event is at: ");
                    FD_list.Items.Add(formatedDateTime);
                    FD_list.Items.Add("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                }
            }
            catch
            {

            }

        }

        private void button2_Click( object sender, EventArgs e)
        {
            button2.Click += new IISHandler1();//Still not connected
        }
    }
}
//https://www.youtube.com/watch?v=6k--v1db4ug -- How to Create a Rss/Atom Feed
//https://www.trumba.com/calendars/FunInTheBurbsIllinoisTollway.xml?preview=1&guid=59dd43b4-e03f-4d0a-9544-eb18683ce136 -- Atom Feed From Tollway
//https://www.illinoisvirtualtollway.com/RSS.aspx -- Illinois Tollway Website
//http://stackoverflow.com/questions/952667/how-do-i-generate-a-kml-file-in-asp-net -- How to Generate a KML file in ASP.Net
