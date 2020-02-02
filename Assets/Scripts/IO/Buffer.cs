using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("MM26.IO.Tests")]
namespace MM26.IO
{
    /// <summary>
    /// A buffer object
    /// </summary>
    internal sealed class Buffer
    {
        byte[] _buffer;

        int _count;
        int _size;

        /// <summary>
        /// Get the array segment
        /// </summary>
        public ArraySegment<byte> ArraySegment
        {
            get
            {
                return new ArraySegment<byte>(_buffer, _count, _size);
            }
        }

        /// <summary>
        /// Create a copy of the content of the buffer
        /// </summary>
        public byte[] Content
        {
            get
            {
                byte[] buffer = new byte[_count];
                Array.Copy(_buffer, buffer, _count);

                return buffer;
            }
        }

        /// <summary>
        /// Create a buffer
        /// </summary>
        /// <param name="size">initial size, default to 1024</param>
        public Buffer(int size = 1024)
        {
            _count = 0;
            _size = size;

            _buffer = new byte[_size];
        }

        /// <summary>
        /// Inform the buffer that some data has been appended to the buffer
        /// </summary>
        /// <param name="count">the number of bytes appended</param>
        public void Append(int count)
        {
            _count += count;

            if (_count == _size)
            {
                _size *= 2;
                Array.Resize(ref _buffer, _size);
            }
        }

        /// <summary>
        /// Reset the buffer
        /// </summary>
        public void Reset()
        {
            _count = 0;
        }
    }
}
