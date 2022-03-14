using CodeLouisvilleLibrary.Serialization.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeLouisvilleLibrary.Serialization
{
    public class EntitySerializationService<T> where T : IEntityWithID
    {
        protected string FileName;

        public EntitySerializationService(string fileName)
        {
            FileName = fileName;
        }

        public virtual async Task<T> SaveAsync(T item)
        {
            List<T> items = new List<T>();

            if (!string.IsNullOrWhiteSpace(Path.GetDirectoryName(FileName)) && !Directory.Exists(Path.GetDirectoryName(FileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(FileName));

            using (Stream stream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                if (File.Exists(FileName) && stream.Length > 0)
                {
                    items = await JsonSerializer.DeserializeAsync<List<T>>(stream) ?? new List<T>();
                }

                if (item.ID <= 0)
                    item.ID = items.Count > 0 ? items.Max(i => i.ID) + 1 : 1;
                else
                    items.RemoveAll(i => i.ID == item.ID);

                items.Add(item);

                stream.SetLength(0); // rewrite instead of appending to the stream

                var options = new JsonSerializerOptions { WriteIndented = true };

                await JsonSerializer.SerializeAsync(stream, items, options);
            }

            return item;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            if (File.Exists(FileName))
            {
                using FileStream stream = File.OpenRead(FileName);

                return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(stream);
            }
            else
            {
                return null;
            }
        }

        public virtual async Task DeleteAsync(int ID)
        {
            await DeleteAsync(await GetByID(ID));
        }

        public virtual async Task DeleteAsync(T item)
        {
            if (item != null)
            {
                List<T> items = new List<T>();

                if (!Directory.Exists(Path.GetDirectoryName(FileName)))
                    Directory.CreateDirectory(Path.GetDirectoryName(FileName));

                using (Stream stream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    if (File.Exists(FileName) && stream.Length > 0)
                    {
                        items = await JsonSerializer.DeserializeAsync<List<T>>(stream) ?? new List<T>();
                    }

                    items.RemoveAll(i => i.ID == item.ID);

                    stream.SetLength(0); // rewrite instead of appending to the stream

                    var options = new JsonSerializerOptions { WriteIndented = true };

                    await JsonSerializer.SerializeAsync(stream, items, options);
                }
            }
        }

        public virtual async Task<T> GetByID(int ID)
        {
            var list = await GetAllAsync();
            return list.FirstOrDefault(e => e.ID == ID);
        }


    }
}