using CherepkoLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cherepko.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }
        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }
        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }
        /// <summary>
        /// Цена
        /// </summary>
        public float Price
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity * item.Value.Rod.Price);
            }
        }

        public virtual void AddToCart(Rod rod)
        {
            // если объект есть в корзине
            // то увеличить количество
            if (Items.ContainsKey(rod.RodId))
                Items[rod.RodId].Quantity++;
            // иначе - добавить объект в корзину
            else
                Items.Add(rod.RodId, new CartItem
                {
                    Rod = rod,
                    Quantity = 1
                });
        }

        public virtual void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }
        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            Items.Clear();
        }
    }
    /// <summary>
    /// Клас описывает одну позицию в корзине
    /// </summary>
    public class CartItem
    {
        public Rod Rod { get; set; }
        public int Quantity { get; set; }
    }
}
