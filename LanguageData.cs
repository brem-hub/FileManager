namespace FileManager
{
    /// <summary>
    /// Class that is used to return needed string in specific language.
    /// Now it supports only RUS and ENG locales.
    /// This class is needed for easier translation
    /// </summary>
    class LanguageData
    {
        /// <summary> Represents language (RUS or ENG) </summary>
        private string language;

        /// <summary>
        /// Main Constructor (https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/classes-and-structs/constructors)
        /// </summary>
        /// <param name="lang"> set language variable to a chosen language </param>
        public LanguageData(string lang)
        {
            language = lang;
        }


        //ALL THE NEXT METHODS JUST RETURN STRINGS IN THE CHOSEN LANGUAGE
        public string[] Menu()
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[] { "Выберите действие",
                                             "[1] - Выбрать диск",
                                             "[2] - Инструкция по применению",
                                             "[3] - Выйти"};
                case "eng":
                    return new[]{"Choose the option",
                                         "[1] - Choose disk",
                                         "[2] - Instruction",
                                         "[3] - Exit"};
            }
        }
        public string[] Ending()
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[] { "До свидания!" };
                case "eng":
                    return new[] { "Good bye!" };
            }
        }

        public string Directory()
        {
            switch (language)
            {
                case "rus":
                default:
                    return "[Директория]";
                case "eng":
                    return "[Directory]";
            }
        }
        public string File()
        {
            switch (language)
            {
                case "rus":
                default:
                    return "[Файл]";
                case "eng":
                    return "[File]";
            }
        }
        public string[] DiskChooseLeftMenu()
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "Выберите диск, нажав на соотв. цифру",
                        "[Esc] для выхода в главное меню"
                    };
                case "eng":
                    return new[]
                    {
                        "Select a disk by pressing the corresponding number",
                        "[Esc] to exit the main menu"
                    };
            }
        }

        public string DiskData(int i, string diskName, string size)
        {
            switch (language)
            {
                case "rus":
                default:
                    return $"{i + 1}) {diskName}. Полный размер диска {size}";
                case "eng":
                    return $"{i + 1}) {diskName}. Total size of disk is {size}";

            }

        }

        public string CurrentEncoding()
        {
            switch (language)
            {
                case "rus":
                default:
                    return "Текущая кодировка:";
                case "eng":
                    return "Current encoding:";
            }
        }

        public string Exit()
        {
            return language == "rus" ? "Выход" : "Exit";
        }
        public string[] DrawInstruction()
        {
            switch (language)
            {
                case "rus":
                default:
                    return new string[]
                    {
                        "FileManager доступен как на Windows, так и на MacOS",
                        "Функционал FileManager:",
                        "1) Перемещение по директориям осуществляется с помощью стрелочек ^ и v.",
                        "2) Для выбора папки или файла используется [Enter].",
                        "3) Изменяемыми считаются только файлы формата [txt, rtf, cp, py, js, html, css]",
                        "",
                        "Функции, доступные пользователю: ",
                        "[Enter] - выбор файла для просмотра или директории для перехода. Для возврата на",
                        "уровень наверх используйте `../`.",
                        "[c] - копирование файла. Для копирования, выберите файл (1) и нажмите [c].",
                        "файл будет скопирован в буфер. Для сохранения файла в другой директории",
                        "нажмите [c] ещё раз в нужной директории.",
                        "[m] - перемещение файла. Для перемещения, выберите файл (1) и нажмите [m].",
                        "файл будет скопирован в буфер. Для перемещения файла в другую директорию",
                        "нажмите [m] ещё раз в нужной дериктории.",
                        "[d] - удаление файла. Для удаления, выберите файл (1) и нажмите [d].",
                        "файл будет удалён. Если вы удалите перемещаемый файл - вы не сможете переместить его.",
                        "[n] - создание нового текстового документа. Для создания нового файла нажмите [n] в папке,",
                        "в которой вы хотите создать файл. Далее введите имя и начните вводить текст в файл.",
                        "для сохранения текста в файле и выхода из редактора нажмите [Esc].",
                        "[k] - конкатенация. Для конкатенации, вам нужно будет указать файл, в который запишется",
                        "результат конкатенации выбранного файла и ещё одного. Далее укажите второй файл с помощью [k].",
                        "Нажмите любую клавишу, для возврата в меню"
                    };
                case "eng":
                    return new string[]
                    {
                        "FileManager available for both Windows and MacOS",
                        "The Functionality Of The FileManager:",
                        "1) moving the directories is performed using the arrows ^ and v.",
                        "2) To select a folder or file, use [Enter].",
                        "3) Variable are considered to be the only file format [txt, rtf, cp, py, js, html, css]",
                        "",
                        "Functions available to the user: ",
                        "[Enter] - select a file to view or a directory to navigate to. To return to",
                        "parent folder, use`../`.",
                        "[c] - copy the file. To copy, select the file (1) and press [c].",
                        "The file will be copied to the clipboard. To save the file in a different directory",
                        "press [c] again in the desired directory.",
                        "[m] - move the file. To move, select a file (1) and press [m].",
                        "The file will be copied to the clipboard. To move the file to another directory",
                        "press [m] again in the desired directory.",
                        "[d] - delete the file. To delete, select the file (1) and press [d].",
                        "The file will be deleted. If you delete a moving file, you will not be able to move it.",
                        "[n] - create a new text document. To create a new file, press [n] in the folder",
                        "where you want to create the file. Then enter a name and start entering text in the file.",
                        "To save the text in the file and exit the editor, press [Esc].",
                        "[k] - concatenation. For concatenation, you will need to specify the file to write to with [k]",
                        "result of concatenation of the selected file and another one.",
                        "Next, specify the second file using [k].",
                        "Press any key to return to the menu"
                    };
            }
        }

        public string[] DirFileMenu()
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "[^/v] выбор файла/папки",
                        "[e] выбор кодировки",
                        "[c] копирование файла",
                        "[d] удаление файла",
                        "[n] создание файла",
                        "[m] перемещение файла",
                        "[k] конкатенация файла",
                        "[x] отмена операции",
                        "[Enter] просмотр файла",
                        "        вход в папку",
                        "[Ecs] выход в меню"
                    };
                case "eng":
                    return new[]
                    {
                        "[^/v] select file/folder",
                        "[e] encoding",
                        "[c] copy file",
                        "[d] delete file",
                        "[n] create file",
                        "[m] move file",
                        "[k] concatenate file",
                        "[x] cancel operation",
                        "[Enter] view file",
                        "          folder",
                        "[Ecs] exit to menu"
                    };
            }
        }

        public string[] AddTextData()
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "[Esc] Сохранить и выйти"
                    };
                case "eng":
                    return new[]
                    {
                        "[Esc] Save and exit"
                    };
            }
        }
        public string[] ChooseEncoding()
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "Меню выбора кодировки.",
                        "Файлы будут читаться и записываться в выбранной вами кодировке"
                    };
                case "eng":
                    return new[]
                    {
                        "Encoding selection menu.",
                        "Files will be read and written in the encoding you selected"
                    };
            }
        }

        public string[] DrawConcOk(string fileName)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        $"Файл {fileName} в буфере.",
                        "Для конкатенации его с другим файлом выберите его и нажмите [k]"
                    };
                case "eng":
                    return new[]
                    {
                        $"File {fileName} in the buffer.",
                        "to concatenate it with another file, select it and press [k]"
                    };
            }
        }
        public string[] DrawCopyOk(string fileName)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        $"Файл {fileName} был успешно скопирован.",
                        "Для копирования в нужную папку перейдите в неё и нажмите [c]"
                    };
                case "eng":
                    return new[]
                    {
                        $"File {fileName} was successfully copied.",
                        "To copy to the desired folder, go to it and press [c]"
                    };
            }
        }
        public string[] CopiedFile(string fileName)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[] { "Скопированный файл:", fileName };
                case "eng":
                    return new[] { "Copied file:", fileName };
            }
        }
        public string[] ConcFile(string fileName)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[] { "Файл для конкатенации:", fileName };
                case "eng":
                    return new[] { "File to concatenate:", fileName };
            }
        }
        public string[] DrawMoveOk(string fileName)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        $"Файл {fileName} был успешно помещён в буфер.",
                        "Для копирования в нужную папку перейдите в неё и нажмите [m]"
                    };
                case "eng":
                    return new[]
                    {
                        $"File {fileName} was successfully moved to buffer.",
                        "To copy to the desired folder, go to it and press [m]"
                    };
            }
        }
        public string[] DrawDeleteOk(string fileName)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        $"Файл {fileName} был успешно удалён."
                    };
                case "eng":
                    return new[]
                    {
                        $"File {fileName} was successfully deleted."
                    };
            }
        }

        public string[] MovedFile(string fileName)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "Файл для перемещения:",
                        fileName
                    };
                case "eng":
                    return new[]
                    {
                        "File to move:",
                        fileName
                    };
            }
        }
        public string[] NewFileName()
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "Введите новое имя файла вместе с расширением.",
                        "Для отмены нажмите [Esc]"
                    };
                case "eng":
                    return new[]
                    {
                        "Input new file name with extension.",
                        "To cancel, press [Esc]"
                    };
            }
        }

        // ======================================================
        //                       Errors
        // ======================================================
        public string[] UnauthorizedAccessE(string fileName)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "Произошла ошибка доступа. У вас нет доступа к данному файлу/папке",
                        fileName
                    };
                case "eng":
                    return new[] { "An access error occurred. You do not have access to this file/folder", fileName };
            }
        }
        public string[] ExtensionE(string fileName)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        $"Ошибка файла. Расширение файла {fileName} не поддерживается.",
                        "Подробная информация доступна в инструкции."
                    };
                case "eng":
                    return new[]
                    {
                        $"File error. The file {fileName} extension is not supported.",
                        "Detailed information is available in the instructions."
                    };
            }
        }
        public string[] FolderFileE(string fileName)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        $"Ошибка файла. {fileName} является папкой.",
                        "Невозможно проводить операции с папками."
                    };
                case "eng":
                    return new[]
                    {
                        $"File error. {fileName} is a folder.",
                        "Folder operations cannot be performed."
                    };
            }
        }
        public string[] DirectoryNotFoundE(string fileName)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "Произошла ошибка. Данного файла/папки не существует",
                        fileName
                    };
                case "eng":
                    return new[] { "Error occured. This file/folder does not exist.", fileName };
            }
        }
        public string[] MoveFolderE(string fileName)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "Ошибка копирования. Нельзя копировать папку."
                    };
                case "eng":
                    return new[]
                    {
                        "Copying error. Cannot copy a folder."
                    };
            }
        }
        public string[] PlugE(string error)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "Произошла внутренняя ошибка. Текст ошибки:", error };
                case "eng":
                    return new[] { "Internal error occured. Error text: ", error };
            }
        }
        public string[] OperationCopyE()
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "Буфер занят другим файлом, отмените операцию копирования, чтобы продолжить."
                    };
                case "eng":
                    return new[]
                    {
                        "The buffer is occupied by another file, cancel the copy operation to continue."
                    };
            }
        }
        public string[] OperationMoveE()
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "Буфер занят другим файлом, отмените операцию перемещения, чтобы продолжить."
                    };
                case "eng":
                    return new[]
                    {
                        "The buffer is occupied by another file, cancel the move operation to continue."
                    };
            }
        }
        public string[] OperationConcE()
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[]
                    {
                        "Буфер занят другим файлом, отмените операцию конкатенации, чтобы продолжить."
                    };
                case "eng":
                    return new[]
                    {
                        "The buffer is occupied by another file, cancel the concatenate operation to continue."
                    };
            }
        }
    }
}
