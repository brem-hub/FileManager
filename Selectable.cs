namespace FileManager
{
    interface ISelectable
    {
        void SetActive();
    }

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