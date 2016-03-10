namespace Newq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Target collection of statement or clause.
    /// </summary>
    public class Target : ICustomizable<Action<Target>>
    {
        /// <summary>
        /// 
        /// </summary>
        protected Action<Target> handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="Target"/> class.
        /// </summary>
        /// <param name="context"></param>
        public Target(Context context)
        {
            Context = context;
            Items = new List<object>();
        }

        /// <summary>
        /// 
        /// </summary>
        public Context Context { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public List<object> Items { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public void SetHandler(Action<Target> handler)
        {
            this.handler = handler;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            handler(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<OrderByColumn> GetOrderByColumns()
        {
            var list = new List<OrderByColumn>();

            Items.ForEach(item => {
                if (item is OrderByColumn)
                {
                    list.Add(item as OrderByColumn);
                }
            });

            return list;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var target = string.Empty;

            if (Items.Count > 0)
            {
                foreach (var item in Items)
                {
                    target += string.Format("{0}, ", item);
                }

                target = target.Remove(target.Length - 2);
            }

            return target;
        }

        /// <summary>
        /// Adds a object to the end of the target.
        /// </summary>
        /// <param name="item"></param>
        public void Add(object item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Adds a object to the end of the target.
        /// </summary>
        /// <param name="item"></param>
        public void Add(OrderByColumn item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the target.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(object item)
        {
            Items.Remove(item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the target.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(OrderByColumn item)
        {
            Items.Remove(item);
        }

        /// <summary>
        /// Removes all objects from the target.
        /// </summary>
        public void Clear()
        {
            Items.Clear();
        }

        /// <summary>
        /// <see cref="Add(object)"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Target operator +(Target target, object item)
        {
            target.Add(item);

            return target;
        }

        /// <summary>
        /// <see cref="Add(OrderByColumn)"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Target operator +(Target target, OrderByColumn item)
        {
            target.Add(item);

            return target;
        }

        /// <summary>
        /// <see cref="Remove(object)"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Target operator -(Target target, object item)
        {
            target.Remove(item);

            return target;
        }

        /// <summary>
        /// <see cref="Remove(OrderByColumn)"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Target operator -(Target target, OrderByColumn item)
        {
            target.Remove(item);

            return target;
        }
    }
}
