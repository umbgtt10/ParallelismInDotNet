using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskParallelism
{
    public class ConcurrentPipeline : SequentialPipeline
    {
        private readonly int _limit;

        public ConcurrentPipeline(int delay, int size, int limit) : base(delay, size)
        {
            _limit = limit;
        }

        public override void Start()
        {
            var loadedInfoCollection = new BlockingCollection<int>(_limit);
            var scaledInfoCollection = new BlockingCollection<int>(_limit);
            var filteredInfoCollection = new BlockingCollection<int>(_limit);

            var loadTask = Task.Factory.StartNew(() =>
                LoadPipelineImage(loadedInfoCollection));
            var scaleTask = Task.Factory.StartNew(() =>
                ScalePipelineImage(loadedInfoCollection, scaledInfoCollection));
            var filterTask = Task.Factory.StartNew(() =>
                FilterPipelineImage(scaledInfoCollection, filteredInfoCollection));
            var displayTask = Task.Factory.StartNew(() =>
                DisplayPipelineImage(filteredInfoCollection));

            Task.WaitAll(loadTask, scaleTask, filterTask, displayTask);
            Console.WriteLine("Finished");
        }

        private void DisplayPipelineImage(BlockingCollection<int> filteredInfoCollection)
        {
            foreach (var element in filteredInfoCollection.GetConsumingEnumerable())
            {
                DisplayImage(element);
            }

            Console.WriteLine("DisplayPipelineImage");
        }

        private void FilterPipelineImage(BlockingCollection<int> scaledInfoCollection, BlockingCollection<int> filteredInfoCollection)
        {
            foreach (var element in scaledInfoCollection.GetConsumingEnumerable())
            {
                FilterImage(element);
                filteredInfoCollection.Add(element);
            }

            filteredInfoCollection.CompleteAdding();
            Console.WriteLine("FilterPipelineImage");
        }

        private void ScalePipelineImage(BlockingCollection<int> loadedInfoCollection, BlockingCollection<int> scaledInfoCollection)
        {
            foreach (var element in loadedInfoCollection.GetConsumingEnumerable())
            {
                ScaleInfo(element);
                scaledInfoCollection.Add(element);
            }

            scaledInfoCollection.CompleteAdding();
            Console.WriteLine("ScalePipelineImage");
        }

        private void LoadPipelineImage(BlockingCollection<int> loadedInfoCollection)
        {
            for (int indexer = 1; indexer < _size; indexer++)
            {
                loadedInfoCollection.Add(LoadImage(indexer));
            }

            loadedInfoCollection.CompleteAdding();

            Console.WriteLine("LoadPipelineImage");
        }
    }
}