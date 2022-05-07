namespace UOArchitect
{
    using OrbServerSDK;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Ultima;
    using UOArchitectInterface;

    public class Connection
    {
        private static bool _busy = false;
        public static ClientBusyEvent OnBusy;
        public static ConnectEvent OnConnect;
        public static DisconnectEvent OnDisconnect;
        public static ClientReadyEvent OnReady;

        public static BuildResponse BuildDesign(DesignItemCol items)
        {
            if (items.Count == 0)
            {
                return null;
            }
            Client.BringToTop();
            new BuildRequestArgs(items);
            return (BuildResponse) ExecuteRequest("UOAR_BuildDesign", new BuildRequestArgs(items));
        }

        public static bool ConnectToServer()
        {
            bool flag = false;
            LoginResult result = OrbClient.LoginToServer(Config.UserName, Config.Password, Config.ServerIP, Config.Port);
            if (result.Code == LoginCodes.Success)
            {
                flag = true;
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
                flag = false;
            }
            if (flag && (OnConnect != null))
            {
                OnConnect();
            }
            return flag;
        }

        public static void Disconnect()
        {
            OrbClient.Disconnect();
            if (OnDisconnect != null)
            {
                OnDisconnect();
            }
        }

        private static void ExecuteCommand(string alias, OrbCommandArgs args)
        {
            RaiseBusyEvent();
            OrbClient.SendCommand(alias, args);
            RaiseReadyEvent();
        }

        private static OrbResponse ExecuteRequest(string alias, OrbRequestArgs args)
        {
            RaiseBusyEvent();
            OrbResponse response = OrbClient.SendRequest(alias, args);
            RaiseReadyEvent();
            return response;
        }

        public static ExtractResponse ExtractDesign(ExtractRequestArgs args)
        {
            Client.BringToTop();
            return (ExtractResponse) ExecuteRequest("UOAR_ExtractDesign", args);
        }

        private static void RaiseBusyEvent()
        {
            if (OnBusy != null)
            {
                OnBusy();
            }
        }

        private static void RaiseReadyEvent()
        {
            if (OnReady != null)
            {
                OnReady();
            }
        }

        public static void SendDeleteItemsCommand(DeleteCommandArgs args)
        {
            if (((args != null) && (args.ItemSerials != null)) && (args.ItemSerials.Length != 0))
            {
                Client.BringToTop();
                ExecuteCommand("UOAR_DeleteItems", args);
            }
        }

        public static GetLocationResp SendGetLocationRequest(OrbRequestArgs args)
        {
            Client.BringToTop();
            return (GetLocationResp) OrbClient.SendRequest("UOAR_GetLocation", args);
        }

        public static void SendMoveItemsCommand(MoveItemsArgs args)
        {
            if (((args != null) && (args.ItemSerials != null)) && (args.ItemSerials.Length != 0))
            {
                Client.BringToTop();
                ExecuteCommand("UOAR_MoveItems", args);
            }
        }

        public static SelectItemsResponse SendSelectItemsRequest(SelectItemsRequestArgs args)
        {
            Client.BringToTop();
            return (SelectItemsResponse) OrbClient.SendRequest("UOAR_SelectItems", args);
        }

        public static bool IsBusy
        {
            get
            {
                return _busy;
            }
        }

        public static bool IsConnected
        {
            get
            {
                return OrbClient.IsConnected;
            }
        }

        public delegate void ClientBusyEvent();

        public delegate void ClientReadyEvent();

        public delegate void ConnectEvent();

        public delegate void DisconnectEvent();
    }
}

