using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace FileManager
{
    //===========================================
    //            Exception definitions
    //===========================================
    /// <summary>
    ///     Exception that is called when file has wrong extension.
    /// </summary>
    public class ExtensionException : Exception
    {
        public ExtensionException()
        {
        }

        public ExtensionException(string message) : base(message)
        {
        }

        public ExtensionException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <summary>
    ///     Exception that is called when copy operation is used on a folder.
    /// </summary>
    public class CopyFolderException : Exception
    {
        public CopyFolderException()
        {
        }

        public CopyFolderException(string message) : base(message)
        {
        }

        public CopyFolderException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <summary>
    ///     Exception that is called when any operation is used on a folder.
    /// </summary>
    public class FolderFileException : Exception
    {
        public FolderFileException()
        {
        }

        public FolderFileException(string message) : base(message)
        {
        }

        public FolderFileException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    internal class FileManager
    {
        private readonly string[] _readableExtensions = { "txt", "cs", "cpp", "py", "js", "html", "css", "rtf" };

        // Drawer Engine instance.
        private Drawer _drawer;

        // Current encoding.
        private Encoding _encoding;
        private FileInfo _fileConc;

        // Files that are used as buffers for operation.
        private FileInfo _fileCopy;
        private FileInfo _fileMove;

        // Path of file from buffer. (Can be removed)
        private string _prevPath;

        // Current folder path.
        private DirectoryInfo _root;

        /// <summary> Default ctor. </summary>
        public FileManager()
        {
            _encoding = Encoding.UTF8;

            _drawer = new Drawer();
        }

        /// <summary>
        ///     Method that initiates manager.
        /// </summary>
        public void Init()
        {
            _drawer = new Drawer();
            _drawer.DrawIntro();
            
            //Choose the language
            string language;
            var data = Console.ReadKey();
            switch (data.Key)
            {
                case ConsoleKey.D1:
                    language = "rus";
                    break;
                case ConsoleKey.D2:
                    language = "eng";
                    break;
                // if any other key is pressed - exit
                default:
                    Exit();
                    return;
            }
            
            _drawer.SetLanguage(language);
            Menu();
        }

        /// <summary>
        ///     Method that launches main menu.
        /// </summary>
        private void Menu()
        {
            var check = true;
            do
            {
                //TODO: fix bug: when return from the disk - top of the screen is ruined
                _drawer.DrawMainMenu();

                var data = Console.ReadKey(true);
                switch (data.KeyChar)
                {
                    // Choose disk.
                    case '1':
                        ChooseDisk();
                        break;
                    // Read instruction.
                    case '2':
                        Instruction();
                        break;
                    // Exit.
                    case '3':
                        Exit();
                        check = false;
                        break;
                        // If any other key is pressed - rerun menu.
                }
            } while (check);
        }

        private void Instruction()
        {
            _drawer.DrawInstruction();
        }

        /// <summary>
        ///     Method that chooses disk and starts manager.
        /// </summary>
        private void ChooseDisk()
        {
            var drives = DriveInfo.GetDrives();
            var check = false;
            ConsoleKeyInfo key;
            do
            {
                _drawer.DrawDriveChooser(drives);

                key = Console.ReadKey(true);
                if (uint.TryParse(key.KeyChar.ToString(), out var result) && result != 0 && result <= drives.Length ||
                    key.Key == ConsoleKey.Escape)
                {
                    if (key.Key == ConsoleKey.Escape)
                        return;
                    check = true;
                    _root = drives[result - 1].RootDirectory;
                }
            } while (!check);

            RunManager(false);
        }

        /// <summary>
        ///     Method that prints all folders and allows user to choose an option.
        /// </summary>
        /// <param name="exit"> bool value for recursion </param>
        private void RunManager(bool exit)
        {
            // If Escape was pressed in depth recursion - exit.
            if (exit)
                return;

            var currentDir = _root;
            var dirs = new List<string>();
            var files = new List<string>();

            var currentActive = 0;
            var items = UpdateItems(currentDir, ref files, ref dirs, ref currentActive);

            // Infinite cycle, until Escape is pressed.
            while (true)
            {
                DrawMainView(items, currentDir);

                var key = Console.ReadKey();
                switch (key.Key)
                {
                    // Go down 1 item.
                    case ConsoleKey.DownArrow:
                        IncreaseIterator(ref items, ref currentActive);
                        break;
                    // Go up 1 item.
                    case ConsoleKey.UpArrow:
                        DecreaseIterator(ref items, ref currentActive);
                        break;
                    // Interact.
                    case ConsoleKey.Enter:
                        if (!items[currentActive].IsFile)
                        {
                            _root = new DirectoryInfo(Path.Combine(_root.FullName, items[currentActive].Content));
                            items[currentActive].SetInactive();
                            RunManager(exit);
                        }
                        else
                        {
                            ReadFile(items, currentActive);
                            break;
                        }

                        return;
                    // Copy.
                    case ConsoleKey.C:
                        CopyFile(items, currentActive);
                        items = UpdateItems(currentDir, ref files, ref dirs, ref currentActive);
                        break;
                    // Delete.
                    case ConsoleKey.D:
                        DeleteFile(items, currentActive);
                        items = UpdateItems(currentDir, ref files, ref dirs, ref currentActive);
                        break;
                    // Move.
                    case ConsoleKey.M:
                        MoveFile(items, currentActive);
                        items = UpdateItems(currentDir, ref files, ref dirs, ref currentActive);
                        break;
                    // Concatenate
                    case ConsoleKey.K:
                        ConcatenateFile(items, currentActive);
                        items = UpdateItems(currentDir, ref files, ref dirs, ref currentActive);
                        break;
                    // Cancel any operation and clear buffers.
                    case ConsoleKey.X:
                        RevertOptions();
                        break;
                    // New file.
                    case ConsoleKey.N:
                        NewFile(items, currentActive);
                        items = UpdateItems(currentDir, ref files, ref dirs, ref currentActive);
                        break;
                    // Encoding.
                    case ConsoleKey.E:
                        ChangeEncoding();
                        break;
                    // Exit.
                    case ConsoleKey.Escape:
                        exit = true;
                        return;
                }
            }
        }

        /// <summary>
        ///     Method draws main menu of manager with options.
        ///     If any buffer is filled - it shows info about it.
        /// </summary>
        /// <param name="items"> dirs and files </param>
        /// <param name="currentDir"> iterator over items </param>
        private void DrawMainView(List<Item> items, DirectoryInfo currentDir)
        {
            if (_fileCopy != null)
                _drawer.DrawDirFileSelection(items, currentDir, _fileCopy.Name);
            else if (_fileMove != null)
                _drawer.DrawDirFileSelection(items, currentDir, moveFile: _fileMove.Name);
            else if (_fileConc != null)
                _drawer.DrawDirFileSelection(items, currentDir, concFile: _fileConc.Name);
            else
                _drawer.DrawDirFileSelection(items, currentDir);
        }

        /// <summary>
        ///     Method that empties buffers.
        /// </summary>
        private void RevertOptions()
        {
            _prevPath = "";
            _fileConc = null;
            _fileMove = null;
            _fileCopy = null;
        }

        /// <summary>
        ///     Method that does concatenation.
        ///     If _fileConc is null it saves current file to buffer
        ///     else it concatenates buffered file(in _fileConc) with current
        ///     and saves it to buffered file.
        /// </summary>
        /// <param name="items"> dirs and files </param>
        /// <param name="currentItem"> iterator over items </param>
        private void ConcatenateFile(List<Item> items, int currentItem)
        {
            // Checks for the other not null buffers.
            if (_fileMove != null)
            {
                _drawer.DrawError(new Exception("move"), items[currentItem].Content);
                return;
            }

            if (_fileCopy != null)
            {
                _drawer.DrawError(new Exception("copy"), items[currentItem].Content);
                return;
            }

            if (_fileConc == null)
            {
                if (items[currentItem].IsFile)
                {
                    _prevPath = Path.Combine(_root.FullName, items[currentItem].Content);
                    _fileConc = new FileInfo(_prevPath);
                    _drawer.DrawConcOk(items[currentItem].Content);
                }
                else
                {
                    _drawer.DrawError(new FolderFileException(), items[currentItem].Content);
                }
            }
            else
            {
                try
                {
                    var sr = _fileConc.AppendText();
                    var path = Path.Combine(_root.FullName, items[currentItem].Content);
                    var text = File.ReadAllText(path);
                    sr.Write(text);
                    sr.Close();
                    _fileConc = null;
                    _prevPath = "";
                }
                catch (Exception e)
                {
                    _drawer.DrawError(e, _prevPath);
                    _fileConc = null;
                    _prevPath = "";
                }
            }
        }

        /// <summary>
        ///     Method that creates new file in current folder
        ///     and adds text to it.
        /// </summary>
        /// <param name="items"> dirs and files </param>
        /// <param name="currentItem"> iterator over items </param>
        private void NewFile(List<Item> items, int currentItem)
        {
            string newPath;
            var interrupted = false;
            // Get new file name.
            do
            {
                _drawer.DrawNewFileNameChoose();
                var fileName = ReadLineWithInterrupt(ref interrupted);
                newPath = Path.Combine(_root.FullName, fileName ?? string.Empty);
            } while (!interrupted && (!Path.HasExtension(newPath) ||
                                      !_readableExtensions.Contains(Path.GetExtension(newPath).Remove(0, 1))));

            // If user pressed Escape - exit
            if (interrupted)
                return;

            interrupted = false;
            // Input lines to file.
            var allText = "";
            do
            {
                _drawer.DrawAddText(Path.GetFileName(newPath), allText);
                allText += ReadLineWithInterrupt(ref interrupted) + Environment.NewLine;
            } while (!interrupted);

            try
            {
                File.WriteAllText(newPath, allText, _encoding);
            }
            catch (Exception e)
            {
                _drawer.DrawError(e, newPath);
            }
        }


        /// <summary>
        ///     Function that reads line with possible interruption from user.
        /// </summary>
        /// <param name="flag"> if was interrupted return true </param>
        /// <param name="interrupt"> key that is checked as interruptor </param>
        /// <returns></returns>
        private string ReadLineWithInterrupt(ref bool flag, ConsoleKey interrupt = ConsoleKey.Escape)
        {
            var line = "";
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == interrupt || key.Key == ConsoleKey.Enter)
                {
                    if (key.Key == interrupt)
                        flag = true;
                    return line;
                }

                if (key.Key == ConsoleKey.Backspace)
                {
                    try
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        line = line.Remove(line.Length - 1, 1);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                }
                else
                {
                    line += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
        }

        /// <summary>
        ///     Method that moves file from one location to another.
        ///     If _fileMove is null it saves current file to buffer
        ///     else it moves file from buffer to destination folder.
        /// </summary>
        /// <param name="items"> dirs and files </param>
        /// <param name="currentItem"> iterator over items </param>
        private void MoveFile(List<Item> items, int currentItem)
        {
            // Checks buffers.
            if (_fileCopy != null)
            {
                _drawer.DrawError(new Exception("copy"), items[currentItem].Content);
                return;
            }

            if (_fileConc != null)
            {
                _drawer.DrawError(new Exception("conc"), items[currentItem].Content);
                return;
            }

            if (_fileMove == null)
            {
                if (items[currentItem].IsFile)
                {
                    _prevPath = Path.Combine(_root.FullName, items[currentItem].Content);
                    _fileMove = new FileInfo(_prevPath);
                    _drawer.DrawMoveOk(items[currentItem].Content);
                }
            }
            else
            {
                try
                {
                    _fileMove.MoveTo(Path.Combine(_root.FullName, _fileMove.Name));
                    _fileMove = null;
                    _prevPath = "";
                }
                catch (Exception e)
                {
                    _drawer.DrawError(e, _prevPath);
                    _fileMove = null;
                    _prevPath = "";
                }
            }
        }

        /// <summary>
        ///     Method that deletes current file.
        ///     Can delete only files from _readableExtensions
        /// </summary>
        /// <param name="items"> dirs and files </param>
        /// <param name="currentItem">iterator over items </param>
        private void DeleteFile(List<Item> items, int currentItem)
        {
            if (!items[currentItem].IsFile)
            {
                _drawer.DrawError(new FolderFileException(), items[currentItem].Content);
                return;
            }

            var extension = Path.GetExtension(items[currentItem].Content);
            if (extension != null && _readableExtensions.Contains(extension.Remove(0, 1)))
                try
                {
                    File.Delete(Path.Combine(_root.FullName, items[currentItem].Content));
                    _drawer.DrawDeleteOk(items[currentItem].Content);
                }
                catch (Exception e)
                {
                    _drawer.DrawError(e, items[currentItem].Content);
                }
            else
                _drawer.DrawError(new ExtensionException("File has wrong extension"), items[currentItem].Content);
        }

        /// <summary>
        ///     Method that generates items with handling exceptions.
        /// </summary>
        /// <param name="currentDir"> current directory </param>
        /// <param name="files"> files in current directory </param>
        /// <param name="dirs"> dirs in current directory </param>
        /// <param name="currentActive"> iterator over items </param>
        /// <returns> List of items(selectable) </returns>
        private List<Item> UpdateItems(DirectoryInfo currentDir, ref List<string> files, ref List<string> dirs,
            ref int currentActive)
        {
            try
            {
                GenerateDirsAndFiles(currentDir, out files, out dirs);
            }
            catch (Exception e)
            {
                _drawer.DrawError(e, currentDir.FullName);
            }

            var items = GenerateItems(dirs, files, ref currentActive);
            return items;
        }

        /// <summary>
        ///     Method copies file.
        ///     If _fileCopy buffer is null - saves current file to it
        ///     else copies _fileCopy to current folder.
        /// </summary>
        /// <param name="items"> dirs and folders </param>
        /// <param name="currentItem"> iterator over items </param>
        private void CopyFile(List<Item> items, int currentItem)
        {
            // Check buffers
            if (_fileMove != null)
            {
                _drawer.DrawError(new Exception("move"), items[currentItem].Content);
                return;
            }

            if (_fileConc != null)
            {
                _drawer.DrawError(new Exception("conc"), items[currentItem].Content);
                return;
            }

            if (_fileCopy == null)
            {
                if (items[currentItem].IsFile)
                {
                    _prevPath = Path.Combine(_root.FullName, items[currentItem].Content);
                    _fileCopy = new FileInfo(_prevPath);
                    _drawer.DrawCopyOk(items[currentItem].Content);
                }
                else
                {
                    _drawer.DrawError(new FolderFileException(), items[currentItem].Content);
                }
            }
            else
            {
                try
                {
                    _fileCopy.CopyTo(Path.Combine(_root.FullName, _fileCopy.Name));
                    _fileCopy = null;
                    _prevPath = "";
                }
                catch (Exception e)
                {
                    _drawer.DrawError(e, _prevPath);
                    _fileCopy = null;
                    _prevPath = "";
                }
            }
        }

        /// <summary>
        ///     Method changes _encoding.
        /// </summary>
        private void ChangeEncoding()
        {
            _drawer.DrawChangeEncoding(_encoding.EncodingName);
            do
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        _encoding = Encoding.UTF8;
                        return;
                    case ConsoleKey.D2:
                        _encoding = Encoding.ASCII;
                        return;
                    case ConsoleKey.D3:
                        _encoding = Encoding.Unicode;
                        return;
                    case ConsoleKey.D4:
                        _encoding = Encoding.UTF32;
                        return;
                    case ConsoleKey.Escape:
                        return;
                }
            } while (true);
        }

        /// <summary>
        ///     Method that prints current file if file has valid extension.
        /// </summary>
        /// <param name="items"> dirs and files </param>
        /// <param name="currentActive"> iterate over items </param>
        private void ReadFile(List<Item> items, int currentActive)
        {
            var extension = Path.GetExtension(items[currentActive].Content);
            if (_readableExtensions.Contains(extension.Remove(0, 1)))
                try
                {
                    var data = File.ReadAllLines(Path.Combine(_root.FullName, items[currentActive].Content), _encoding);
                    _drawer.DrawTextFile(data, items[currentActive].Content);
                }
                catch (Exception e)
                {
                    _drawer.DrawError(e, items[currentActive].Content);
                }
            else
                _drawer.DrawError(new ExtensionException("File has wrong extension"), items[currentActive].Content);
        }

        /// <summary>
        ///     Method decreases currentItem iterator.
        ///     Used to move ^
        ///     Also provides out of range check.
        /// </summary>
        /// <param name="items"> dirs and files </param>
        /// <param name="currentActive"> iterator over items </param>
        private static void DecreaseIterator(ref List<Item> items, ref int currentActive)
        {
            items.Find(item => item.Active)?.SetInactive();
            if (currentActive - 1 < 0)
                currentActive = items.Count - 1;
            else
                currentActive--;

            items[currentActive].SetActive();
        }

        /// <summary>
        ///     Method increases currentItem iterator.
        ///     Used to move v
        ///     Also provides out of range check.
        /// </summary>
        /// <param name="items"> dirs and files </param>
        /// <param name="currentActive"> iterator over items </param>
        private static void IncreaseIterator(ref List<Item> items, ref int currentActive)
        {
            items.Find(item => item.Active)?.SetInactive();
            if (currentActive >= items.Count - 1)
                currentActive = 0;
            else
                currentActive++;
            items[currentActive].SetActive();
        }

        /// <summary>
        ///     Method generates items from dirs and files in current folder.
        /// </summary>
        /// <param name="dirs"> directories in current folder </param>
        /// <param name="files"> files in current folder </param>
        /// <param name="currentItem">
        ///     iterator over items is ref, because if last element is deleted
        ///     iterator should point to prev element.
        /// </param>
        /// <returns> List of items(selectable) </returns>
        private static List<Item> GenerateItems(List<string> dirs, List<string> files, ref int currentItem)
        {
            var items = new List<Item> { new Item("../") };
            items.AddRange(dirs.Select(dir => new Item(Path.GetFileName(dir))));
            items.AddRange(files.Select(file => new Item(Path.GetFileName(file), true)));


            if (currentItem >= items.Count)
                currentItem = Math.Max(0, items.Count - 1);
            items[currentItem].SetActive();

            return items;
        }

        /// <summary>
        ///     Method returns files and dirs in current folder.
        /// </summary>
        /// <param name="currentDir"> current folder </param>
        /// <param name="files">files in current folder </param>
        /// <param name="dirs">dirs in current folder </param>
        private static void GenerateDirsAndFiles(DirectoryInfo currentDir, out List<string> files,
            out List<string> dirs)
        {
            dirs = new List<string>(Directory.GetDirectories(currentDir.FullName));
            dirs.RemoveAll(dir => dir.EndsWith(".sys") || Path.GetFileName(dir).StartsWith("$"));

            files = new List<string>(Directory.GetFiles(currentDir.FullName));
            files.RemoveAll(file => file.EndsWith(".sys") || Path.GetFileName(file).StartsWith("$"));
        }

        /// <summary>
        ///     Method exits from application.
        /// </summary>
        private void Exit()
        {
            _drawer.DrawEndingScreen();
        }
    }
}