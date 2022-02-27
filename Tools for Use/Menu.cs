using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLouisvilleLibrary
{
    public class Menu<T>
    {
        private List<KeyValuePair<T, string>> menuItems = new List<KeyValuePair<T, string>>();

        internal List<KeyValuePair<T, string>>  MenuItems
        {
            get
            {
                return menuItems;
            }
        }

        public void AddMenuItem(T menuKey, string menuText)
        {
            menuItems.Add(new KeyValuePair<T, string>(menuKey, menuText));
        }

    }

    public class Menu:Menu<string>
    {

    }
}
