namespace UOArchitect
{
    using System;
    using System.Xml;

    public class ServerListing
    {
        public string GUID;
        public string Password;
        public int Port;
        public bool SavePassword;
        public string ServerIP;
        public string ServerName;
        public string UserName;

        public ServerListing(XmlNode listingNode)
        {
            this.SavePassword = false;
            this.GUID = Guid.NewGuid().ToString();
            if (listingNode != null)
            {
                this.Deserialize(listingNode);
            }
        }

        public ServerListing(string server, string ip, int port, string username)
        {
            this.SavePassword = false;
            this.ServerName = server;
            this.ServerIP = ip;
            this.Port = port;
            this.UserName = username;
        }

        private void Deserialize(XmlNode listingNode)
        {
            this.GUID = listingNode.Attributes.GetNamedItem("id").Value;
            foreach (XmlNode node in listingNode.ChildNodes)
            {
                string str = node.Name.ToLower();
                if (str == null)
                {
                    continue;
                }
                str = string.IsInterned(str);
                if (str == "name")
                {
                    this.ServerName = node.InnerText.Trim();
                    continue;
                }
                if (str == "password")
                {
                    this.Password = node.InnerText.Trim();
                    continue;
                }
                if (str == "ip")
                {
                    this.ServerIP = node.InnerText.Trim();
                    continue;
                }
                if (str == "acct")
                {
                    this.UserName = node.InnerText.Trim();
                    continue;
                }
                if (str == "port")
                {
                    try
                    {
                        this.Port = int.Parse(node.InnerText.Trim());
                        continue;
                    }
                    catch
                    {
                        this.Port = 0;
                        continue;
                    }
                }
            }
        }

        public XmlNode Serialize(XmlDocument document)
        {
            XmlNode node = document.CreateElement("server");
            XmlAttribute attribute = document.CreateAttribute("id");
            attribute.Value = this.GUID;
            node.Attributes.Append(attribute);
            XmlNode newChild = document.CreateElement("name");
            newChild.InnerText = this.ServerName;
            node.AppendChild(newChild);
            newChild = document.CreateElement("password");
            newChild.InnerText = this.Password;
            node.AppendChild(newChild);
            newChild = document.CreateElement("ip");
            newChild.InnerText = this.ServerIP;
            node.AppendChild(newChild);
            newChild = document.CreateElement("port");
            newChild.InnerText = this.Port.ToString();
            node.AppendChild(newChild);
            newChild = document.CreateElement("acct");
            newChild.InnerText = this.UserName;
            node.AppendChild(newChild);
            return node;
        }

        public bool IsValid
        {
            get
            {
                bool flag = true;
                if ((this.ServerIP == null) || (this.ServerIP.Length == 0))
                {
                    return false;
                }
                if ((this.UserName == null) || (this.UserName.Length == 0))
                {
                    return false;
                }
                if (this.Port <= 0)
                {
                    flag = false;
                }
                return flag;
            }
        }
    }
}

