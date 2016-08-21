using System;
using System.Collections;
using System.Collections.Generic;

namespace MongoDB.ControlLib.Driver
{
    public class Filter : IEnumerable<KeyValuePair<object, object>>, IEnumerator<KeyValuePair<object, object>>
    {

        int _position = -1;
        KeyValuePair<object, object>[] _list = new KeyValuePair<object, object>[0];

        public void Add(KeyValuePair<object, object> filter)
        {
            KeyValuePair<object, object>[] temp = new KeyValuePair<object, object>[_list.Length + 1];
            Array.Copy(_list, temp, _list.Length);
            _position++;
            temp[_position] = filter;
            _list = temp;
        }
        public KeyValuePair<object, object> this[int index]
        {
            get
            {
                try
                {
                    return _list[index];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public KeyValuePair<object, object> Current
        {
            get
            {
                try
                {
                    return _list[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Dispose()
        {
            _list = null;
        }

        public bool MoveNext()
        {
            _position++;
            return (_position < _list.Length);
        }

        public void Reset()
        {
            _position = -1;
        }
        public IEnumerator<KeyValuePair<object, object>> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
