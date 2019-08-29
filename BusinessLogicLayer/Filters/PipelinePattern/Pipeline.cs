﻿namespace BusinessLogicLayer.Filters.PipelinePattern
{
    public class Pipeline<T> : IFilterChain<T>
    {
        private IFilter<T> _root;

        public T Execute(T input)
        {
            return _root.Execute(input);
        }

        public IFilterChain<T> Register(IFilter<T> filter)
         {
             if (_root == null)
             {
                 _root = filter;
             }
             else
             {
                 _root.Register(filter);
             }
             return this;
         }
    }

   
 }
