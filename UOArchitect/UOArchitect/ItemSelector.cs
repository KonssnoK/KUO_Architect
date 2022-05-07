namespace UOArchitect
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using UOArchitectInterface;

    public class ItemSelector
    {
        public ItemsSelectedtEvent OnSelection;

        private void RaiseSelectionEvent(SelectItemsResponse response)
        {
            if (this.OnSelection != null)
            {
                this.OnSelection(response);
            }
        }

        public SelectItemsResponse SelectItems(SelectItemsRequestArgs args, bool asyncronous)
        {
            if (asyncronous)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.StartSelection), args);
                return null;
            }
            return Connection.SendSelectItemsRequest(args);
        }

        private void StartSelection(object state)
        {
            SelectItemsRequestArgs args = (SelectItemsRequestArgs) state;
            SelectItemsResponse response = Connection.SendSelectItemsRequest(args);
            this.RaiseSelectionEvent(response);
        }

        public delegate void ItemsSelectedtEvent(SelectItemsResponse response);
    }
}

