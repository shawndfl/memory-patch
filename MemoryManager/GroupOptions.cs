using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager
{
    /// <summary>
    /// These are options list that help define numerical values in memory.
    /// The user will have the ability to create and edit them.
    /// </summary>
    public class GroupOptions
    {
        public string Name { get; set; }

        private Dictionary<string, Option> Options =
            new Dictionary<string, Option>();

        #region Constructors
        public GroupOptions()
        {
        }
        public GroupOptions(SaveGroupOptions data)
        {
            Name = data.Name;
            foreach (Option option in data.Options)
            {
                Options.Add(option.Value, option);
            }
        }
        #endregion

        #region Public Functions
        public Option GetOption(string value)
        {
            Option option;
            Options.TryGetValue(value, out option);
            return option;
        }

        public List<Option> GetOptions()
        {
            //Option[] options = new Option[Options.Count];
            //int i = 0;
            //foreach (Option option in Options.Values)
            //{
            //    options[i++] = option;
            //}
            return Options.Values.ToList<Option>();
        }

        public bool AddOption(string itemName, string value, out Option option)
        {
            bool added = false;
            if (!Options.TryGetValue(value, out option))
            {
                option = new Option();
                option.Item = itemName;
                option.Value = value;

                Options.Add(value, option);
                added = true;
            }

            option.Item = itemName;
            return added;
        }

        public void RemoveOption(string value)
        {
            Options.Remove(value);
        }

        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
