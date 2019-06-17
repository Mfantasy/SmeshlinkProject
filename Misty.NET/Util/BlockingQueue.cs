using System;
using System.Collections.Generic;
using System.Threading;

namespace SmeshLink.Misty.Util
{
    /// <summary>
    /// Blocking queue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BlockingQueue<T>
    {
        readonly Queue<T> _queue;
        readonly Int32 _maxSize;
        readonly Object _syncRoot = new Byte[0];

        /// <summary>
        /// Initializes a blocking queue.
        /// </summary>
        public BlockingQueue()
        {
            _queue = new Queue<T>();
        }

        /// <summary>
        /// Initializes a blocking queue with a max size.
        /// </summary>
        /// <param name="maxSize">the max size of this queue</param>
        public BlockingQueue(Int32 maxSize)
        {
            if (maxSize < 0)
                throw new ArgumentException("The max size should be not be less than 0.", "maxSize");
            _maxSize = maxSize;
            _queue = new Queue<T>(maxSize);
        }

        /// <summary>
        /// Gets a value indicating whether this queue is empty or not.
        /// </summary>
        public Boolean IsEmpty
        {
            get { return _queue.Count == 0; }
        }

        /// <summary>
        /// Gets the max size of this blocking queue, or 0 if not limited.
        /// </summary>
        public Int32 MaxSize
        {
            get { return _maxSize; }
        }

        /// <summary>
        /// Offers an item to this queue.
        /// </summary>
        /// <param name="item">the item to be enqueued</param>
        public void Enqueue(T item)
        {
            lock (_syncRoot)
            {
                if (_maxSize > 0)
                {
                    while (_queue.Count >= _maxSize)
                    {
                        Monitor.Wait(_syncRoot);
                    }
                }
                _queue.Enqueue(item);
                Monitor.PulseAll(_syncRoot);
            }
        }

        /// <summary>
        /// Dequeues an item from this queue.
        /// </summary>
        /// <param name="millisecondsTimeout">the time in milliseconds before timeout</param>
        /// <returns>the dequeued item</returns>
        /// <exception cref="System.Threading.ThreadInterruptedException"></exception>
        public T Dequeue(Int32 millisecondsTimeout)
        {
            lock (_syncRoot)
            {
                if (IsEmpty)
                {
                    Monitor.Wait(_syncRoot, millisecondsTimeout);
                }

                if (IsEmpty)
                {
                    return default(T);
                }
                else
                {
                    T item = _queue.Dequeue();
                    if (_maxSize > 0 && _queue.Count == _maxSize - 1)
                        Monitor.PulseAll(_syncRoot);
                    return item;
                }
            }
        }
    }
}
