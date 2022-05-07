namespace UOArchitect
{
    using System;

    public abstract class BaseDesignAdapter
    {
        private string _filter;
        private string _title;

        private BaseDesignAdapter()
        {
        }

        public BaseDesignAdapter(string filter, string title)
        {
            this._filter = filter;
            this._title = title;
        }

        public abstract void Export(DesignData design);
        protected virtual string GetExportFileName()
        {
            return this.GetExportFileName("");
        }

        protected virtual string GetExportFileName(string defaultName)
        {
            return Utility.GetSaveFileName(this._filter, this._title, defaultName);
        }

        protected virtual string GetImportFileName()
        {
            return Utility.BrowseForFile(this._filter, this._title);
        }

        public abstract DesignData ImportDesign();

        public string filter
        {
            get
            {
                return this._filter;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
        }
    }
}

