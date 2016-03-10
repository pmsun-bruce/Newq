namespace Newq
{
    using System;
    using System.Collections.Generic;

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
        public bool Run()
        {
            Items.Clear();

            if (handler != null)
            {
                handler(this);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a string that represents the current customization.
        /// </summary>
        /// <returns></returns>
        public string GetCustomization()
        {
            var target = string.Empty;

            Run();
            Items.ForEach(item => target += string.Format("{0}, ", item));

            return target.Length > 0 ? target.Remove(target.Length - 2) : target;
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
