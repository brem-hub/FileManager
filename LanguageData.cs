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
        public string[] Instruction(int n)
        {
            switch (language)
            {
                case "rus":
                default:
                    return new string[] { "В классическом варианте игры компьютер загадывает 4-значное число.",
                                          "В режиме с N-значным числом, пользователь должен ввести размер числа [1, 10]",
                                          "Задача пользователя - угадать это число за меньшее число ходов.",
                                          "После того как игрок вводит своё предположение, игра показывает:",
                                          "сколько коров - чисел входящих в число, но не стоящих на своих местах",
                                          "cколько быков - чисел стоящих на своих местах находятся в предложенном числе.",
                                         $"Всего у игрока {n} ходов.",
                                          "Удачи!!!",
                                          "",
                                          "Нажмите любую клавишу, для возврата в меню"};
                case "eng":
                    return new string[] {"In the classic version of the game, the computer makes a 4-digit number.",
                                         "In N-digit mode, the user must enter the size of the number [1, 10]",
                                         "The user's task is to guess this number in fewer moves.",
                                         "After the player enters their guess, the game shows:",
                                         "how many cows are numbers included in the number, but not standing in their places",
                                         "how many bulls - numbers standing in their places are in the suggested number.",
                                        $"The player has {n} total moves.",
                                         "Good luck!!!",
                                         "",
                                         "Press any key to return to the menu"};
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

        public string[] NewFileName()
        {
            switch (language)
            {
                case "rus":
                default:
                    return new[] { "Введите новое имя файла вместе с расширением." };
                case "eng":
                    return new[] { "Input new file name with extension." };
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
