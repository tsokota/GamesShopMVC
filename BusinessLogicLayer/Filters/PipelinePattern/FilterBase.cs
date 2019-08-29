namespace BusinessLogicLayer.Filters.PipelinePattern
{
    public abstract class FilterBase<T> : IFilter<T>
    {
        private IFilter<T> _next;

        protected abstract T Process(T input);

        public T Execute(T input)
        {
            T result = Process(input);
            if (_next != null)
            {
                result = _next.Execute(result);
            }
            return result;
        }

        public void Register(IFilter<T> filter)
        {
            if (_next == null)
            {
                _next = filter;
            }
            else
            {
                _next.Register(filter);
            }
        }
    }
}
