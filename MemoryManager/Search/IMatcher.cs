using MemoryManager.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager.Search
{
    abstract class Matcher
    {
        protected SearchType _searchType;
        protected float _changedBy;
        protected List<String> _parameters;

        public abstract void Initialize(byte[] target, SearchType type, List<String> parameters);

        public abstract bool Compare(byte[] buffer, int startIndex);
    }

    class ByteMatcher : Matcher
    {
        private byte _target;
        private byte p1, p2;

        public override bool Compare(byte[] buffer, int startIndex)
        {
            byte value = buffer[startIndex];
            switch (_searchType)
            {
                case SearchType.Excat:
                    return value == _target;
                case SearchType.UnKnown:
                    return true;                    
                case SearchType.HasIncreased:
                    return value > _target;                    
                case SearchType.HasDecreased:
                    return value > _target;
                case SearchType.HasNotChanged:
                    return value == _target;
                case SearchType.HasChanged:
                    return value != _target;                    
                case SearchType.HasDecreasedBy:
                    return ((value - p1) < _target);
                default:
                    break;
            }
            return false;
        }

        public override void Initialize(byte[] target, SearchType type, List<String> parameters)
        {
            _target = target[0];
            _searchType = type;
            if (parameters.Count > 0)
            {
                byte[] parameter1 = Parser.ToBytes(parameters[0], DataType.Byte);
                p1 = Parser.ToByte(parameter1, 0);
            }
        }
    }
}
