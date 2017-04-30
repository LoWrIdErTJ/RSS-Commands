using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using UBotPlugin;
using System.Linq;
using System.Windows;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Security.Cryptography;
using System.Configuration;
using System.Media;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Net;
using System.Management;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Data.OleDb;

namespace CSVtoHTML
{

    // API KEY HERE
    public class PluginInfo
    {
        public static string HashCode { get { return "e6665eee24c8cc9e2bb4438aa97593ad07bcc7af"; } }
    }

    // ---------------------------------------------------------------------------------------------------------- //
    //
    // ---------------------------------               COMMANDS               ----------------------------------- //
    //
    // ---------------------------------------------------------------------------------------------------------- //

    //
    //
    // RSS TO VARIABLE
    //
    //
    public class RSStoVAR : IUBotCommand
    {

        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public RSStoVAR()
        {
            _parameters.Add(new UBotParameterDefinition("Link to RSS", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("RSS Result", UBotType.UBotVariable));

        }

        public string Category
        {
            get { return "Data Commands"; }
        }

        public string CommandName
        {
            get { return "rss to variable"; }
        }


        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {

            string rssurl = parameters["Link to RSS"];
            string vartosave = parameters["RSS Result"];

            var reader = XmlReader.Create(rssurl);
            var feed = SyndicationFeed.Load(reader);

            string s = "";
            foreach (SyndicationItem i in feed.Items)
            {
                s += i.Title.Text + "||" + i.Summary.Text + "||" + i.PublishDate.ToString() + "||";
                foreach (SyndicationElementExtension extension in i.ElementExtensions)
                {
                    XElement ele = extension.GetObject<XElement>();
                    s += ele.Name + " :: " + ele.Value + "<br />";
                }
                s += "<hr />";
            }

            ubotStudio.SetVariable(vartosave, s.ToString());
        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

        

}
