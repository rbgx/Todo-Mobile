using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Todo.Models;

namespace Todo.Data
{
	public class TodoItemDatabase
	{
		readonly SQLiteAsyncConnection _database;

		public TodoItemDatabase(string dbPath)
		{
			_database = new SQLiteAsyncConnection(dbPath);
			_database.CreateTableAsync<TodoItem>().Wait();
		}

		public Task<List<TodoItem>> GetItemsAsync()
		{
			return _database.Table<TodoItem>().ToListAsync();
		}

		public Task<List<TodoItem>> GetItemsNotDoneAsync()
		{
			return _database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
		}

		public Task<TodoItem> GetItemAsync(int id)
		{
			return _database.Table<TodoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
		}

		public Task<int> SaveItemAsync(TodoItem item)
		{
			if (item.Id != 0)
			{
				return _database.UpdateAsync(item);
			}
			else {
				return _database.InsertAsync(item);
			}
		}

		public Task<int> DeleteItemAsync(TodoItem item)
		{
			return _database.DeleteAsync(item);
		}
	}
}

