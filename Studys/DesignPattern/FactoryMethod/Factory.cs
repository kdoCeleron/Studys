using System;
using System.Collections.Generic;
using FactoryMethod.Interface;

namespace FactoryMethod
{ 
    public class Factory<T> where T : class, IProduct
    {
        private List<string> owners = new List<string>();

        public T Create(string owner)
        {
            Type t = typeof(T);
            var obj = Activator.CreateInstance(t, owner);
            T p = obj as T;
            
            RegisterProduct(p);
            return p;
        }

        private void RegisterProduct(T product)
        {
            owners.Add(product.Owner);
        }

        public IEnumerable<string> Owners
        {
            get { return owners; }
        }
    }
}