using SQLite;

namespace ShoppingList.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsChecked { get; set; }
    }
}
