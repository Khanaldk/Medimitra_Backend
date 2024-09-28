using System.Collections.Concurrent;

namespace MediMitra.Services
{
    public class BookingQueueService
    {

        private readonly ConcurrentQueue<int> _bookingQueue = new ConcurrentQueue<int>();

        public void EnqueueQueue(int bookingId)
        {
            _bookingQueue.Enqueue(bookingId);
        }

        public int GetTotalQueueSize()
        {
            return _bookingQueue.Count;
        }
    }
}
