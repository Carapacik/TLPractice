using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace University
{
    public class Menu
    {
        private readonly List<Item> _items;
        private readonly TextReader _textReader;
        private readonly TextWriter _textWriter;
        private bool _exit;

        public Menu(TextWriter textWriter, TextReader textReader)
        {
            _textWriter = textWriter;
            _textReader = textReader;
            _items = new List<Item>();
        }

        public void AddItem(string shortcut, string description, Action<string> command)
        {
            _items.Add(new Item(shortcut, description, command));
        }

        public void Run()
        {
            while (!_exit)
            {
                var command = _textReader.ReadLine();
                if (command is null or "0") break;
                try
                {
                    ExecuteCommand(command);
                }
                catch (Exception e)
                {
                    _textWriter.WriteLine(e.Message);
                }
            }
        }

        public void UniversityHelp()
        {
            _textWriter.WriteLine("Available commands");
            foreach (var command in _items) _textWriter.WriteLine($"  [{command.Shortcut}]: {command.Description}");
        }

        public void Exit()
        {
            _exit = true;
        }

        private void ExecuteCommand(string command)
        {
            if (!command.Any())
            {
                _textWriter.WriteLine("Unknown command");
            }
            else
            {
                var item = _items.Where(i => string.Equals(i.Shortcut, command, StringComparison.CurrentCultureIgnoreCase)).ToList();
                if (!item.Any())
                {
                    _textWriter.WriteLine("Unknown command");
                }
                else
                {
                    item.First().Command(command);
                    _textWriter.WriteLine("Success\n\r");
                }
            }
        }
    }
}