namespace UOArchitect
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Xml;

    public class ServerListingCol : CollectionBase
    {
        public int Add(ServerListing listing)
        {
            return base.List.Add(listing);
        }

        private void Deserialize(XmlNodeList serverNodeList)
        {
            foreach (XmlNode node in serverNodeList)
            {
                base.List.Add(new ServerListing(node));
            }
        }

        public ServerListing GetListingByID(string id)
        {
            for (int i = 0; i < base.List.Count; i++)
            {
                ServerListing listing = (ServerListing) base.List[i];
                if (listing.GUID == id)
                {
                    return listing;
                }
            }
            return null;
        }

        public void LoadListings(XmlNodeList serverNodeList)
        {
            base.Clear();
            this.Deserialize(serverNodeList);
        }

        public void Remove(ServerListing listing)
        {
            base.List.Remove(listing);
        }

        public void SaveListings(XmlDocument document, XmlNode serversNode)
        {
            foreach (ServerListing listing in base.List)
            {
                serversNode.AppendChild(listing.Serialize(document));
            }
        }

        public ServerListing this[int index]
        {
            get
            {
                return (ServerListing) base.List[index];
            }
        }
    }
}

