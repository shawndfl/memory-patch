using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Globalization;
using System.IO;

namespace MemoryManager
{
    /// <summary>
    /// This class manages all the groups with their addresses.
    /// It also holds all the option values.
    /// </summary>
    public class AddressManager
    {
        #region Fields
        private List<LockData> _lockList = new List<LockData>();
        private Dictionary<string, GroupAddress> _groups =
            new Dictionary<string, GroupAddress>();

        private Dictionary<string, GroupOptions> _options =
            new Dictionary<string, GroupOptions>();
        #endregion

        #region Constructors
        public AddressManager() { }

        public AddressManager(string file)
        {
            Stream stream = null;
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(AddressData));              

                stream = File.Open(file, FileMode.Open, FileAccess.Read);

                AddressData data = (AddressData)ser.Deserialize(stream);
                LoadData(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Reading File " + file + "." + ex);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }        
        #endregion

        #region Private Methods
        private void LoadData(AddressData data)
        {            
            _groups.Clear();
            foreach (SaveGroupData group in data.Groups)
            {
                if (_groups.ContainsKey(group.Name))
                    throw new Exception("Group already added " + group.Name);

                GroupAddress runtimeGroup = new GroupAddress(group);

                _groups.Add(group.Name, runtimeGroup);
            }

            _options.Clear();
            foreach (SaveGroupOptions option in data.Options)
            {
                if (_options.ContainsKey(option.Name))
                    throw new Exception("Option already added " + option.Name);

                _options.Add(option.Name, new GroupOptions(option));
            }
        }
        #endregion 

        #region Group Functions

        /// <summary>
        /// Adds a new group
        /// </summary>
        /// <param name="name">name of group</param>
        /// <returns></returns>
        public GroupAddress AddNewGroup(string name)
        {
            GroupAddress group = new GroupAddress(name);
            _groups.Add(name, group);
            return group;
        }

        /// <summary>
        /// Gets a group from a name
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public GroupAddress GetGroup(string groupName)
        {
            GroupAddress data;
            _groups.TryGetValue(groupName, out data);
            return data;        
        }

        /// <summary>
        /// Renames a group
        /// </summary>
        /// <param name="currentName">old name</param>
        /// <param name="newName">new name</param>
        /// <returns></returns>
        public GroupAddress RenameGroup(string currentName, string newName)
        {
            GroupAddress group;
            if (_groups.TryGetValue(currentName, out group))
            {
                _groups.Remove(currentName);
                group.Name = newName;
                _groups.Add(newName, group);
            }
            return group;
        }

        /// <summary>
        /// Removes a group from address manager
        /// </summary>
        /// <param name="name"></param>
        public void RemoveGroup(string name)
        {
            _groups.Remove(name);
        }

        #endregion

        #region Address Functions
        /// <summary>
        /// Adds a new address
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="address"></param>
        public void AddAddress(string groupName, SavedAddress address)
        {
            GroupAddress data;
            if (!_groups.TryGetValue(groupName, out data))
            {
                data = new GroupAddress(groupName);
                _groups.Add(groupName, data);
            }

            data.AddAddress(address);
        }       

        #endregion

        #region Option Functions

        public GroupOptions[] GetOptionGroups()
        {
            if (_options.Count > 0)
            {
                GroupOptions[] data = new GroupOptions[_options.Count];
                int i =0;
                foreach (GroupOptions group in _options.Values)
                {
                    data[i++] = group;
                }
                return data;
            }
            else
            {
                return  new GroupOptions[0];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Option GetOptionByValue(string groupName, string value)
        {
            GroupOptions group;
            if (groupName != null && _options.TryGetValue(groupName, out group))            
                return group.GetOption(value);            
            else            
                return null;            
        }

        public Option[] GetOptions(string groupName)
        {
            GroupOptions data;
            if (_options.TryGetValue(groupName, out data))
            {
                return data.GetOptions();
            }
            else
            {
                return new Option[0];
            }
        }

        public bool AddOptionGroup(string groupName)
        {
            GroupOptions data;
            if (!_options.TryGetValue(groupName, out data))
            {
                data = new GroupOptions();
                data.Name = groupName;
                _options.Add(groupName, data);
                return true;
            }
            else
            {
                return false;
            }            
        }

        public bool AddOption(string groupName, string optionName, string value, out Option option)
        {
            GroupOptions data;
            if (!_options.TryGetValue(groupName, out data))
            {
                data = new GroupOptions();
                data.Name = groupName;
                _options.Add(groupName, data);
            }
            
            return data.AddOption(optionName, value, out option);            
        }

        public void RemoveOption(string groupName, string optionValue)
        {
            GroupOptions data;
            if (_options.TryGetValue(groupName, out data))
            {
                data.RemoveOption(optionValue);
            }
        }

        #endregion       
    
        #region GUI Helpers
        //public void FillInTree(TreeNode root)
        //{
        //    root.Nodes.Clear();
        //    foreach (GroupAddress group in _groups.Values)
        //    {
        //        //create the group node
        //        TreeNode groupNode = new TreeNode(group.Name);
        //        groupNode.Tag = group;
        //        root.Nodes.Add(groupNode);

        //        //create all the addresses
        //        List<SavedAddress> lst = group.GetListOfAddresses();
        //        foreach (SavedAddress address in lst)
        //        {
        //            TreeNode addressNode = new TreeNode(address.ToString());
        //            addressNode.Tag = address;

        //            //find index for add
        //            int index = 0;
        //            foreach (TreeNode childAddress in groupNode.Nodes)
        //            {
        //                SavedAddress addressChild = childAddress.Tag as SavedAddress;
        //                if (address.Address < addressChild.Address)
        //                {
        //                    break;
        //                }
        //                index++;
        //            }

        //            //add the address node to the group
        //            if (groupNode.Nodes.Count == 0)
        //                groupNode.Nodes.Add(addressNode);
        //            else
        //                groupNode.Nodes.Insert(index, addressNode);                                     
        //        }
        //    }
        //    root.Expand();
        //}
        #endregion

        public void Save(string file)
        {

            AddressData data = new AddressData();
            //data.Process = access.ProcessName;
            //data.Module = access.ModuleName;

            //doing things this way protect encapulation
            foreach (GroupAddress group in _groups.Values)
            {
                SaveGroupData saveGroup = new SaveGroupData();
                saveGroup.Addresses = group.GetListOfAddresses();
                saveGroup.Name = group.Name;
                saveGroup.Notes = group.Notes;
                
                data.Groups.Add(saveGroup);
            }

            foreach (GroupOptions option in _options.Values)
            {
                SaveGroupOptions savedGroup = new SaveGroupOptions();
                savedGroup.Name = option.Name;
                savedGroup.Options.AddRange(option.GetOptions());

                data.Options.Add(savedGroup);
            }
            Stream stream = null;
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(AddressData));
                if (File.Exists(file))
                    File.Delete(file);

                stream = File.Open(file, FileMode.CreateNew, FileAccess.Write);

                ser.Serialize(stream, data);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving.\n" + ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
    }     
}
