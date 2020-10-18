namespace FileManager
{
    /// <summary>
    /// This interface is needed for any selectable GUI element.
    /// It can be Buttons, Items of list, Menus.
    /// </summary>
    /// TODO: Add function that allows only one element to be Active.
    interface ISelectable
    {
        void SetActive();
        void SetInactive();
    }

    /// <summary>
    /// Class used for listing dirs and files and allows to create pointer * that shows currently chosen Item.
    /// </summary>
    class Item : ISelectable
    {
        private bool _active;
        private bool _isFile;

        public bool Active
        {
            get { return _active; }
            private set { _active = value; }
        }

        public bool IsFile
        {
            get { return _isFile; }
            set { _isFile = value; }
        }
        //public bool Active { get; set; }
        public string Content { get; }

        public Item(string content, bool isFile = false, bool active = false)
        {
            Content = content;
            _active = active;
            _isFile = isFile;
        }

        public void SetActive()
        {
            Active = true;
        }

        public void SetInactive()
        {
            Active = false;
        }
    }
}