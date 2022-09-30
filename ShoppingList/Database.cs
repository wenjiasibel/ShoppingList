using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingList.Models;
using SQLite;

public class Database
{
    readonly SQLiteAsyncConnection _database;

    public Database(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Item>().Wait();
    }

    public Task<List<Item>> GetItemsAsync()
    {
        return _database.Table<Item>().ToListAsync();
    }

    public Task<int> SaveItemAsync(Item item)
    {
        return _database.InsertAsync(item);
    }

    public Task<int> DeleteItemAsync(Item item)
    {
        return _database.DeleteAsync(item);
    }

    public Task<int> UpdateItemAsync(Item item)
    {
        return _database.UpdateAsync(item);
    }
}