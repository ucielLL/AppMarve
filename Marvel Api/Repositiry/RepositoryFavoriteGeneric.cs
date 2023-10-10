using Marvel_Api.Model;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel_Api.Repositiry
{
    public class RepositoryFavoriteGeneric
    {
        SQLiteAsyncConnection Connection;
        public RepositoryFavoriteGeneric()
        {
            Connection =
                            new SQLiteAsyncConnection(Constants.DatabasePath,
                          Constants.Flags);
        }
       public  void initTables()
        {
            Connection.CreateTableAsync<Thumbnail>().ConfigureAwait(false);
            Connection.CreateTableAsync<Item>().ConfigureAwait(false);
            Connection.CreateTableAsync<Characters>().ConfigureAwait(false);
            Connection.CreateTableAsync<ItemResultSeries>().ConfigureAwait(false);
            Connection.CreateTableAsync<ItemResultBase>().ConfigureAwait(false);


        }
        public void Dispose()
        {
            Connection.CloseAsync();
        }

        public async Task<bool> ItemExist<T>(int idElementApi) where T : ItemResultBase, new()
        {
            initTables();   

            try
            {
                var item = await Connection.Table<T>().FirstOrDefaultAsync(i => i.id == idElementApi);
                return item != null;
            }
            catch (Exception) { return false; }

        }

        public async Task<bool> SaveOrRemplaceItemWithChildrenAsync<TItem>(TItem item, bool recursive = false)
        {

            try
            {
                initTables();

                await Connection.InsertOrReplaceWithChildrenAsync(item, true);

                return true;
            }
            catch (Exception EX)
            {
                var E = EX.Message;
                return false;
            }
        }
        public async Task<bool> DelateWithChindenAsync<Ttable>(int id)
        {
            try
            {
                var nDelate = await Connection.DeleteAsync<Ttable>(id);

                return nDelate < 1 ? false : true;
            }
            catch (Exception ex) { return false; }
        }

        public async Task<List<ItemBase>> GetAsSimpleItemsAsync<Ttable>() where Ttable : ItemResultBase, new()
        {
            try
            {
                if (await Connection.Table<Ttable>().CountAsync() == 0) return null;
                var list = await Connection.Table<Ttable>().ToListAsync();

                return (List<ItemBase>)list.Select(i => {
                    return i as ItemBase;
                }
                ).ToList();
            }
            catch (Exception ex) { var e = ex.Message; return null; }
        }

        public async Task<Ttable> GetItemById<Ttable>(int id) where Ttable : ItemResultBase, new() 
        {
            try
            {
               return await Connection.GetWithChildrenAsync<Ttable>(id);
            }
            catch (Exception){ return null; }
        }


    }
}

