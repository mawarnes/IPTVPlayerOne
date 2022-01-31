using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using iptvplayer.Models;
using SQLite;
using Xamarin.Essentials;

namespace iptvplayer.Services
{
    public class BaseDataService<T> : IDataService<T> where T : DbEntity, new()
    {
        protected SQLiteAsyncConnection db;

        public async Task Init()
        {
            if (db != null)
                return;
            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "IPTVPlayerOne.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<T>();
            await InitTable();

            Console.WriteLine($"{typeof(T).Name} Table created!");
        }

        protected virtual Task InitTable()
        {
            return Task.FromResult(default(object));
        }

        public async Task Remove(int id)
        {
            await Init();
            await db.DeleteAsync<T>(id);
        }

        public async Task<IEnumerable<T>> Get()
        {
            await Init();
            return await db.Table<T>().ToListAsync();
        }

        public async Task<int> Add(T dbEntity)
        {
            await Init();
            return await db.InsertAsync(dbEntity);
        }

        public async Task<int> AddAll(IEnumerable<T> dbEntityList)
        {
            await Init();
            return await db.InsertAllAsync(dbEntityList);
        }

        public async Task<T> GetById(int id)
        {
            await Init();
            return await db.Table<T>().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<int> Update(T dbEntity)
        {
            await Init();
            return await db.UpdateAsync(dbEntity);
        }
    }
}
